using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class PersonsController : BaseController
    {
        private readonly IPersonRepository _personRepo;
        private readonly IBeholderPersonRepository _beholderPersonRepo;
        private readonly ILookupRepository _lookupRepo;
        private readonly int _newApprovalStatusId;

        public PersonsController(
                IPersonRepository personRepo,
                IBeholderPersonRepository beholderPersonRepo,
                ILookupRepository lookupRepo)
        {

            _personRepo = personRepo;
            _beholderPersonRepo = beholderPersonRepo;
            _lookupRepo = lookupRepo;

            ViewBag.PossiblePrefixes = _lookupRepo.GetPrefixes();
            ViewBag.PossibleSuffixes = _lookupRepo.GetSuffixes();
            ViewBag.PossibleGenders = _lookupRepo.GetGenders();
            ViewBag.PossibleRaces = _lookupRepo.GetRaces();
            ViewBag.PossibleLicenseTypes = _lookupRepo.GetLicenseTypes();
            ViewBag.PossibleEyeColors = _lookupRepo.GetEyeColors();
            ViewBag.PossibleHairColors = _lookupRepo.GetHairColors();
            ViewBag.PossibleHairPatterns = _lookupRepo.GetHairPatterns();
            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleMaritialStatus = _lookupRepo.GetMaritialStatuses();
            ViewBag.PossibleAddressTypes = _lookupRepo.GetAddressTypes();
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            //ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            ViewBag.PossibleRemovalStatus = _lookupRepo.GetRemovalStatus();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();

            //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes();

            _newApprovalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;

        }

        #region Base Entity

        public JsonResult GetPersonSelectList(string term)
        {
            if (term == null)
            {
                term = Request.Params["filter[filters][0][value]"].Trim();
            }

            if (string.IsNullOrWhiteSpace(term) || term == "-1")
            {
                var list = _personRepo.Get(CurrentUser, null).Take(50).OrderBy(x => x.CommonPerson.LName).ThenBy(x => x.CommonPerson.FName)
                                    .Select(x => new
                                    {
                                        x.Id,
                                        Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                        x.CommonPerson.DOB,
                                        MovementClass = x.MovementClass != null ? x.MovementClass.Name : "Unknown",
                                        x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.City,
                                        State = x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.State.StateCode
                                    });


                var items = list.ToList().Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.City,
                    x.State,
                    DOB = x.DOB != null ? Convert.ToDateTime(x.DOB).ToShortDateString() : "Unknown",
                    Movement = x.MovementClass,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            if (term.IsInt())
            {
                var id = Convert.ToInt32(term);
                var list = _personRepo.Get(CurrentUser, p => p.Id == id)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                            x.CommonPerson.DOB,
                                            MovementClass = x.MovementClass != null ? x.MovementClass.Name : "Unknown",
                                            x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.City,
                                            State = x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.State.StateCode
                                        });

                var items = list.ToList().Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.City,
                    x.State,
                    DOB = x.DOB != null ? Convert.ToDateTime(x.DOB).ToShortDateString() : "Unknown",
                    Movement = x.MovementClass,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                term = term.Trim();
                var list = _personRepo.Get(CurrentUser, p => p.CommonPerson.LName.Contains(term) || p.CommonPerson.FName.Contains(term)).OrderBy(x => x.CommonPerson.LName).ThenBy(x => x.CommonPerson.FName)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                            x.CommonPerson.DOB,
                                            MovementClass = x.MovementClass != null ? x.MovementClass.Name : "Unknown",
                                            x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.City,
                                            State = x.CommonPerson.AddressPersonRels.FirstOrDefault().Address.State.StateCode
                                        });

                var items = list.ToList().Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.City,
                    x.State,
                    DOB = x.DOB != null ? Convert.ToDateTime(x.DOB).ToShortDateString() : "Unknown",
                    Movement = x.MovementClass,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Search for BeholderPersons 
        public JsonResult GetDropdown(string term)
        {
            //TODO: Why is there a need for 2 personrepo
            term = term.Trim();
            var list = _beholderPersonRepo.GetDropdown(p => p.label.Contains(term)).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCommonPersonList(string term)
        {
            //TODO: Why is there a need for 2 personrepo
            term = term.Trim();
            var list = _personRepo.GetCommonPersons(p => p.LName.Contains(term) || p.FName.Contains(term)).ToArray().Select(
                    e => new
                    {
                        e.Id,
                        label = e.FullName
                    });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //TODO: Added to replace GetDropDown for Person
        // GET: Search for BeholderPersons 
        public JsonResult GetPersonList(string term)
        {
            //TODO: Why is there a need for 2 personrepo
            term = term.Trim();
            var list = _beholderPersonRepo.Get(CurrentUser, p => p.CommonPerson.LName.Contains(term) || p.CommonPerson.FName.Contains(term)).ToArray().Select(
                e => new
                {
                    e.Id,
                    label = e.CommonPerson.FullName
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /BeholderPersons/
        public ActionResult Index(List<int> movementclassid = null, string movementclassid_string = "", List<int> stateid = null, string stateid_string = "", string fname = "", string lname = "", string alias = "", string location = "", int? page = 1, int? pageSize = 15)
        {
            if (!string.IsNullOrWhiteSpace(movementclassid_string))
            {
                movementclassid = movementclassid_string.Split(',').Select(int.Parse).ToList();
            }

            if (!string.IsNullOrWhiteSpace(stateid_string))
            {
                stateid = stateid_string.Split(',').Select(int.Parse).ToList();
            }

            fname = fname.Trim();
            lname = lname.Trim();
            alias = alias.Trim();
            location = location.Trim();

            Session["stateid"] = stateid;
            Session["movementclassid"] = movementclassid;
            Session["fname"] = fname;
            Session["lname"] = lname;
            Session["alias"] = alias;
            Session["location"] = location;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            var pred = PredicateBuilder.True<BeholderPerson>();
            if (movementclassid != null) pred = pred.And(p => movementclassid.Contains((int)p.MovementClassId));
            if (!string.IsNullOrWhiteSpace(fname)) pred = pred.And(p => p.CommonPerson.FName.Contains(fname));
            if (!string.IsNullOrWhiteSpace(lname)) pred = pred.And(p => p.CommonPerson.LName.Contains(lname));
            if (!string.IsNullOrWhiteSpace(alias)) pred = pred.And(p => p.CommonPerson.AliasPersonRels.Any(c => c.Alias.AliasName.Contains(alias)));
            if (!string.IsNullOrWhiteSpace(location)) pred = pred.And(p => p.CommonPerson.AddressPersonRels.Any(c => c.Address.City.Contains(location)));
            if (stateid != null) pred = pred.And(p => p.CommonPerson.AddressPersonRels.Any(c => stateid.Contains((int)c.Address.StateId)));

            var list = _personRepo.Get(CurrentUser, pred).OrderBy(m => m.CommonPerson.LName)
                .ThenBy(n => n.CommonPerson.FName)
                .ToPagedList(page ?? 1, pageSize ?? 15);

            return View("Index", list);
        }

        //
        // GET: /BeholderPersons/Details/5
        public ViewResult DetailsLite(int id)
        {
            //ViewBag.BeholderPersonId = id; 
            var person = _personRepo.Get(id, CurrentUser);

            return person != null ? View(person) : View("Person404");
        }

        //
        // GET: /BeholderPersons/Details/5
        public ViewResult Details(int id)
        {
            var person = _personRepo.Get(id, CurrentUser);
            if (person == null) return View("Person404");

            ViewBag.BeholderPersonId = id;
            ViewBag.PersonId = person.Id;
            ViewBag.CommonPersonId = person.CommonPerson.Id;
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.EventId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.ContactId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.Controller = "Persons";

            return View(person);
        }

        //
        // GET: /Persons/Create
        public ActionResult Create()
        {
            var bperson = new BeholderPerson();
            var cperson = new CommonPerson();
            bperson.CommonPerson = cperson;
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            return View(bperson);
        }

        //
        // POST: /Persons/Create
        [HttpPost]
        public ActionResult Create(BeholderPerson beholderperson)
        {
            if (ModelState.IsValid)
            {
                _personRepo.InsertOrUpdate(beholderperson);
                _personRepo.Save();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { url = Url.Action("Details", new { id = beholderperson.Id }) });
                }
                return RedirectToAction("Details", new { id = beholderperson.Id });
            }
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            return View(beholderperson);
        }

        //
        // GET: /BeholderPersons/Edit/5
        public ActionResult Edit(int id)
        {
            var person = _personRepo.Get(id, CurrentUser);
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            if (person != null)
            {
                return View(person);
            }
            return View("Person404");

        }

        //
        // POST: /Persons/Edit/5
        [HttpPost]
        public ActionResult Edit(BeholderPerson beholderperson)
        {
            if (ModelState.IsValid)
            {
                //beholderperson.Modifiedpublic int = currentUser.Id;
                _personRepo.InsertOrUpdate(beholderperson);
                //try
                //{
                _personRepo.Save();
                //}
                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            var temp = string.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage);
                //            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            //    ve.PropertyName, ve.ErrorMessage);
                //        }
                //    }
                //    throw;
                //}
                //catch (DbUpdateException e)
                //{
                //    foreach (var eve in e.Entries)
                //    {
                //        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entity.GetType().Name, eve.State);
                //        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    }
                //    throw;
                //}
                if (Request.IsAjaxRequest())
                {
                    return Json(new { url = Url.Action("Details", new { id = beholderperson.Id }) });
                }
                return RedirectToAction("Details", new { id = beholderperson.Id });
            }
            return View(beholderperson);
        }

        public ActionResult RemovePerson(int id)
        {
            var person = _personRepo.Get(id, CurrentUser);
            return View(person);
        }

        [HttpPost]
        public ActionResult RemovePerson(int id, string removalreason)
        {
            var beholderperson = _personRepo.Get(id, CurrentUser);
            beholderperson.RemovalStatusId = 1;
            _personRepo.InsertOrUpdate(beholderperson);
            _personRepo.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var person = _personRepo.Get(id, CurrentUser);
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(BeholderPerson beholderperson)
        {

            if (ModelState.IsValid)
            {
                _personRepo.InsertOrUpdate(beholderperson);
                //_personRepo.Delete(beholderperson.Id);
                try
                {
                    //_personRepo.Save();
                    _personRepo.Save();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var temp = string.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage);
                            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //    ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                catch (DbUpdateException e)
                {
                    foreach (var eve in e.Entries)
                    {
                        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entity.GetType().Name, eve.State);
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    }
                    throw;
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(new { url = Url.Action("Details", new { id = beholderperson.Id }) });
                }
                return RedirectToAction("Details", new { id = beholderperson.Id });
            }
            return View();
        }

        public ImageResult ShowPrimaryImage(int id)
        {
            byte[] picture;
            string contentType;

            var mediaImage = _personRepo.GetPrimaryImage(id);
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

            //var image = _personRepo.GetPrimaryImage(id);
            //byte[] picture = ImageHelper.Decompress(image.MediaImage.Image.IMAGE1);
            //var contentType = image.MediaImage.Image.MimeType.Name;
            //return this.Image(picture, contentType);
        }

        #endregion

        #region MediaImages

        public ActionResult GetPersonMediaImages(int personId, int mediaImageId)
        {
            IQueryable<PersonMediaImageRel> personMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personMediaImages = _personRepo.GetImages(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "MediaImages";
                personMediaImages = _personRepo.GetImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaImageList", personMediaImages);
            }
            return PartialView("_PersonMediaImageList", personMediaImages);
        }

        public ActionResult CreatePersonMediaImage(int personId, int mediaImageId)
        {
            var personMediaImageRel = new PersonMediaImageRel
            {
                MediaImageId = mediaImageId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                personMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personMediaImageRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaImage", personMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,PersonId")] PersonMediaImageRel personMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaImageRel.BeholderPerson == null)
                {
                    personMediaImageRel.MediaImage = null;
                    _personRepo.InsertOrUpdateImage(personMediaImageRel);
                    _personRepo.Save();
                    //return RedirectToAction("Details", "Persons", new { id = personMediaImageRel.PersonId });
                    return null;
                }

                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaImageRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateImage(personMediaImageRel);
                _personRepo.Save();
                //return RedirectToAction("Details", "MediaImage", new { id = personMediaImageRel.MediaImageId });
                return null;
            }
            return View();

        }

        public ActionResult EditPersonMediaImage(int id, string source)
        {
            var personMediaImageRel = _personRepo.GetImage(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = personMediaImageRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = personMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaImage", personMediaImageRel);
            }
            return View(personMediaImageRel);

        }

        [HttpPost]
        public ActionResult EditPersonMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,PersonId,MediaImageId")] PersonMediaImageRel personMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaImageRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personMediaImageRel.MediaImage = null;
                    _personRepo.InsertOrUpdateImage(personMediaImageRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaImageRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaImageRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateImage(personMediaImageRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = personMediaImageRel.MediaImageId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonMediaImage(int id)
        {
            _personRepo.DeleteImage(id);
            _personRepo.Save();
        }

        #endregion

        #region Media Audio Video

        public ActionResult GetPersonMediaAudioVideos(int personId, int mediaAudioVideoId)
        {
            IQueryable<PersonMediaAudioVideoRel> personMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                //TODO: LookupRepo GetRelationshipTypes with To/From signaature, not sure what you mean(slj)
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personMediaAudioVideos = _personRepo.GetAudioVideos(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "MediaAudioVideos";
                personMediaAudioVideos = _personRepo.GetAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaAudioVideoList", personMediaAudioVideos);
            }
            return PartialView("_PersonMediaAudioVideoList", personMediaAudioVideos);
        }

        public ActionResult CreatePersonMediaAudioVideo(int personId, int mediaAudioVideoId)
        {

            var personMediaAudioVideoRel = new PersonMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                personMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personMediaAudioVideoRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaAudioVideo", personMediaAudioVideoRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaAudioVideoId,PersonId")] PersonMediaAudioVideoRel personMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaAudioVideoRel.BeholderPerson == null)
                {
                    personMediaAudioVideoRel.MediaAudioVideo = null;
                    _personRepo.InsertOrUpdateAudioVideo(personMediaAudioVideoRel);
                    _personRepo.Save();
                    return null;
                    //return RedirectToAction("Details", "Persons", new { id = personMediaAudioVideoRel.PersonId });
                }

                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaAudioVideoRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateAudioVideo(personMediaAudioVideoRel);
                _personRepo.Save();
                return null;
                //return RedirectToAction("Details", "MediaAudioVideos", new { id = personMediaAudioVideoRel.MediaAudioVideoId });
            }
            return View();

        }

        public ActionResult EditPersonMediaAudioVideo(int id, string source)
        {
            var personMediaAudioVideoRel = _personRepo.GetAudioVideo(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = personMediaAudioVideoRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = personMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaAudioVideo", personMediaAudioVideoRel);
            }
            return View(personMediaAudioVideoRel);

        }

        [HttpPost]
        public ActionResult EditPersonMediaAudioVideo(PersonMediaAudioVideoRel personMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaAudioVideoRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personMediaAudioVideoRel.MediaAudioVideo = null;
                    _personRepo.InsertOrUpdateAudioVideo(personMediaAudioVideoRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaAudioVideoRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaAudioVideoRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateAudioVideo(personMediaAudioVideoRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = personMediaAudioVideoRel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonMediaAudioVideo(int id)
        {
            _personRepo.DeleteAudioVideo(id);
            _personRepo.Save();

        }

        #endregion

        #region Events

        public ActionResult GetPersonEvents(int personId, int eventId)
        {

            IQueryable<PersonEventRel> personEvents;
            if (eventId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personEvents = _personRepo.GetEvents(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Events";
                personEvents = _personRepo.GetEvents(p => p.Event.Id.Equals(eventId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonEventList", personEvents);
            }
            return PartialView("_PersonEventList", personEvents);

        }

        public ActionResult CreatePersonEvent(int personId, int eventId)
        {
            var personEventRel = new PersonEventRel
            {
                EventId = eventId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (eventId == -1)
            {
                personEventRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personEventRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonEvent", personEventRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,PersonId")] PersonEventRel personEventRel)
        {
            if (ModelState.IsValid)
            {
                if (personEventRel.BeholderPerson == null)
                {
                    //TODO: Why does the event need to be set to null
                    personEventRel.Event = null;
                    _personRepo.InsertOrUpdateEvent(personEventRel);
                    _personRepo.Save();
                    //return RedirectToAction("Details", "Persons", new { id = personEventRel.PersonId });
                    return null;
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personEventRel.BeholderPerson = null;
                //TODO: Why does the person need to be set to null
                _personRepo.InsertOrUpdateEvent(personEventRel);
                _personRepo.Save();
                return null;
                //return RedirectToAction("Details", "Event", new { id = personEventRel.EventId });
            }
            return View(personEventRel);

        }

        public ActionResult EditPersonEvent(int id, string source)
        {
            var personEventRel = _personRepo.GetPersonEvent(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = personEventRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = personEventRel.EventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonEvent", personEventRel);
            }
            return View(personEventRel);

        }

        [HttpPost]
        public ActionResult EditPersonEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,PersonId")] PersonEventRel personEventRel)
        {
            if (ModelState.IsValid)
            {
                if (personEventRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personEventRel.Event = null;
                    _personRepo.InsertOrUpdateEvent(personEventRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personEventRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personEventRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateEvent(personEventRel);
                _personRepo.Save();
                return RedirectToAction("Details", "Events", new { id = personEventRel.EventId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonEvent(int id)
        {
            _personRepo.DeleteEvent(id);
            _personRepo.Save();
        }

        #endregion

        #region Contacts

        public ActionResult GetPersonContacts(int personId, int contactId)
        {
            IQueryable<PersonContactRel> personContacts;
            if (contactId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.ContactId = contactId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personContacts = _personRepo.GetContacts(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.ContactId = contactId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Contacts";
                personContacts = _personRepo.GetContacts(p => p.Contact.Id.Equals(contactId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonContactList", personContacts);
            }
            return PartialView("_PersonContactList", personContacts);
        }

        public ActionResult CreatePersonContact(int personId, int contactId)
        {
            var personContactRel = new PersonContactRel
            {
                ContactId = contactId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (contactId == -1)
            {
                personContactRel.Contact = new Contact();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personContactRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Contacts";
                ViewBag.ContactId = contactId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonContact", personContactRel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreatePersonContact([Bind(Include = "Id,RelationshipTypeId,PrimaryStatusId,DateStart,DateEnd,PersonId,ContactId")] PersonContactRel personContactRel)
        {
            if (ModelState.IsValid)
            {
                //if (personContactRel.BeholderPerson == null)
                //{
                //    //personContactRel.Contact = null;
                //    _personRepo.InsertOrUpdateContact(personContactRel);
                //    _personRepo.Save();
                //    return RedirectToAction("Details", "Persons", new { id = personContactRel.PersonId });
                //}
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //personContactRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateContact(personContactRel);
                _personRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public ActionResult EditPersonContact(int id, string source)
        {
            var personContactRel = _personRepo.GetPersonContact(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = personContactRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = personContactRel.ContactId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonContact", personContactRel);
            }
            return View(personContactRel);

        }

        [HttpPost]
        public ActionResult EditPersonContact([Bind(Include = "Id,RelationshipTypeId,PrimaryStatusId,DateStart,DateEnd,PersonId,ContactId")] PersonContactRel personContactRel)
        {
            if (ModelState.IsValid)
            {
                //if (personContactRel.BeholderPerson == null)
                //{
                //    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                //    personContactRel.Contact = null;
                //    _personRepo.InsertOrUpdateContact(personContactRel);
                //    _personRepo.Save();
                //    return RedirectToAction("Details", "Persons", new { id = personContactRel.PersonId });
                //}
                ////reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //personContactRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateContact(personContactRel);
                _personRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return PartialView("_CreateOrEditPersonContact", personContactRel);
        }

        [HttpPost]
        public void DeletePersonContact(int id)
        {
            _personRepo.DeleteContact(id);
            _personRepo.Save();
        }

        #endregion

        #region Vehicles

        public ActionResult GetPersonVehicles(int personId, int vehicleId)
        {
            IQueryable<PersonVehicleRel> personVehicles;

            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personVehicles = _personRepo.GetVehicles(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Vehicles";
                personVehicles = _personRepo.GetVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonVehicleList", personVehicles);
            }
            return PartialView("_PersonVehicleList", personVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreatePersonVehicle(int vehicleId, int personId)
        {
            var personVehicleRel = new PersonVehicleRel
            {
                VehicleId = vehicleId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                personVehicleRel.Vehicle = new Vehicle();
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personVehicleRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonVehicle", personVehicleRel);
            }

            return View();

        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreatePersonVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,VehicleId,PersonId")] PersonVehicleRel personvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (personvehiclerel.BeholderPerson == null)
                {
                    personvehiclerel.Vehicle = null;
                    _personRepo.InsertOrUpdateVehicle(personvehiclerel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personvehiclerel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personvehiclerel.BeholderPerson = null;
                _personRepo.InsertOrUpdateVehicle(personvehiclerel);
                _personRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = personvehiclerel.VehicleId });
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditPersonVehicle(int id, string source)
        {
            var personVehicleRel = _personRepo.GetVehicle(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = personVehicleRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = personVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonVehicle", personVehicleRel);
            }
            return View(personVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditPersonVehicle(PersonVehicleRel personvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (personvehiclerel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personvehiclerel.Vehicle = null;
                    _personRepo.InsertOrUpdateVehicle(personvehiclerel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personvehiclerel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personvehiclerel.BeholderPerson = null;
                _personRepo.InsertOrUpdateVehicle(personvehiclerel);
                _personRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = personvehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonVehicle(int id)
        {
            _personRepo.DeleteVehicle(id);
            _personRepo.Save();
        }

        #endregion

        #region Persons

        public ActionResult GetPersonPersons(int personId)
        {
            BeholderPerson persons;

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
            ViewBag.PersonId = personId;
            ViewBag.Person2Id = personId;
            ViewBag.Controller = "Persons";
            try
            {
                persons = _personRepo.Get(personId, CurrentUser);
            }
            catch (Exception ex)
            {
                return View("Person404");
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonPersonList", persons);
            }
            return PartialView("_PersonPersonList", persons);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreatePersonPerson(int personId)
        {
            var personPersonRel = new PersonPersonRel
            {
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now
                //BeholderPerson2 = new BeholderPerson(),
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
            ViewBag.Controller = "Persons";
            return PartialView("_CreateOrEditPersonPerson", personPersonRel);

        }

        [HttpPost]
        public ActionResult CreatePersonPerson([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,Person2Id,PersonId")] PersonPersonRel personpersonrel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            personpersonrel.BeholderPerson2 = null;
            _personRepo.InsertOrUpdatePersonPerson(personpersonrel);
            _personRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditPersonPerson(int id)
        {
            var personPersonRel = _personRepo.GetPersonPerson(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
            return PartialView("_CreateOrEditPersonPerson", personPersonRel);
        }

        [HttpPost]
        public ActionResult EditPersonPerson([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,Person2Id,PersonId")] PersonPersonRel personpersonrel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _personRepo.InsertOrUpdatePersonPerson(personpersonrel);
            _personRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public void DeletePersonPerson(int id)
        {
            _personRepo.DeletePersonPerson(id);
            _personRepo.Save();
        }

        #endregion

        #region ScreenNames

        //
        // GET: /PersonScreenNames/
        public ActionResult GetPersonScreenNames(int id)
        {
            ViewBag.BeholderPersonId = id;
            ViewBag.PersonId = id;
            var screenNames = _personRepo.GetScreenNames(p => p.BeholderPersonId.Equals(id)).ToArray();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonScreenNameList", screenNames);
            }
            return PartialView("_PersonScreenNameList", _personRepo.GetScreenNames(p => p.BeholderPersonId.Equals(id)).ToArray());
        }

        //
        // GET: /PersonScreenNames/Create
        public ActionResult CreatePersonScreenName(int personId)
        {
            var screenName = new PersonScreenName
            {
                BeholderPersonId = personId
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonScreenName", screenName);
            }
            return View(screenName);
        }

        //
        // POST: /PersonScreenNames/Create
        [HttpPost]
        public ActionResult CreatePersonScreenName(PersonScreenName personscreenname)
        {
            ViewBag.PersonId = personscreenname.BeholderPersonId;
            if (ModelState.IsValid)
            {
                _personRepo.InsertOrUpdateScreenName(personscreenname);
                _personRepo.Save();

                //                return RedirectToAction("Details", "Persons", new { id = personscreenname.BeholderPersonId });
            }
            return View(personscreenname);
        }

        //
        // GET: /PersonScreenNames/Edit/5
        public ActionResult EditPersonScreenName(int id)
        {
            var screenName = _personRepo.GetScreenName(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonScreenName", screenName);
            }
            return View(screenName);
        }

        //
        // POST: /PersonScreenNames/Edit/5
        [HttpPost]
        public ActionResult EditPersonScreenName(PersonScreenName personscreenname)
        {
            ViewBag.PersonId = personscreenname.BeholderPersonId;
            if (ModelState.IsValid)
            {
                _personRepo.InsertOrUpdateScreenName(personscreenname);
                _personRepo.Save();
                //                return RedirectToAction("Details", "Persons", new { id = personscreenname.BeholderPersonId });
            }
            return View(personscreenname);
        }

        //
        // POST: /PersonScreenNames/Delete/5
        [HttpPost]
        public void DeletePersonScreenName(int id)
        {
            _personRepo.DeleteScreenName(id);
            _personRepo.Save();
        }

        #endregion

        #region Media Correspondence

        public ActionResult GetPersonMediaCorrespondences(int personId, int mediaCorrespondenceId)
        {
            IQueryable<PersonMediaCorrespondenceRel> personMediaCorrespondences;
            if (mediaCorrespondenceId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personMediaCorrespondences = _personRepo.GetCorrespondences(p => p.BeholderPerson.Id.Equals(personId));
                //personMediaCorrespondences = _personmediaaudiovideorelRepository.GetPersonMediaCorrespondences(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "MediaCorrespondences";
                personMediaCorrespondences = _personRepo.GetCorrespondences(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaCorrespondenceList", personMediaCorrespondences);
            }
            return PartialView("_PersonMediaCorrespondenceList", personMediaCorrespondences);
        }

        public ActionResult CreatePersonMediaCorrespondence(int personId, int mediaCorrespondenceId)
        {
            var personMediaCorrespondenceRel = new PersonMediaCorrespondenceRel
            {
                MediaCorrespondenceId = mediaCorrespondenceId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaCorrespondenceId == -1)
            {
                personMediaCorrespondenceRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personMediaCorrespondenceRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaCorrespondence", personMediaCorrespondenceRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,BeholderPersonId,PersonId")] PersonMediaCorrespondenceRel personMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaCorrespondenceRel.BeholderPerson == null)
                {
                    personMediaCorrespondenceRel.MediaCorrespondence = null;
                    _personRepo.InsertOrUpdateCorrespondence(personMediaCorrespondenceRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaCorrespondenceRel.PersonId });
                }

                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaCorrespondenceRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateCorrespondence(personMediaCorrespondenceRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = personMediaCorrespondenceRel.MediaCorrespondenceId });
            }
            return View();

        }

        public ActionResult EditPersonMediaCorrespondence(int id, string source)
        {
            var personMediaCorrespondenceRel = _personRepo.GetCorrespondence(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = personMediaCorrespondenceRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaCorrespondenceId = personMediaCorrespondenceRel.MediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaCorrespondence", personMediaCorrespondenceRel);
            }
            return View(personMediaCorrespondenceRel);

        }

        [HttpPost]
        public ActionResult EditPersonMediaCorrespondence(PersonMediaCorrespondenceRel personMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaCorrespondenceRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personMediaCorrespondenceRel.MediaCorrespondence = null;
                    _personRepo.InsertOrUpdateCorrespondence(personMediaCorrespondenceRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaCorrespondenceRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaCorrespondenceRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateCorrespondence(personMediaCorrespondenceRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = personMediaCorrespondenceRel.MediaCorrespondenceId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonMediaCorrespondence(int id)
        {
            _personRepo.DeleteCorrespondence(id);
            _personRepo.Save();

        }

        #endregion

        #region Media Published

        public ActionResult GetPersonMediaPublisheds(int personId, int mediaPublishedId)
        {
            IQueryable<PersonMediaPublishedRel> personMediaPublisheds;
            if (mediaPublishedId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personMediaPublisheds = _personRepo.GetPublisheds(p => p.BeholderPerson.Id.Equals(personId)).OrderByDescending(x => x.MediaPublished.DateModified);
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "MediaPublisheds";
                personMediaPublisheds = _personRepo.GetPublisheds(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaPublishedList", personMediaPublisheds);
            }
            return PartialView("_PersonMediaPublishedList", personMediaPublisheds);
        }

        public ActionResult CreatePersonMediaPublished(int personId, int mediaPublishedId)
        {
            var personMediaPublishedRel = new PersonMediaPublishedRel
            {
                MediaPublishedId = mediaPublishedId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaPublishedId == -1)
            {
                personMediaPublishedRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personMediaPublishedRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaPublished", personMediaPublishedRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,BeholderPersonId,PersonId")] PersonMediaPublishedRel personMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaPublishedRel.BeholderPerson == null)
                {
                    personMediaPublishedRel.MediaPublished = null;
                    _personRepo.InsertOrUpdatePublished(personMediaPublishedRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaPublishedRel.PersonId });
                }

                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaPublishedRel.BeholderPerson = null;
                _personRepo.InsertOrUpdatePublished(personMediaPublishedRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = personMediaPublishedRel.MediaPublishedId });
            }
            return View();

        }

        public ActionResult EditPersonMediaPublished(int id, string source)
        {
            var personMediaPublishedRel = _personRepo.GetPublished(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = personMediaPublishedRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaPublishedId = personMediaPublishedRel.MediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaPublished", personMediaPublishedRel);
            }
            return View(personMediaPublishedRel);

        }

        [HttpPost]
        public ActionResult EditPersonMediaPublished(PersonMediaPublishedRel personMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaPublishedRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personMediaPublishedRel.MediaPublished = null;
                    _personRepo.InsertOrUpdatePublished(personMediaPublishedRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaPublishedRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaPublishedRel.BeholderPerson = null;
                _personRepo.InsertOrUpdatePublished(personMediaPublishedRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = personMediaPublishedRel.MediaPublishedId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonMediaPublished(int id)
        {
            _personRepo.DeletePublished(id);
            _personRepo.Save();

        }

        #endregion

        #region Website/EGroup

        public ActionResult GetPersonMediaWebsiteEGroups(int personId, int mediaWebsiteEGroupId)
        {
            IQueryable<PersonMediaWebsiteEGroupRel> personMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "Persons";
                personMediaWebsiteEGroups = _personRepo.GetWebsiteEGroups(p => p.BeholderPerson.Id.Equals(personId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.PersonId = personId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                personMediaWebsiteEGroups = _personRepo.GetWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaWebsiteEGroupList", personMediaWebsiteEGroups);
            }
            return PartialView("_PersonMediaWebsiteEGroupList", personMediaWebsiteEGroups);
        }

        public ActionResult CreatePersonMediaWebsiteEGroup(int personId, int mediaWebsiteEGroupId)
        {

            var personMediaWebsiteEGroupRel = new PersonMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                PersonId = personId,
                ApprovalStatusId = _newApprovalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                personMediaWebsiteEGroupRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = personId;
            }
            else
            {
                personMediaWebsiteEGroupRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaWebsiteEGroup", personMediaWebsiteEGroupRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreatePersonMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,PersonId")] PersonMediaWebsiteEGroupRel personMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                _personRepo.InsertOrUpdateWebsiteEGroup(personMediaWebsiteEGroupRel);
                _personRepo.Save();
                return null;
            }
            if (personMediaWebsiteEGroupRel.MediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
            }
            return View(personMediaWebsiteEGroupRel);

        }

        public ActionResult EditPersonMediaWebsiteEGroup(int id, string source)
        {
            var personMediaWebsiteEGroupRel = _personRepo.GetWebsiteEGroup(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = personMediaWebsiteEGroupRel.PersonId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaWebsiteEGroupId = personMediaWebsiteEGroupRel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonMediaWebsiteEGroup", personMediaWebsiteEGroupRel);
            }
            return View(personMediaWebsiteEGroupRel);

        }

        [HttpPost]
        public ActionResult EditPersonMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,PersonId")] PersonMediaWebsiteEGroupRel personMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (personMediaWebsiteEGroupRel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    personMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _personRepo.InsertOrUpdateWebsiteEGroup(personMediaWebsiteEGroupRel);
                    _personRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = personMediaWebsiteEGroupRel.PersonId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                personMediaWebsiteEGroupRel.BeholderPerson = null;
                _personRepo.InsertOrUpdateWebsiteEGroup(personMediaWebsiteEGroupRel);
                _personRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = personMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeletePersonMediaWebsiteEGroup(int id)
        {
            _personRepo.DeleteWebsiteEGroup(id);
            _personRepo.Save();

        }

        #endregion

    }
}

