using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.WebPages;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class ContactsController : BaseController
    {
        private readonly ACDBContext _ctx;

        private readonly IContactRepository _contactRepo;
        private readonly ILookupRepository _lookupRepo;

        public ContactsController(
            IContactRepository contactRepo,
            ILookupRepository lookupRepo,
            ACDBContext ctx)
        {
            _ctx = ctx;
            _contactRepo = contactRepo;
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
            ViewBag.PossibleRemovalStatus = _lookupRepo.GetRemovalStatus();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleContactType = _lookupRepo.GetContactTypes();
            ViewBag.PossibleContactTopic = _lookupRepo.GetContactTopics();

        }

        #region Contacts

        public JsonResult GetContactSelectList(string term)
        {
            if (term == null)
            {
                term = Request.Params["filter[filters][0][value]"].Trim();
            }
            if (string.IsNullOrWhiteSpace(term) || term == "-1")
            {
                var list = _contactRepo.Get(CurrentUser, null).Take(50).OrderBy(x => x.CommonPerson.LName).ThenBy(x => x.CommonPerson.FName)
                                    .Select(x => new
                                    {
                                        x.Id,
                                        Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                        x.CommonPerson.DOB,
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
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            if (term.IsInt())
            {
                var id = Convert.ToInt32(term);
                var list = _contactRepo.Get(CurrentUser, p => p.Id == id)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                            x.CommonPerson.DOB,
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
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                term = term.Trim();
                var list = _contactRepo.Get(CurrentUser, p => p.CommonPerson.LName.Contains(term) || p.CommonPerson.FName.Contains(term)).OrderBy(x => x.CommonPerson.LName).ThenBy(x => x.CommonPerson.FName)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            Name = x.CommonPerson.LName + ", " + x.CommonPerson.FName,
                                            x.CommonPerson.DOB,
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
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetContactList(string term)
        {
            //var _contactRepo = new ContactRepository();
            var list = _contactRepo.GetContacts(CurrentUser, p => p.CommonPerson.LName.Contains(term.Trim()) || p.CommonPerson.FName.Contains(term.Trim())).ToArray().Select(
                e => new
                {
                    e.Id,
                    label = e.CommonPerson.FullName
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string fname = "", string lname = "", string location = "", List<int> stateid = null, string stateid_string = "", int? page = 1, int? pageSize = 15)
        {
            //Store page in session to allow returning back from details action link
            if (!string.IsNullOrWhiteSpace(stateid_string))
            {
                stateid = stateid_string.Split(',').Select(int.Parse).ToList();
            }
            fname = fname.Trim();
            lname = lname.Trim();
            location = location.Trim();

            Session["stateid"] = stateid;
            Session["page"] = page;
            Session["pageSize"] = pageSize;
            Session["fname"] = fname;
            Session["lname"] = lname;
            Session["location"] = location;

            var pred = PredicateBuilder.True<Contact>();

            if (stateid != null) pred = pred.And(p => p.AddressContactRels.Any(c => stateid.Contains((int)c.Address.StateId)));
            if (!string.IsNullOrWhiteSpace(fname)) pred = pred.And(p => p.CommonPerson.FName.Contains(fname));
            if (!string.IsNullOrWhiteSpace(lname)) pred = pred.And(p => p.CommonPerson.LName.Contains(lname));
            if (!string.IsNullOrWhiteSpace(location)) pred = pred.And(p => p.AddressContactRels.Any(c => c.Address.City.Contains(location)));

            var list = _contactRepo.GetContacts(CurrentUser, pred).OrderBy(m => m.CommonPerson.LName).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactList", list);
            }
            return View("Index", list);

        }

        public ViewResult Details(int id)
        {
            var contact = _contactRepo.Find(CurrentUser, id);
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.ContactId = id;
            ViewBag.CommonPersonId = contact.CommonPerson.Id;
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.MediaPublishedId = -1;
            ViewBag.Controller = "Contacts";
            return View(_contactRepo.Find(CurrentUser, id));
        }

        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var contact = new Contact();
            var cperson = new CommonPerson();
            contact.CommonPerson = cperson;
            return View(contact);
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepo.InsertOrUpdate(contact);
                _contactRepo.Save();
                return RedirectToAction("Details", new { id = contact.Id });
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var contact = _contactRepo.Find(CurrentUser, id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        //public ActionResult Edit([Bind(Include = "Id,FName,MName,LName,CommonPersonId")] Contact contact)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            if (ModelState.IsValid)
            {
                _contactRepo.InsertOrUpdate(contact);
                _contactRepo.Save();
                return RedirectToAction("Details", new { id = contact.Id });
            }
            return View();
        }

        public ActionResult RemoveContact(int id)
        {
            var contact = _contactRepo.Find(CurrentUser, id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult RemoveContact(int id, string removalreason)
        {
            var contact = _contactRepo.Find(CurrentUser, id);
            contact.RemovalReason = removalreason;

            contact.RemovalStatusId = 1;
            _contactRepo.InsertOrUpdate(contact);
            _contactRepo.Save();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public void Delete(int id)
        {
            _contactRepo.Delete(id);
            _contactRepo.Save();
        }

        #endregion


        #region MediaImage

        public ImageResult ShowPrimaryImage(int id)
        {
            byte[] picture;
            string contentType = null;

            var mediaImage = _contactRepo.GetPrimaryImage(id);
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

            //var image = _contactRepo.GetPrimaryImage(id);
            //byte[] picture = ImageHelper.Decompress(image.MediaImage.Image.IMAGE1);
            //var contentType = image.MediaImage.Image.MimeType.Name;
            //return this.Image(picture, contentType);
        }

        public ActionResult GetContactMediaImages(int contactId, int mediaImageId)
        {
            IQueryable<ContactMediaImageRel> contactMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.ContactId = contactId;
                ViewBag.Controller = "Contacts";
                contactMediaImages = _contactRepo.GetContactMediaImages(p => p.Contact.Id.Equals(contactId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.ContactId = contactId;
                ViewBag.Controller = "MediaImages";
                contactMediaImages = _contactRepo.GetContactMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactMediaImageList", contactMediaImages);
            }
            return View(contactMediaImages);
        }

        public ActionResult CreateContactMediaImage(int contactId, int mediaImageId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var contactMediaImageRel = new ContactMediaImageRel()
            {
                MediaImageId = mediaImageId,
                ContactId = contactId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                contactMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Contacts";
            }
            else
            {
                contactMediaImageRel.Contact = new Contact();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
            }
            return PartialView("_CreateOrEditContactMediaImage", contactMediaImageRel);

        }

        [HttpPost]
        public ActionResult CreateContactMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ContactId,MediaImageId")] ContactMediaImageRel contactMediaImageRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _contactRepo.InsertOrUpdateContactImage(contactMediaImageRel);
            _contactRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditContactMediaImage(int id, string source)
        {
            var contactMediaImageRel = _contactRepo.GetImage(id);
            if (source == "Contacts")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
            }
            return PartialView("_CreateOrEditContactMediaImage", contactMediaImageRel);
        }


        [HttpPost]
        public ActionResult EditContactMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,ContactId,MediaImageId")] ContactMediaImageRel contactMediaImageRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _contactRepo.InsertOrUpdateContactImage(contactMediaImageRel);
            _contactRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public void DeleteContactMediaImage(int id)
        {
            _contactRepo.DeleteContactImage(id);
            _contactRepo.Save();
        }

        #endregion


        #region Contact Relationships

        public ActionResult GetContactContacts(int contactId)
        {
            Contact entities;

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
            ViewBag.ContactId = contactId;
            ViewBag.Contact2Id = contactId;
            ViewBag.Controller = "Contacts";
            try
            {
                entities = _contactRepo.Find(contactId);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactContactList", entities);
            }
            return View(entities);
        }

        public ActionResult CreateContactContact(int contactId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var contactContactRel = new ContactContactRel
            {
                ContactId = contactId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                Contact2 = new Contact(),
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
            ViewBag.Controller = "Contacts";
            ViewBag.ContactId = contactId;
            ViewBag.Contact2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactContact", contactContactRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateContactContact([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ContactId,Contact2Id")] ContactContactRel contactcontactrel)
        {
            if (ModelState.IsValid)
            {
                contactcontactrel.Contact2 = null;
                _contactRepo.InsertOrUpdateContactContact(contactcontactrel);
                _contactRepo.Save();
                return RedirectToAction("Details", "Contacts", new { id = contactcontactrel.ContactId });
            }
            return View();
        }

        public ActionResult EditContactContact(int id)
        {
            var contactContactRel = _contactRepo.GetContactContact(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactContact", contactContactRel);
            }
            return View(contactContactRel);

        }

        [HttpPost]
        public ActionResult EditContactContact(ContactContactRel contactcontactrel)
        {
            if (ModelState.IsValid)
            {
                _contactRepo.InsertOrUpdateContactContact(contactcontactrel);
                _contactRepo.Save();
                return RedirectToAction("Details", "Contacts", new { id = contactcontactrel.ContactId });
            }

            return View();

        }

        [HttpPost]
        public void DeleteContactContact(int id)
        {
            _contactRepo.DeleteContactContact(id);
            _contactRepo.Save();
        }

        #endregion


        #region Media Correspondence

        public ActionResult GetContactMediaCorrespondences(int contactId, int mediaCorrespondenceId)
        {
            IQueryable<ContactMediaCorrespondenceRel> contactMediaCorrespondences;
            if (mediaCorrespondenceId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.ContactId = contactId;
                ViewBag.Controller = "Contacts";
                contactMediaCorrespondences = _contactRepo.GetContactMediaCorrespondences(p => p.Contact.Id.Equals(contactId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.ContactId = contactId;
                ViewBag.Controller = "MediaCorrespondences";
                contactMediaCorrespondences = _contactRepo.GetContactMediaCorrespondences(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactMediaCorrespondenceList", contactMediaCorrespondences);
            }
            return View(contactMediaCorrespondences);
        }

        public ActionResult CreateContactMediaCorrespondence(int contactId, int mediaCorrespondenceId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new ContactMediaCorrespondenceRel()
            {
                MediaCorrespondenceId = mediaCorrespondenceId,
                ContactId = contactId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaCorrespondenceId == -1)
            {
                rel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Contacts";
                ViewBag.ContactId = contactId;
            }
            else
            {
                rel.Contact = new Contact();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            return PartialView("_CreateOrEditContactMediaCorrespondence", rel);
        }

        [HttpPost]
        public ActionResult CreateContactMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ContactId,MediaCorrespondenceId")] ContactMediaCorrespondenceRel contactMediaCorrespondenceRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _contactRepo.InsertOrUpdateContactMediaCorrespondence(contactMediaCorrespondenceRel);
            _contactRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditContactMediaCorrespondence(int id, string source)
        {
            var rel = _contactRepo.GetContactMediaCorrespondence(id);
            if (source == "Contacts")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
            }
            return PartialView("_CreateOrEditContactMediaCorrespondence", rel);
        }

        [HttpPost]
        public ActionResult EditContactMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ContactId,MediaCorrespondenceId")] ContactMediaCorrespondenceRel contactMediaCorrespondenceRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.OK);
            _contactRepo.InsertOrUpdateContactMediaCorrespondence(contactMediaCorrespondenceRel);
            _contactRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public void DeleteContactMediaCorrespondence(int id)
        {
            _contactRepo.DeleteContactMediaCorrespondence(id);
            _contactRepo.Save();
        }

        #endregion

    }
}

