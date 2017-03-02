using Caseiro.Mvc.PagedList.Extensions;
using MvcContrib.Pagination;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationRepository _organizationRepo;
        private readonly ILookupRepository _lookupRepo;

        public OrganizationsController(
            ILookupRepository lookupRepo,
            IOrganizationRepository organizationRepo)
        {
            _lookupRepo = lookupRepo;
            _organizationRepo = organizationRepo;

            ViewBag.PossibleOrganizationTypes = _lookupRepo.GetOrganizationTypes().OrderBy(x => x.SortOrder);
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus().OrderBy(x => x.SortOrder);
        }

        #region base

        // GET: Search for BeholderPersons 
        public JsonResult GetDropdown(string term)
        {
            //TODO: Filter organization dropdown by user
            term = term.Trim();
            var list = _organizationRepo.GetDropdown(p => p.label.Contains(term)).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            return Search(searchTerm, null);
        }

        public ViewResult Search(string searchTerm, int? approvalStatus)
        {
            searchTerm = searchTerm.Trim();
            var list = approvalStatus == null ?
                                  _organizationRepo.GetOrganizations(currentUser, p => p.OrganizationName.Contains(searchTerm)) :
                                  _organizationRepo.GetOrganizations(currentUser, p => p.OrganizationName.Contains(searchTerm) && p.ApprovalStatusId.Equals(approvalStatus));

            return View("Index", list as IPagination<Organization>);
        }

        //
        // GET: /Organizations/
        //public ViewResult Index()
        public ActionResult Index(int? activeyear, int? activestatusid, List<int> movementclassid = null, string movementclassid_string = "", string organizationname = "", int? page = 1, int? pageSize = 15)
        {
            if (!string.IsNullOrWhiteSpace(movementclassid_string))
            {
                movementclassid = ((List<int>)movementclassid_string.Split(',').Select(int.Parse).ToList());
            }
            organizationname = organizationname.Trim();

            Session["movementclassid"] = movementclassid;
            Session["organizationname"] = organizationname;
            Session["activeyear"] = activeyear;
            Session["activestatusid"] = activestatusid;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            var pred = PredicateBuilder.True<Organization>();
            if (movementclassid != null) pred = pred.And(p => movementclassid.Contains((int)p.MovementClassId));
            if (!string.IsNullOrWhiteSpace(organizationname)) pred = pred.And(p => p.OrganizationName.Contains(organizationname));
            if (activeyear != null) pred = pred.And(p => p.ActiveYear == activeyear);
            if (activestatusid != null) pred = pred.And(p => p.ActiveStatusId == activestatusid);

            var list = _organizationRepo.GetOrganizations(currentUser, pred)
                .OrderBy(m => m.OrganizationName).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return list.Any() ? PartialView("_OrganizationList", list) : PartialView("Organization404");
            }

            return View("Index", list);
        }

        public ImageResult ShowPrimaryImage(int id)
        {
            byte[] picture = null;
            string contentType = null;

            var mediaImage = _organizationRepo.GetPrimaryImage(id);
            var image = mediaImage.MediaImage.Image;

            if (image == null)
            {
                picture = System.IO.File.ReadAllBytes(Server.MapPath("~/content/images/image_unavailable.jpg"));
                contentType = "image/x-png";
            }
            else
            {
                picture = ImageHelper.Decompress(image.IMAGE1);
                contentType = image.MimeType.Name;
            }

            return this.Image(picture, contentType);

        }

        public ViewResult DetailsLite(int id)
        {
            var organization = _organizationRepo.GetOrganization(currentUser, id);
            return organization != null ? View(organization) : View("Organization404");
        }
        //
        // GET: /Organizations/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.OrganizationId = id;
            ViewBag.Organization2Id = id;
            ViewBag.ChapterId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.MediaPublishedId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.Controller = "Organizations";

            var organization = _organizationRepo.GetOrganization(currentUser, id);

            if (organization != null)
            {
                return View(_organizationRepo.GetOrganization(currentUser, id));
            }
            return View("Organization404");
        }

        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var organization = new Organization
            {
                //OrganizationTypeId = Queryable.SingleOrDefault(_lookupRepo.GetOrganizationTypes(), p => p.Name.Equals("Physical")).Id,
                //ApprovalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id,
                //ActiveStatusId = Queryable.SingleOrDefault(_lookupRepo.GetActiveStatuses(), p => p.Name.Equals("Active")).Id,
                ActiveYear = DateTime.Now.Year,
                ReportStatusFlag = false
            };
            return View(organization);

        }

        //
        // POST: /Organizations/Create
        [HttpPost]
        public ActionResult Create(Organization organization)
        {
            if (ModelState.IsValid)
            {
                _organizationRepo.InsertOrUpdate(organization);
                _organizationRepo.Save();
                return RedirectToAction("Details", new { id = organization.Id });
            }
            return View();
        }

        //
        // GET: /Organizations/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var organization = _organizationRepo.GetOrganization(currentUser, id);
            if (organization != null)
            {
                return View(organization);
            }
            return View("Organization404");
        }

        //
        // POST: /Organizations/Edit/5
        [HttpPost]
        public ActionResult Edit(Organization organization)
        {
            if (ModelState.IsValid)
            {
                _organizationRepo.InsertOrUpdate(organization);
                _organizationRepo.Save();
                return RedirectToAction("Details", new { id = organization.Id });
            }
            return View();
        }

        public ActionResult RemoveOrganization(int id)
        {
            var org = _organizationRepo.GetOrganization(currentUser, id);
            return View(org);
        }

        [HttpPost]
        public ActionResult RemoveOrganization(int id, string removalreason)
        {
            var organization = _organizationRepo.GetOrganization(currentUser, id);
            organization.RemovalReason = removalreason;

            organization.RemovalStatusId = 1;
            _organizationRepo.InsertOrUpdate(organization);
            _organizationRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /Organizations/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_organizationRepo.GetOrganization(currentUser, id));
        }

        //
        // POST: /Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            _organizationRepo.Delete(id);
            _organizationRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }


        #endregion


        #region Persons

        public ActionResult GetOrganizationPersons(int organizationId, int personId)
        {
            IQueryable<OrganizationPersonRel> organizationPersons;
            if (organizationId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.OrganizationId = organizationId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                organizationPersons = _organizationRepo.GetOrganizationPersons(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.OrganizationId = organizationId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Organizations";
                organizationPersons = _organizationRepo.GetOrganizationPersons(p => p.Organization.Id.Equals(organizationId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationPersonList", organizationPersons);
            }
            return View(organizationPersons);
        }

        //
        // GET: Organizations/EditOrganizationPerson
        public ActionResult EditOrganizationPerson(int id, string source)
        {
            try
            {
                var organizationPersonRel = _organizationRepo.GetOrganziationPerson(id);
                if (source == "Organizations")
                {
                    ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                    ViewBag.Controller = source;
                    ViewBag.OrganizationId = organizationPersonRel.OrganizationId;
                }
                else
                {
                    ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                    ViewBag.Controller = source;
                    ViewBag.PersonId = organizationPersonRel.PersonId;
                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_CreateOrEditOrganizationPerson", organizationPersonRel);
                }
                return View(organizationPersonRel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // POST: Organizations/EditOrganizationPerson/
        [HttpPost]
        public ActionResult EditOrganizationPerson(OrganizationPersonRel organizationPersonRel)
        {

            if (ModelState.IsValid)
            {
                if (organizationPersonRel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    organizationPersonRel.BeholderPerson = null;
                    _organizationRepo.InsertOrUpdateOrganizationPerson(organizationPersonRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationPersonRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationPersonRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationPerson(organizationPersonRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Persons", new { id = organizationPersonRel.PersonId });
            }

            return View();
        }

        //
        // GET: Organizations/CreateOrganizationPerson
        public ActionResult CreateOrganizationPerson(int organizationId, int personId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationPersonRel = new OrganizationPersonRel()
            {
                PersonId = personId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (personId == -1)
            {
                organizationPersonRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                organizationPersonRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationPerson", organizationPersonRel);
            }

            return View();

        }

        //
        // POST: organizations/CreateOrganizationPerson/
        [HttpPost]
        public ActionResult CreateOrganizationPerson([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,PersonId")] OrganizationPersonRel organizationPersonRel)
        {
            if (ModelState.IsValid)
            {

                //if (organizationPersonRel.Organization == null)
                //{
                //    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                //    organizationPersonRel.BeholderPerson = null;
                _organizationRepo.InsertOrUpdateOrganizationPerson(organizationPersonRel);
                _organizationRepo.Save();
                //    return RedirectToAction("Details", "Organizations", new { id = organizationPersonRel.OrganizationId });
                //}
                ////reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //organizationPersonRel.Organization = null;
                //_organizationRepo.InsertOrUpdateOrganizationPerson(organizationPersonRel);
                //_organizationRepo.Save();
                //return RedirectToAction("Details", "Persons", new { id = organizationPersonRel.PersonId });
                return null;
            }
            return View(organizationPersonRel);
        }

        public void DeleteOrganizationPerson(int id)
        {
            _organizationRepo.DeleteOrganizationPerson(id);
            _organizationRepo.Save();
        }

        #endregion


        #region Events

        public ActionResult GetOrganizationEvents(int organizationId, int eventId)
        {
            IQueryable<OrganizationEventRel> organizationEvents;
            if (eventId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationEvents = _organizationRepo.GetOrganizationEvents(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Events";
                organizationEvents = _organizationRepo.GetOrganizationEvents(p => p.Event.Id.Equals(eventId));
            }

            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_OrganizationEventList", organizationEvents);
            //}
            //return View(organizationEvents);
        }

        public ActionResult CreateOrganizationEvent(int organizationId, int eventId)
        {
            //TODO: Refactor code smell?
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationEventRel = new OrganizationEventRel()
            {
                EventId = eventId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (eventId == -1)
            {
                organizationEventRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                organizationEventRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationEvent", organizationEventRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,EventId")] OrganizationEventRel organizationEventRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationEventRel.Organization == null)
                {
                    organizationEventRel.Event = null;
                    _organizationRepo.InsertOrUpdateOrganizationEvent(organizationEventRel);
                    _organizationRepo.Save();
                    return null;
                    //return RedirectToAction("Details", "Organizations", new { id = organizationEventRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationEventRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationEvent(organizationEventRel);
                _organizationRepo.Save();
                return null;
                //return RedirectToAction("Details", "Event", new { id = organizationEventRel.EventId });
            }
            return View(organizationEventRel);

        }

        public ActionResult EditOrganizationEvent(int id, string source)
        {
            var organizationEventRel = _organizationRepo.GetOrganizationEvent(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = organizationEventRel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = organizationEventRel.EventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationEvent", organizationEventRel);
            }
            return View(organizationEventRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditOrganizationEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,EventId")] OrganizationEventRel organizationEventRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationEventRel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    organizationEventRel.Event = null;
                    _organizationRepo.InsertOrUpdateOrganizationEvent(organizationEventRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationEventRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationEventRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationEvent(organizationEventRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Events", new { id = organizationEventRel.EventId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationEvent(int id)
        {
            _organizationRepo.DeleteOrganizationEvent(id);
            _organizationRepo.Save();

        }

        #endregion


        #region Media Images

        public ActionResult GetOrganizationMediaImages(int organizationId, int mediaImageId)
        {
            IQueryable<OrganizationMediaImageRel> organizationMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationMediaImages = _organizationRepo.GetOrganizationMediaImages(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "MediaImages";
                organizationMediaImages = _organizationRepo.GetOrganizationMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_OrganizationMediaImageList", organizationMediaImages);
            //}
            //return View(organizationMediaImages);
        }

        public ActionResult CreateOrganizationMediaImage(int organizationId, int mediaImageId)
        {
            //var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationMediaImageRel = new OrganizationMediaImageRel()
            {
                MediaImageId = mediaImageId,
                OrganizationId = organizationId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                organizationMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                organizationMediaImageRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaImage", organizationMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaImageId")] OrganizationMediaImageRel organizationMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaImageRel.Organization == null)
                {
                    organizationMediaImageRel.MediaImage = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaImage(organizationMediaImageRel);
                    _organizationRepo.Save();
                    //return RedirectToAction("Details", "Organizations", new { id = organizationMediaImageRel.OrganizationId });
                    return null;
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationMediaImageRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaImage(organizationMediaImageRel);
                _organizationRepo.Save();
                return null;
                //return RedirectToAction("Details", "MediaImage", new { id = organizationMediaImageRel.MediaImageId });
            }
            return View();

        }

        public ActionResult EditOrganizationMediaImage(int id, string source)
        {
            var organizationMediaImageRel = _organizationRepo.GetOrganizationMediaImage(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = organizationMediaImageRel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = organizationMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaImage", organizationMediaImageRel);
            }
            return View(organizationMediaImageRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditOrganizationMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,OrganizationId,MediaImageId")] OrganizationMediaImageRel organizationMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaImageRel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    organizationMediaImageRel.MediaImage = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaImage(organizationMediaImageRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaImageRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationMediaImageRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaImage(organizationMediaImageRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = organizationMediaImageRel.MediaImageId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationMediaImage(int id)
        {
            _organizationRepo.DeleteOrganizationMediaImage(id);
            _organizationRepo.Save();

        }

        #endregion


        #region Vehicles

        public ActionResult GetOrganizationVehicles(int organizationId, int vehicleId)
        {
            IQueryable<OrganizationVehicleRel> organizationVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Organization") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationVehicles = _organizationRepo.GetOrganizationVehicles(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Organization")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Vehicles";
                organizationVehicles = _organizationRepo.GetOrganizationVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationVehicleList", organizationVehicles);
            }
            return View(organizationVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateOrganizationVehicle(int vehicleId, int organizationId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationVehicleRel = new OrganizationVehicleRel()
            {
                VehicleId = vehicleId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                organizationVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                organizationVehicleRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationVehicle", organizationVehicleRel);
            }

            return View();

        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreateOrganizationVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,VehicleId")] OrganizationVehicleRel organizationvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (organizationvehiclerel.Organization == null)
                {
                    organizationvehiclerel.Vehicle = null;
                    _organizationRepo.InsertOrUpdateOrganizationVehicle(organizationvehiclerel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationvehiclerel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationvehiclerel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationVehicle(organizationvehiclerel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = organizationvehiclerel.VehicleId });
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditOrganizationVehicle(int id, string source)
        {
            var organizationVehicleRel = _organizationRepo.GetOrganizationVehicle(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = organizationVehicleRel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = organizationVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationVehicle", organizationVehicleRel);
            }
            return View(organizationVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditOrganizationVehicle(OrganizationVehicleRel organizationvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (organizationvehiclerel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    organizationvehiclerel.Vehicle = null;
                    _organizationRepo.InsertOrUpdateOrganizationVehicle(organizationvehiclerel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationvehiclerel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationvehiclerel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationVehicle(organizationvehiclerel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = organizationvehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationVehicle(int id)
        {
            _organizationRepo.DeleteOrganizationVehicle(id);
            _organizationRepo.Save();
        }

        #endregion


        #region Organizations

        public ActionResult GetOrganizationOrganizations(int organizationId)
        {
            //if (chapterId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Organization") && x.ObjectTo.Equals("Organization")).OrderBy(x => x.SortOrder);
            ViewBag.OrganizationId = organizationId;
            ViewBag.Organization2Id = organizationId;
            ViewBag.Controller = "Organization";
            Organization organization = _organizationRepo.GetOrganization(currentUser, organizationId);
            //}
            //else
            //{
            //    ViewBag.PossibleRelationshipTypes = context.RelationshipTypes.Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
            //    ViewBag.EventId = eventId;
            //    ViewBag.ChapterId = chapterId;
            //    ViewBag.Controller = "Events";
            //    chapterEvents = _chaptereventrelRepository.GetChapterEvents(p => p.Event.Id.Equals(eventId));
            //}

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationOrganizationList", organization);
            }
            return View(organization);
        }

        public ActionResult CreateOrganizationOrganization(int organizationId)
        {
            var org = _organizationRepo.GetOrganization(organizationId);
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationOrganizationRel = new OrganizationOrganizationRel
            {
                OrganizationId = organizationId,
                Organization = org,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
            ViewBag.OrganizationId = organizationId;
            ViewBag.Organization2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationOrganization", organizationOrganizationRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationOrganization([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,Organization2Id")] OrganizationOrganizationRel organizationOrganizationRel)
        {
            if (ModelState.IsValid)
            {
                _organizationRepo.InsertOrUpdateOrganizationOrganization(organizationOrganizationRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Organizations", new { id = organizationOrganizationRel.OrganizationId });
            }
            return View();

        }

        public ActionResult EditOrganizationOrganization(int id)
        {
            //TODO: OrganizationOrganizationRel filter by security level
            var organizationOrganizationRel = _organizationRepo.GetOrganizationOrganization(id);

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationOrganization", organizationOrganizationRel);
            }
            return View(organizationOrganizationRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditOrganizationOrganization([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,Organization2Id")]OrganizationOrganizationRel organizationOrganizationRel)
        {
            if (ModelState.IsValid)
            {
                _organizationRepo.InsertOrUpdateOrganizationOrganization(organizationOrganizationRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Organizations", new { id = organizationOrganizationRel.OrganizationId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteOrganizationOrganization(int id)
        {
            _organizationRepo.DeleteOrganizationOrganization(id);
            _organizationRepo.Save();
        }

        #endregion


        #region Media Audio Videos

        public ActionResult GetOrganizationMediaAudioVideos(int organizationId, int mediaAudioVideoId)
        {
            IQueryable<OrganizationMediaAudioVideoRel> organizationMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationMediaAudioVideos = _organizationRepo.GetOrganizationMediaAudioVideos(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "MediaAudioVideos";
                organizationMediaAudioVideos = _organizationRepo.GetOrganizationMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaAudioVideoList", organizationMediaAudioVideos);
            }
            return View(organizationMediaAudioVideos);
        }

        public ActionResult CreateOrganizationMediaAudioVideo(int organizationId, int mediaAudioVideoId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new OrganizationMediaAudioVideoRel()
            {
                MediaAudioVideoId = mediaAudioVideoId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                rel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                rel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaAudioVideo", rel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaAudioVideoId")] OrganizationMediaAudioVideoRel organizationMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                _organizationRepo.InsertOrUpdateOrganizationMediaAudioVideo(organizationMediaAudioVideoRel);
                _organizationRepo.Save();
                return null;
            }
            return View(organizationMediaAudioVideoRel);

        }

        public ActionResult EditOrganizationMediaAudioVideo(int id, string source)
        {
            var rel = _organizationRepo.GetOrganizationMediaAudioVideo(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = rel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaAudioVideo", rel);
            }
            return View(rel);

        }

        //
        // POST: /OrganizationPersonRels/EditOrganizationPerson
        [HttpPost]
        public ActionResult EditOrganizationMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,OrganizationId,MediaAudioVideoId")] OrganizationMediaAudioVideoRel organizationMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaAudioVideoRel.Organization == null)
                {
                    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                    organizationMediaAudioVideoRel.MediaAudioVideo = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaAudioVideo(organizationMediaAudioVideoRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaAudioVideoRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                organizationMediaAudioVideoRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaAudioVideo(organizationMediaAudioVideoRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = organizationMediaAudioVideoRel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationMediaAudioVideo(int id)
        {
            _organizationRepo.DeleteOrganizationMediaAudioVideo(id);
            _organizationRepo.Save();

        }

        #endregion


        #region Media Correspondence

        public ActionResult GetOrganizationMediaCorrespondences(int organizationId, int mediaCorrespondenceId)
        {
            IQueryable<OrganizationMediaCorrespondenceRel> organizationMediaCorrespondences;
            if (mediaCorrespondenceId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationMediaCorrespondences = _organizationRepo.GetOrganizationMediaCorrespondences(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "MediaCorrespondences";
                organizationMediaCorrespondences = _organizationRepo.GetOrganizationMediaCorrespondences(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaCorrespondenceList", organizationMediaCorrespondences);
            }
            return View(organizationMediaCorrespondences);
        }

        public ActionResult CreateOrganizationMediaCorrespondence(int organizationId, int mediaCorrespondenceId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new OrganizationMediaCorrespondenceRel()
            {
                MediaCorrespondenceId = mediaCorrespondenceId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaCorrespondenceId == -1)
            {
                rel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                rel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaCorrespondence", rel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaCorrespondenceId")] OrganizationMediaCorrespondenceRel organizationMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                //if (organizationMediaCorrespondenceRel.Organization == null)
                //{
                //    organizationMediaCorrespondenceRel.MediaCorrespondence = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaCorrespondence(organizationMediaCorrespondenceRel);
                _organizationRepo.Save();
                return null;
                //    return RedirectToAction("Details", "Organizations", new { id = organizationMediaCorrespondenceRel.OrganizationId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                //    organizationMediaCorrespondenceRel.Organization = null;
                //    _organizationRepo.InsertOrUpdateOrganizationMediaCorrespondence(organizationMediaCorrespondenceRel);
                //    _organizationRepo.Save();
                //    return RedirectToAction("Details", "MediaCorrespondences", new { id = organizationMediaCorrespondenceRel.MediaCorrespondenceId });
                //}
            }
            return View(organizationMediaCorrespondenceRel);

        }

        public ActionResult EditOrganizationMediaCorrespondence(int id, string source)
        {
            var rel = _organizationRepo.GetOrganizationMediaCorrespondence(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = rel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaCorrespondence", rel);
            }
            return View(rel);

        }

        //
        // POST: /OrganizationPersonRels/EditOrganizationPerson
        [HttpPost]
        public ActionResult EditOrganizationMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaCorrespondenceId")] OrganizationMediaCorrespondenceRel organizationMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaCorrespondenceRel.Organization == null)
                {
                    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                    organizationMediaCorrespondenceRel.MediaCorrespondence = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaCorrespondence(organizationMediaCorrespondenceRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaCorrespondenceRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                organizationMediaCorrespondenceRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaCorrespondence(organizationMediaCorrespondenceRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = organizationMediaCorrespondenceRel.MediaCorrespondenceId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationMediaCorrespondence(int id)
        {
            _organizationRepo.DeleteOrganizationMediaCorrespondence(id);
            _organizationRepo.Save();

        }


        #endregion


        #region Media Published

        public ActionResult GetOrganizationMediaPublisheds(int organizationId, int mediaPublishedId)
        {
            IQueryable<OrganizationMediaPublishedRel> organizationMediaPublisheds;
            if (mediaPublishedId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationMediaPublisheds = _organizationRepo.GetOrganizationMediaPublisheds(p => p.Organization.Id.Equals(organizationId)).OrderByDescending(x => x.MediaPublished.DateModified);
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "MediaPublisheds";
                organizationMediaPublisheds = _organizationRepo.GetOrganizationMediaPublisheds(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaPublishedList", organizationMediaPublisheds);
            }
            return View(organizationMediaPublisheds);
        }

        public ActionResult CreateOrganizationMediaPublished(int organizationId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new OrganizationMediaPublishedRel()
            {
                MediaPublishedId = mediaPublishedId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaPublishedId == -1)
            {
                rel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                rel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaPublished", rel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaPublishedId")] OrganizationMediaPublishedRel organizationMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                //if (organizationMediaPublishedRel.Organization == null)
                //{
                //    organizationMediaPublishedRel.MediaPublished = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaPublished(organizationMediaPublishedRel);
                _organizationRepo.Save();
                return null;
                //    return RedirectToAction("Details", "Organizations", new { id = organizationMediaPublishedRel.OrganizationId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                //    organizationMediaPublishedRel.Organization = null;
                //    _organizationRepo.InsertOrUpdateOrganizationMediaPublished(organizationMediaPublishedRel);
                //    _organizationRepo.Save();
                //    return RedirectToAction("Details", "MediaPublisheds", new { id = organizationMediaPublishedRel.MediaPublishedId });
                //}
            }
            return View(organizationMediaPublishedRel);

        }

        public ActionResult EditOrganizationMediaPublished(int id, string source)
        {
            var rel = _organizationRepo.GetOrganizationMediaPublished(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = rel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaPublished", rel);
            }
            return View(rel);

        }

        //
        // POST: /OrganizationPersonRels/EditOrganizationPerson
        [HttpPost]
        public ActionResult EditOrganizationMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,OrganizationId")] OrganizationMediaPublishedRel organizationMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaPublishedRel.Organization == null)
                {
                    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                    organizationMediaPublishedRel.MediaPublished = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaPublished(organizationMediaPublishedRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaPublishedRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                organizationMediaPublishedRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaPublished(organizationMediaPublishedRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = organizationMediaPublishedRel.MediaPublishedId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationMediaPublished(int id)
        {
            _organizationRepo.DeleteOrganizationMediaPublished(id);
            _organizationRepo.Save();

        }


        #endregion


        #region Media WebsiteEGroup

        public ActionResult GetOrganizationMediaWebsiteEGroups(int organizationId, int mediaWebsiteEGroupId)
        {
            IQueryable<OrganizationMediaWebsiteEGroupRel> organizationMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationMediaWebsiteEGroups = _organizationRepo.GetOrganizationMediaWebsiteEGroups(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                organizationMediaWebsiteEGroups = _organizationRepo.GetOrganizationMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaWebsiteEGroupList", organizationMediaWebsiteEGroups);
            }
            return View(organizationMediaWebsiteEGroups);
        }

        public ActionResult CreateOrganizationMediaWebsiteEGroup(int organizationId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new OrganizationMediaWebsiteEGroupRel()
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                rel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                rel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaWebsiteEGroup", rel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,MediaWebsiteEGroupId")] OrganizationMediaWebsiteEGroupRel organizationMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaWebsiteEGroupRel.Organization == null)
                {
                    organizationMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaWebsiteEGroup(organizationMediaWebsiteEGroupRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaWebsiteEGroupRel.OrganizationId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                    organizationMediaWebsiteEGroupRel.Organization = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaWebsiteEGroup(organizationMediaWebsiteEGroupRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = organizationMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
                }
            }
            return View();

        }

        public ActionResult EditOrganizationMediaWebsiteEGroup(int id, string source)
        {
            var rel = _organizationRepo.GetOrganizationMediaWebsiteEGroup(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = rel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationMediaWebsiteEGroup", rel);
            }
            return View(rel);

        }

        //
        // POST: /OrganizationPersonRels/EditOrganizationPerson
        [HttpPost]
        public ActionResult EditOrganizationMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,OrganizationId")] OrganizationMediaWebsiteEGroupRel organizationMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationMediaWebsiteEGroupRel.Organization == null)
                {
                    //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                    organizationMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _organizationRepo.InsertOrUpdateOrganizationMediaWebsiteEGroup(organizationMediaWebsiteEGroupRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationMediaWebsiteEGroupRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not OrganizationOrganizationRel.
                organizationMediaWebsiteEGroupRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationMediaWebsiteEGroup(organizationMediaWebsiteEGroupRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = organizationMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationMediaWebsiteEGroup(int id)
        {
            _organizationRepo.DeleteOrganizationMediaWebsiteEGroup(id);
            _organizationRepo.Save();

        }


        #endregion


        #region Subscriptions

        public ActionResult GetOrganizationSubscriptions(int organizationId, int subscriptionId)
        {
            IQueryable<OrganizationSubscriptionRel> organizationSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Organizations";
                organizationSubscriptions = _organizationRepo.GetOrganizationSubscriptions(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.OrganizationId = organizationId;
                ViewBag.Controller = "Subscriptions";
                organizationSubscriptions = _organizationRepo.GetOrganizationSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationSubscriptionList", organizationSubscriptions);
            }
            return View(organizationSubscriptions);
        }

        public ActionResult CreateOrganizationSubscription(int organizationId, int subscriptionId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var organizationSubscriptionRel = new OrganizationSubscriptionRel()
            {
                SubscriptionId = subscriptionId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                organizationSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                organizationSubscriptionRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationSubscription", organizationSubscriptionRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateOrganizationSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,SubscriptionId")] OrganizationSubscriptionRel organizationSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationSubscriptionRel.Organization == null)
                {
                    organizationSubscriptionRel.Subscription = null;
                    _organizationRepo.InsertOrUpdateOrganizationSubscription(organizationSubscriptionRel);
                    _organizationRepo.Save();
                    //return RedirectToAction("Details", "Organizations", new { id = organizationSubscriptionRel.OrganizationId });
                    return null;
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationSubscriptionRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationSubscription(organizationSubscriptionRel);
                _organizationRepo.Save();
                //return RedirectToAction("Details", "Subscription", new { id = organizationSubscriptionRel.SubscriptionId });
                return null;
            }
            return View();

        }

        public ActionResult EditOrganizationSubscription(int id, string source)
        {
            var organizationSubscriptionRel = _organizationRepo.GetOrganizationSubscription(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = organizationSubscriptionRel.OrganizationId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = organizationSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationSubscription", organizationSubscriptionRel);
            }
            return View(organizationSubscriptionRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditOrganizationSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,OrganizationId,SubscriptionId")] OrganizationSubscriptionRel organizationSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                if (organizationSubscriptionRel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    organizationSubscriptionRel.Subscription = null;
                    _organizationRepo.InsertOrUpdateOrganizationSubscription(organizationSubscriptionRel);
                    _organizationRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = organizationSubscriptionRel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                organizationSubscriptionRel.Organization = null;
                _organizationRepo.InsertOrUpdateOrganizationSubscription(organizationSubscriptionRel);
                _organizationRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = organizationSubscriptionRel.SubscriptionId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteOrganizationSubscription(int id)
        {
            _organizationRepo.DeleteOrganizationSubscription(id);
            _organizationRepo.Save();

        }

        #endregion


        #region Status History

        public ActionResult GetOrganizationStatusHistories(int id)
        {
            var entities = _organizationRepo.GetOrganizationStatusHistories(p => p.OrganizationId.Equals(id)).ToArray().OrderBy(c => c.ActiveYear);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationStatusHistoryList", entities);
            }
            return View("Index");
        }

        #endregion

    }
}


