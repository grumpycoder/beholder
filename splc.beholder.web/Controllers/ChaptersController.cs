using System;
using System.Collections;
using System.Net;
using System.Web.WebPages;
using Caseiro.Mvc.PagedList;
using Caseiro.Mvc.PagedList.Extensions;
using Kendo.Mvc.Extensions;
using splc.beholder.web.Models;
using splc.beholder.web.Utility;
using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class ChaptersController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IChapterRepository _chapterRepo;

        public ChaptersController(
            ILookupRepository lookupRepo,
            IChapterRepository chapterRepo)
        {
            _lookupRepo = lookupRepo;
            _chapterRepo = chapterRepo;

            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleChapterTypes = _lookupRepo.GetChapterTypes();
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRemovalStatus = _lookupRepo.GetRemovalStatus();
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes();
            ViewBag.PossibleContactInfoTypes = lookupRepo.GetContactInfoTypes();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
        }

        public JsonResult GetChapterList(string term)
        {

            string chaptername = string.Empty;
            string state = string.Empty;
            string city = string.Empty;

            if (term == null)
            {
                term = Request.Params["filter[filters][0][value]"];
                if (term != null)
                {
                    var searchParams = term.Split(':').ToList();

                    chaptername = searchParams[0].Trim();
                    //state = searchParams.ElementAtOrDefault(1);
                    state = searchParams.Count() > 1 ? searchParams[1].Trim() : string.Empty;
                    city = searchParams.Count() > 2 ? searchParams[2].Trim() : string.Empty;
                }
            }

            if (string.IsNullOrWhiteSpace(term) || term == "-1")
            {
                var list = _chapterRepo.GetChapters().OrderBy(x => x.ChapterName).Take(50).OrderBy(x => x.ChapterName).Take(50).Select(c => new
                {
                    c.ChapterName,
                    c.Id,
                    c.AddressChapterRels.FirstOrDefault().Address.City,
                    State = c.AddressChapterRels.FirstOrDefault().Address.State.StateCode,
                    Movement = c.MovementClass.Name, 
                    ActiveYear = c.ActiveYear
                });

                var items = list.ToList().Select(x => new
                {
                    Name = x.ChapterName,
                    x.Id,
                    x.City,
                    x.State,
                    x.Movement,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State), 
                    x.ActiveYear
                });

                return Json(items, JsonRequestBehavior.AllowGet);

            }
            if (term.IsInt())
            {
                var id = Convert.ToInt32(term);
                var list = _chapterRepo.GetChapters(x => x.Id == id).Select(c => new
                {
                    c.ChapterName,
                    c.Id,
                    c.AddressChapterRels.FirstOrDefault().Address.City,
                    State = c.AddressChapterRels.FirstOrDefault().Address.State.StateCode,
                    Movement = c.MovementClass.Name, 
                    ActiveYear = c.ActiveYear
                });

                var items = list.ToList().Select(x => new
                {
                    Name = x.ChapterName,
                    x.Id,
                    x.City,
                    x.State,
                    x.Movement,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State), 
                    x.ActiveYear
                });
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var list = _chapterRepo.GetChapters(x => x.ChapterName.Contains(chaptername) && (x.AddressChapterRels.Any(a => a.Address.State.StateCode.Contains(state) && a.Address.City.Contains(city)))).Select(c => new
                var list = _chapterRepo.GetChapters(x => x.ChapterName.Contains(chaptername) && (x.AddressChapterRels.FirstOrDefault().Address.State.StateCode.Contains(state) && x.AddressChapterRels.FirstOrDefault().Address.City.Contains(city))).Select(c => new
                {
                    c.ChapterName,
                    c.Id,
                    c.AddressChapterRels.FirstOrDefault().Address.City,
                    State = c.AddressChapterRels.FirstOrDefault().Address.State.StateCode,
                    Movement = c.MovementClass.Name, 
                    ActiveYear = c.ActiveYear
                });
                var items = list.ToList().Select(x => new
                {
                    Name = x.ChapterName,
                    x.Id,
                    x.City,
                    x.State,
                    x.Movement,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State), 
                    x.ActiveYear
                });
                return Json(items, JsonRequestBehavior.AllowGet);

            }
        }

        // GET: Search for Chapters 
        public JsonResult GetDropdown(string term, int? chapterId)
        {
            var list = _chapterRepo.GetDropdown(p => p.label.Contains(term) && p.Id != chapterId).ToArray();
            //var list = new SelectList(_chapterRepo.GetChapters(x => x.Id == chapterId && x.ChapterChapterRels.Any(m => m.ch)).ToList(), "Id", "Name");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? activeyear, int? activestatusid, string chaptername = "", List<int> movementclassid = null, string movementclassid_string = "", List<int> stateid = null, string stateid_string = "", string location = "", int? page = 1, int? pageSize = 15)
        {
            if (!String.IsNullOrWhiteSpace(movementclassid_string))
            {
                movementclassid = ((List<int>)movementclassid_string.Split(',').Select(int.Parse).ToList());
            }
            if (!String.IsNullOrWhiteSpace(stateid_string))
            {
                stateid = stateid_string.Split(',').Select(int.Parse).ToList();
            }

            Session["stateid"] = stateid;
            Session["movementclassid"] = movementclassid;
            Session["chaptername"] = chaptername;
            Session["location"] = location;
            Session["activeyear"] = activeyear;
            Session["activestatusid"] = activestatusid;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            PagedList<Chapter> list = null;
            if (movementclassid == null)
            {
                if (stateid != null)
                {
                    list = _chapterRepo.GetChapters(currentUser, x =>
                        x.ChapterName.Contains(chaptername)
                        && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                        && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                        && (location.Length == 0 || x.AddressChapterRels.Any(m => m.Address.City.Contains(location)))
                        && (x.AddressChapterRels.Any(m => m.Address.StateId != null && stateid.Contains((int)m.Address.StateId)))
                        ).OrderBy(m => m.ChapterName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
                else
                {
                    list = _chapterRepo.GetChapters(currentUser, x =>
                        x.ChapterName.Contains(chaptername)
                        && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                        && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                        && (location.Length == 0 || x.AddressChapterRels.Any(m => m.Address.City.Contains(location)))
                        ).OrderBy(m => m.ChapterName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
            }
            else
            {
                if (stateid != null)
                {
                    list = _chapterRepo.GetChapters(currentUser, x =>
                        x.ChapterName.Contains(chaptername)
                        && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                        && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                        && (movementclassid.Contains((int)x.MovementClassId))
                        && (location.Length == 0 || x.AddressChapterRels.Any(m => m.Address.City.Contains(location)))
                        && (x.AddressChapterRels.Any(m => m.Address.StateId != null && stateid.Contains((int)m.Address.StateId)))
                        ).OrderBy(m => m.ChapterName).ToPagedList(page ?? 1, pageSize ?? 15);

                }
                else
                {
                    list = _chapterRepo.GetChapters(currentUser, x =>
                                                                 x.ChapterName.Contains(chaptername)
                                                              && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                                                              && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                                                              && (movementclassid.Contains((int)x.MovementClassId))
                                                              && (location.Length == 0 || x.AddressChapterRels.Any(m => m.Address.City.Contains(location)))
                                                              ).OrderBy(m => m.ChapterName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
            }

            if (Request.IsAjaxRequest())
            {
                return list.Any() ? PartialView("_ChapterList", list) : PartialView("Chapter404");
            }

            return View("Index", list);

        }

        //[HttpPost]
        //public ActionResult Index(FormCollection form, int? page, int? pageSize)
        //{
        //    //Store page and searchTerm in session to allow returning back from details action link
        //    Session["page"] = page ?? 1;
        //    Session["formcollection"] = form;

        //    string chaptername = string.IsNullOrWhiteSpace(form["ChapterName"]) ? "" : form["ChapterName"];
        //    int? activeyear = string.IsNullOrWhiteSpace(form["ActiveYear"]) ? (int?)null : Convert.ToInt16(form["ActiveYear"]);
        //    int? movementclassid = string.IsNullOrWhiteSpace(form["MovementClassId"]) ? (int?)null : Convert.ToInt16(form["MovementClassId"]);
        //    var city = string.IsNullOrWhiteSpace(form["txtLocation"]) ? string.Empty : form["txtLocation"];

        //    var list = _chapterRepo.GetChapters(currentUser, x => x.ChapterName.Contains(chaptername)
        //                                                                 && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
        //                                                                 && x.MovementClassId == (movementclassid.HasValue ? movementclassid : x.MovementClassId)
        //                                              && x.AddressChapterRels.Any(m => m.Address.City.Contains(city)))
        //        .OrderBy(m => m.ChapterName).AsPagination(page ?? 1, pageSize ?? 15);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return list.Any() ? PartialView("_ChapterList", list) : PartialView("Chapter404");
        //    }

        //    return View("Index", list);

        //}


        // GET: /Organizations/Create
        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var chapter = new Chapter
            {
                ApprovalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id,
                ActiveStatusId = _lookupRepo.GetActiveStatuses().SingleOrDefault(p => p.Name.Equals("Active")).Id,
                ActiveYear = DateTime.Now.Year,
                ReportStatusFlag = false
            };
            return View(chapter);
        }

        //
        // POST: /Organizations/Create
        [HttpPost]
        public ActionResult Create(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                _chapterRepo.InsertOrUpdate(chapter);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapter.Id });
            }
            return View();
        }

        public ViewResult DetailsLite(int id)
        {
            var chapter = _chapterRepo.GetChapter(currentUser, id);
            return chapter != null ? View(chapter) : View("Chapter404");
        }

        public ViewResult Details(int id)
        {
            ViewBag.ChapterId = id;
            ViewBag.Chapter2Id = id;
            ViewBag.OrganizationId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.ContactId = -1;
            ViewBag.Controller = "Chapters";

            var chapter = _chapterRepo.GetChapter(currentUser, id);
            if (chapter != null)
            {
                return View(chapter);
            }
            return View("Chapter404");
        }

        //
        // GET: /Organizations/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_chapterRepo.GetChapter(id));
        }

        //
        // GET: /Organizations/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var chapter = _chapterRepo.GetChapter(currentUser, id);
            return chapter != null ? View(chapter) : View("Chapter404");
        }

        //
        // POST: /Organizations/Edit/5
        [HttpPost]
        public ActionResult Edit(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                _chapterRepo.InsertOrUpdate(chapter);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapter.Id });
            }
            return View(chapter);
        }

        public ActionResult RemoveChapter(int id)
        {
            var chapter = _chapterRepo.GetChapter(currentUser, id);
            return View(chapter);
        }

        [HttpPost]
        public ActionResult RemoveChapter(int id, string removalreason)
        {
            var chapter = _chapterRepo.GetChapter(currentUser, id);
            chapter.RemovalReason = removalreason;

            chapter.RemovalStatusId = 1;
            _chapterRepo.InsertOrUpdate(chapter);
            _chapterRepo.Save();

            return RedirectToAction("Index");

        }

        public ImageResult ShowPrimaryImage(int id)
        {
            byte[] picture;
            string contentType = null;
            Image image = null;

            var mediaImage = _chapterRepo.GetPrimaryImage(id);

            if (mediaImage.MediaImage != null || mediaImage.MediaImage.Image == null)
            {
                picture = System.IO.File.ReadAllBytes(Server.MapPath("~/content/images/image_unavailable.jpg"));
                contentType = "image/x-png";
            }
            else
            {
                picture = ImageHelper.Decompress(mediaImage.MediaImage.Image.IMAGE1);
                contentType = image.MimeType.Name;
            }

            return this.Image(picture, contentType);
        }

        //
        // POST: /Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _chapterRepo.Delete(id);
            _chapterRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region Persons

        public ActionResult GetChapterPersons(int chapterId, int beholderPersonId)
        {
            IQueryable<ChapterPersonRel> chapterPersons;
            if (chapterId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.ChapterId = chapterId;
                ViewBag.BeholderPersonId = beholderPersonId;
                ViewBag.Controller = "Persons";
                chapterPersons = _chapterRepo.GetChapterPersons(p => p.BeholderPerson.Id.Equals(beholderPersonId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.ChapterId = chapterId;
                ViewBag.BeholderPersonId = beholderPersonId;
                ViewBag.Controller = "Chapters";
                chapterPersons = _chapterRepo.GetChapterPersons(p => p.Chapter.Id.Equals(chapterId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterPersonList", chapterPersons);
            }
            return View(chapterPersons);
        }

        //
        //GET: /Chapters/CreatePersonChapter
        public ActionResult CreateChapterPerson(int chapterId, int beholderPersonId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterPersonRel = new ChapterPersonRel()
            {
                ChapterId = chapterId,
                BeholderPersonId = beholderPersonId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (chapterId == -1)
            {
                chapterPersonRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Persons";
                ViewBag.PersonId = beholderPersonId;
            }
            else
            {
                chapterPersonRel.BeholderPerson = new BeholderPerson();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterPerson", chapterPersonRel);
            }

            return View(chapterPersonRel);
        }

        //
        //POST: /Chapters/CreatePersonChapter
        [HttpPost]
        public ActionResult CreateChapterPerson([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,BeholderPersonId")] ChapterPersonRel chapterpersonrel)
        {
            if (ModelState.IsValid)
            {
                if (chapterpersonrel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterpersonrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterPerson(chapterpersonrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = chapterpersonrel.BeholderPersonId });
                }
                //reset the subscription object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterpersonrel.BeholderPerson = null;
                _chapterRepo.InsertOrUpdateChapterPerson(chapterpersonrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapterpersonrel.ChapterId });
            }
            return View();
        }

        //
        // GET: /ChapterPersonRels/EditChapterPerson/5
        public ActionResult EditChapterPerson(int id, string source)
        {
            var chapterPersonRel = _chapterRepo.GetChapterPerson(id);
            if (source == "Persons")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Individual") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.BeholderPersonId = chapterPersonRel.BeholderPersonId;
                ViewBag.ChapterId = -1;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Individual")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterPersonRel.ChapterId;
                ViewBag.BeholderPersonId = -1;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterPerson", chapterPersonRel);
            }
            return View(chapterPersonRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterPerson([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,BeholderPersonId")] ChapterPersonRel chapterpersonrel)
        {
            if (ModelState.IsValid)
            {
                if (chapterpersonrel.BeholderPerson == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterpersonrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterPerson(chapterpersonrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Persons", new { id = chapterpersonrel.BeholderPersonId });
                }
                //reset the subscription object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterpersonrel.BeholderPerson = null;
                _chapterRepo.InsertOrUpdateChapterPerson(chapterpersonrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapterpersonrel.ChapterId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterPerson(int id)
        {
            _chapterRepo.DeleteChapterPerson(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Organizations

        public ActionResult CreateChapterOrganization(int chapterId, int organizationId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterOrganizationRel = new ChapterOrganizationRel
            {
                ChapterId = chapterId,
                OrganizationId = organizationId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (chapterId == -1)
            {
                chapterOrganizationRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Organizations";
                ViewBag.OrganizationId = organizationId;
            }
            else
            {
                chapterOrganizationRel.Organization = new Organization();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterOrganization", chapterOrganizationRel);
            }

            return View();
        }

        //
        //POST: /Chapters/CreatePersonChapter
        [HttpPost]
        public ActionResult CreateChapterOrganization([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,OrganizationId")] ChapterOrganizationRel chapterorganizationrel)
        {
            if (ModelState.IsValid)
            {
                if (chapterorganizationrel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterorganizationrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterOrganization(chapterorganizationrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = chapterorganizationrel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterorganizationrel.Organization = null;
                _chapterRepo.InsertOrUpdateChapterOrganization(chapterorganizationrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapterorganizationrel.ChapterId });
            }
            return View();
        }

        //
        // GET: /ChapterPersonRels/EditChapterPerson/5
        public ActionResult EditChapterOrganization(int id, string source)
        {
            var chapterOrganizationRel = _chapterRepo.GetChaperOrganization(id);
            if (source == "Organizations")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Group") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.OrganizationId = chapterOrganizationRel.OrganizationId;
                ViewBag.ChapterId = -1;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Group")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterOrganizationRel.ChapterId;
                ViewBag.OrganizationId = -1;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterOrganization", chapterOrganizationRel);
            }
            return View(chapterOrganizationRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterOrganization(ChapterOrganizationRel chapterorganizationrel)
        {
            if (ModelState.IsValid)
            {
                if (chapterorganizationrel.Organization == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterorganizationrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterOrganization(chapterorganizationrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Organizations", new { id = chapterorganizationrel.OrganizationId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterorganizationrel.Organization = null;
                _chapterRepo.InsertOrUpdateChapterOrganization(chapterorganizationrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chapterorganizationrel.ChapterId });
            }
            return View();
        }

        // GET: /Aliases/GetPersonAliases/1
        public ActionResult GetChapterOrganizations(int chapterId, int organizationId)
        {
            IQueryable<ChapterOrganizationRel> chapterOrganizations;
            if (chapterId == -1)
            {
                ViewBag.OrganizationId = organizationId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Organizations";
                chapterOrganizations = _chapterRepo.GetChapterOrganizations(p => p.Organization.Id.Equals(organizationId));
            }
            else
            {
                ViewBag.OrganizationId = organizationId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterOrganizations = _chapterRepo.GetChapterOrganizations(p => p.Chapter.Id.Equals(chapterId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterOrganizationList", chapterOrganizations);
            }
            return View(chapterOrganizations);
        }

        [HttpPost]
        public void DeleteChapterOrganization(int id)
        {
            _chapterRepo.DeleteChapterOrganization(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Events

        public ActionResult GetChapterEvents(int chapterId, int eventId)
        {
            IQueryable<ChapterEventRel> chapterEvents;
            if (eventId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterEvents = _chapterRepo.GetChapterEvents(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Events";
                chapterEvents = _chapterRepo.GetChapterEvents(p => p.Event.Id.Equals(eventId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterEventList", chapterEvents);
            }
            return View(chapterEvents);
        }

        public ActionResult CreateChapterEvent(int chapterId, int eventId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterEventRel = new ChapterEventRel
            {
                EventId = eventId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (eventId == -1)
            {
                chapterEventRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                chapterEventRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterEvent", chapterEventRel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,EventId")] ChapterEventRel chapterEventRel)
        {
            if (ModelState.IsValid)
            {
                _chapterRepo.InsertOrUpdateChapterEvent(chapterEventRel);
                _chapterRepo.Save();
                return null;
            }
            return View(chapterEventRel);
        }

        public ActionResult EditChapterEvent(int id, string source)
        {
            var chapterEventRel = _chapterRepo.GetChapterEvent(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterEventRel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterEventRel.EventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterEvent", chapterEventRel);
            }
            return View(chapterEventRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,EventId")] ChapterEventRel chapterEventRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterEventRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterEventRel.Event = null;
                    _chapterRepo.InsertOrUpdateChapterEvent(chapterEventRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterEventRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterEventRel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterEvent(chapterEventRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Events", new { id = chapterEventRel.EventId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterEvent(int id)
        {
            _chapterRepo.DeleteChapterEvent(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Contacts

        public ActionResult GetChapterContacts(int? chapterId, int? contactId)
        {
            ViewBag.ContactId = contactId;
            ViewBag.ChapterId = chapterId;
            ViewBag.Controller = contactId == -1 ? "Chapters" : "Contacts";

            var list =
                _chapterRepo.GetChapterContacts(p => p.ChapterId == chapterId || p.ContactId == contactId);

            //var list = new ACDBContext().ChapterContactRels.Where(x => x.ChapterId == chapterId);

            //IQueryable<ChapterContactRel> chapterContacts;
            //if (contactId == -1)
            //{
            //    //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
            //    //ViewBag.ContactId = contactId;
            //    //ViewBag.ChapterId = chapterId;
            //    //ViewBag.Controller = "Chapters";
            //    chapterContacts = _chapterRepo.GetChapterContacts(p => p.Chapter.Id.Equals(chapterId));
            //}
            //else
            //{
            //    //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
            //    //ViewBag.ContactId = contactId;
            //    //ViewBag.ChapterId = chapterId;
            //    //ViewBag.Controller = "Contacts";
            //    chapterContacts = _chapterRepo.GetChapterContacts(p => p.Contact.Id.Equals(contactId));
            //}

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterContactList", list);
            }
            return null;
            //return View(chapterContacts);
        }

        public ActionResult CreateChapterContact(int chapterId, int contactId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterContactRel = new ChapterContactRel
            {
                ContactId = contactId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (contactId == -1)
            {
                chapterContactRel.Contact = new Contact();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                chapterContactRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Contacts";
                ViewBag.ContactId = contactId;
            }

            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_CreateOrEditChapterContact", chapterContactRel);
            //}

            //return View();
        }

        [HttpPost]
        public ActionResult CreateChapterContact([Bind(Include = "Id,PrimaryStatusId,RelationshipTypeId,DateStart,DateEnd,ContactId,ChapterId")] ChapterContactRel chapterContactRel)
        {
            if (ModelState.IsValid)
            {
                //if (chapterContactRel.Chapter == null)
                //{
                //    chapterContactRel.Contact = null;
                //    _chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                //    _chapterRepo.Save();
                //    return null;
                //    //return RedirectToAction("Details", "Chapters", new { id = chapterContactRel.ChapterId });
                //}
                ////reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //chapterContactRel.Chapter = null;
                //_chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                //_chapterRepo.Save();
                ////return RedirectToAction("Details", "Contact", new { id = chapterContactRel.ContactId });
                //return null;
                _chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                _chapterRepo.Save();
            }
            return null;
            //return View(chapterContactRel);
        }

        public ActionResult EditChapterContact(int id, string source)
        {
            var chapterContactRel = _chapterRepo.GetChapterContact(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Contact")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterContactRel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Contact") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterContactRel.ContactId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterContact", chapterContactRel);
            }
            return View(chapterContactRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterContact([Bind(Include = "Id,PrimaryStatusId,RelationshipTypeId,DateStart,DateEnd,ContactId,ChapterId")] ChapterContactRel chapterContactRel)
        {
            if (ModelState.IsValid)
            {
                //if (chapterContactRel.Chapter == null)
                //{
                //    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                //    chapterContactRel.Contact = null;
                //    _chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                //    _chapterRepo.Save();
                //    //return RedirectToAction("Details", "Chapters", new { id = chapterContactRel.ChapterId });
                //    return null;
                //}
                ////reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //chapterContactRel.Chapter = null;
                //_chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                //_chapterRepo.Save();
                ////return RedirectToAction("Details", "Contacts", new { id = chapterContactRel.ContactId });
                //return null;
                _chapterRepo.InsertOrUpdateChapterContact(chapterContactRel);
                _chapterRepo.Save();
            }
            return null;
            //return View();
        }

        [HttpPost]
        public void DeleteChapterContact(int id)
        {
            _chapterRepo.DeleteChapterContact(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Media Images

        // GET: /Organizations/Details/5
        public ViewResult DetailsMediaImage(int id)
        {
            return View(_chapterRepo.GetChapterMediaImage(id));
        }

        public ActionResult GetChapterMediaImages(int chapterId, int mediaImageId)
        {
            IQueryable<ChapterMediaImageRel> chapterMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterMediaImages = _chapterRepo.GetMediaImages(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "MediaImages";
                chapterMediaImages = _chapterRepo.GetMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaImageList", chapterMediaImages);
            }
            return View(chapterMediaImages);
        }

        public ActionResult CreateChapterMediaImage(int chapterId, int mediaImageId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterMediaImageRel = new ChapterMediaImageRel()
            {
                MediaImageId = mediaImageId,
                ChapterId = chapterId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                chapterMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                chapterMediaImageRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaImage", chapterMediaImageRel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaImageId")] ChapterMediaImageRel chapterMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaImageRel.Chapter == null)
                {
                    chapterMediaImageRel.MediaImage = null;
                    _chapterRepo.InsertOrUpdateMediaImage(chapterMediaImageRel);
                    _chapterRepo.Save();
                    return null;
                    //return RedirectToAction("Details", "Chapters", new { id = chapterMediaImageRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaImageRel.Chapter = null;
                _chapterRepo.InsertOrUpdateMediaImage(chapterMediaImageRel);
                _chapterRepo.Save();
                return null;
                //return RedirectToAction("Details", "MediaImage", new { id = chapterMediaImageRel.MediaImageId });
            }
            return View();
        }

        public ActionResult EditChapterMediaImage(int id, string source)
        {
            var chapterMediaImageRel = _chapterRepo.GetMediaImage(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterMediaImageRel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = chapterMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaImage", chapterMediaImageRel);
            }
            return View(chapterMediaImageRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,ChapterId,MediaImageId")] ChapterMediaImageRel chapterMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaImageRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaImageRel.MediaImage = null;
                    _chapterRepo.InsertOrUpdateMediaImage(chapterMediaImageRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaImageRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaImageRel.Chapter = null;
                _chapterRepo.InsertOrUpdateMediaImage(chapterMediaImageRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = chapterMediaImageRel.MediaImageId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterMediaImage(int id)
        {
            _chapterRepo.DeleteChapterMedia(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Media Audio Videos

        public ActionResult GetChapterMediaAudioVideos(int chapterId, int mediaAudioVideoId)
        {
            IQueryable<ChapterMediaAudioVideoRel> chapterMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterMediaAudioVideos = _chapterRepo.GetChapterMediaAudioVideos(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "MediaAudioVideos";
                chapterMediaAudioVideos = _chapterRepo.GetChapterMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaAudioVideoList", chapterMediaAudioVideos);
            }
            return View(chapterMediaAudioVideos);
        }

        public ActionResult CreateChapterMediaAudioVideo(int chapterId, int mediaAudioVideoId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new ChapterMediaAudioVideoRel()
            {
                MediaAudioVideoId = mediaAudioVideoId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                rel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                rel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaAudioVideo", rel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaAudioVideoId")] ChapterMediaAudioVideoRel chapterMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaAudioVideoRel.Chapter == null)
                {
                    chapterMediaAudioVideoRel.MediaAudioVideo = null;
                    _chapterRepo.InsertOrUpdateChapterMediaAudioVideo(chapterMediaAudioVideoRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaAudioVideoRel.ChapterId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaAudioVideoRel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterMediaAudioVideo(chapterMediaAudioVideoRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = chapterMediaAudioVideoRel.MediaAudioVideoId });
                }
            }
            return View();
        }

        public ActionResult EditChapterMediaAudioVideo(int id, string source)
        {
            var rel = _chapterRepo.GetChapterMediaAudioVideo(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = rel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaAudioVideo", rel);
            }
            return View(rel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,ChapterId,MediaAudioVideoId")] ChapterMediaAudioVideoRel chapterMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaAudioVideoRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaAudioVideoRel.MediaAudioVideo = null;
                    _chapterRepo.InsertOrUpdateChapterMediaAudioVideo(chapterMediaAudioVideoRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaAudioVideoRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaAudioVideoRel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterMediaAudioVideo(chapterMediaAudioVideoRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = chapterMediaAudioVideoRel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterMediaAudioVideo(int id)
        {
            _chapterRepo.DeleteChapterMediaAudioVideo(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Contact Info

        public ActionResult GetChapterContactInfo(int id)
        {
            var contactInfo = _chapterRepo.GetContactInfo(p => p.Chapter.Id.Equals(id));
            ViewBag.ChapterId = id;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterContactInfoList", contactInfo);
            }
            return View("Index");
        }

        public ActionResult CreateContactInfo(int? chapterId)
        {
            //ViewBag.PossibleContactInfoTypes = context.ContactInfoTypes.OrderBy(x => x.SortOrder);
            //ViewBag.PossiblePrimaryStatus = context.PrimaryStatus.OrderBy(x => x.SortOrder);
            var contactInfo = new ChapterContactInfoRel
            {
                ChapterId = chapterId ?? -1,
                ContactInfo = new ContactInfo()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactInfo", contactInfo);
            }
            return View(contactInfo);
        }

        //
        // POST: /ContactInfoPersonRels/Create

        [HttpPost]
        public ActionResult CreateContactInfo(ChapterContactInfoRel contactinforel)
        {
            if (ModelState.IsValid)
            {
                _chapterRepo.InsertOrUpdateContactInfo(contactinforel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = contactinforel.ChapterId });
            }
            return View();
        }

        //
        // GET: /ContactInfoPersonRels/Edit/5

        public ActionResult EditContactInfo(int id)
        {
            ViewBag.PossibleContactInfoTypes = _lookupRepo.GetContactInfoTypes();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();

            var contactInfo = _chapterRepo.GetContactInfo(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactInfo", contactInfo);
            }
            return View(contactInfo);
        }

        //
        // POST: /ContactInfoPersonRels/Edit/5

        [HttpPost]
        public ActionResult EditContactInfo(ChapterContactInfoRel contactinforel)
        {
            ViewBag.ChapterId = contactinforel.ChapterId;
            ViewBag.ContactId = contactinforel.ContactInfoId;

            if (ModelState.IsValid)
            {
                _chapterRepo.InsertOrUpdateContactInfo(contactinforel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = contactinforel.ChapterId });
            }
            return View();
        }

        //
        // GET: /ContactInfoPersonRels/Delete/5

        public void DeleteContactInfo(int id)
        {
            _chapterRepo.DeleteContactInfo(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Vehicles

        public ActionResult GetChapterVehicles(int chapterId, int vehicleId)
        {
            IQueryable<ChapterVehicleRel> chapterVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterVehicles = _chapterRepo.GetChapterVehicles(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Vehicles";
                chapterVehicles = _chapterRepo.GetChapterVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterVehicleList", chapterVehicles);
            }
            return View(chapterVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateChapterVehicle(int vehicleId, int chapterId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterVehicleRel = new ChapterVehicleRel()
            {
                VehicleId = vehicleId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                chapterVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                chapterVehicleRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterVehicle", chapterVehicleRel);
            }

            return View();
        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreateChapterVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,VehicleId")] ChapterVehicleRel chaptervehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (chaptervehiclerel.Chapter == null)
                {
                    chaptervehiclerel.Vehicle = null;
                    _chapterRepo.InsertOrUpdateChapterVehicle(chaptervehiclerel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chaptervehiclerel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chaptervehiclerel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterVehicle(chaptervehiclerel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = chaptervehiclerel.VehicleId });
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditChapterVehicle(int id, string source)
        {
            var chapterVehicleRel = _chapterRepo.GetChapterVehicle(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = chapterVehicleRel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = chapterVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterVehicle", chapterVehicleRel);
            }
            return View(chapterVehicleRel);
        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditChapterVehicle(ChapterVehicleRel chaptervehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (chaptervehiclerel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chaptervehiclerel.Vehicle = null;
                    _chapterRepo.InsertOrUpdateChapterVehicle(chaptervehiclerel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chaptervehiclerel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chaptervehiclerel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterVehicle(chaptervehiclerel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = chaptervehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterVehicle(int id)
        {
            _chapterRepo.DeleteChapterVehicle(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Chapters

        public ActionResult GetChapterChapters(int chapterId)
        {
            Chapter chapters;
            //if (chapterId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
            ViewBag.ChapterId = chapterId;
            ViewBag.Chapter2Id = chapterId;
            ViewBag.Controller = "Chapters";
            try
            {
                chapters = _chapterRepo.GetChapter(chapterId);
                //                chapters = _chapterRepo.Get(p => p.Id.Equals(chapterId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterChapterList", chapters);
            }
            return View(chapters);
        }

        public ActionResult CreateChapterChapter(int chapterId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterChapterRel = new ChapterChapterRel
            {
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now
            };
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
            return PartialView("_CreateOrEditChapterChapter", chapterChapterRel);
        }

        [HttpPost]
        public ActionResult CreateChapterChapter([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,Chapter2Id")] ChapterChapterRel chapterChapterRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            chapterChapterRel.Chapter2 = null;
            _chapterRepo.InsertOrUpdateChapterChapter(chapterChapterRel);
            _chapterRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditChapterChapter(int id)
        {
            var chapterChapterRel = _chapterRepo.GetChapterChapter(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
            return PartialView("_CreateOrEditChapterChapter", chapterChapterRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterChapter([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,Chapter2Id")] ChapterChapterRel chapterChapterRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _chapterRepo.InsertOrUpdateChapterChapter(chapterChapterRel);
            _chapterRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public void DeleteChapterChapter(int id)
        {
            _chapterRepo.DeleteChapterChapter(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Media Published

        public ActionResult GetChapterMediaPublisheds(int chapterId, int mediaPublishedId)
        {
            IQueryable<ChapterMediaPublishedRel> chapterMediaPublisheds;
            if (mediaPublishedId == -1)
            {
                //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterMediaPublisheds = _chapterRepo.GetChapterMediaPublisheds(p => p.ChapterId.Equals(chapterId)).OrderByDescending(x => x.MediaPublished.DateModified);
            }
            else
            {
                //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "MediaPublisheds";
                chapterMediaPublisheds = _chapterRepo.GetChapterMediaPublisheds(x => x.MediaPublishedId.Equals(mediaPublishedId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaPublishedList", chapterMediaPublisheds);
            }
            return View(chapterMediaPublisheds);
        }

        public ActionResult CreateChapterMediaPublished(int chapterId, int mediaPublishedId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new ChapterMediaPublishedRel()
            {
                MediaPublishedId = mediaPublishedId,
                ChapterId = chapterId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaPublishedId == -1)
            {
                rel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                rel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaPublished", rel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaPublishedId")] ChapterMediaPublishedRel chapterMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                //if (chapterMediaPublishedRel.Chapter == null)
                //{
                //    chapterMediaPublishedRel.MediaPublished = null;
                _chapterRepo.InsertOrUpdateChapterMediaPublished(chapterMediaPublishedRel);
                _chapterRepo.Save();
                return null;
                //    return RedirectToAction("Details", "Chapters", new { id = chapterMediaPublishedRel.ChapterId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //    chapterMediaPublishedRel.Chapter = null;
                //    _chapterRepo.InsertOrUpdateChapterMediaPublished(chapterMediaPublishedRel);
                //    _chapterRepo.Save();
                //    return RedirectToAction("Details", "MediaPublisheds", new { id = chapterMediaPublishedRel.MediaPublishedId });
                //}
            }
            return View(chapterMediaPublishedRel);
        }

        public ActionResult EditChapterMediaPublished(int id, string source)
        {
            var rel = _chapterRepo.GetChapterMediaPublished(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = rel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaPublishedId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaPublished", rel);
            }
            return View(rel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,ChapterId")] ChapterMediaPublishedRel chapterMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaPublishedRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaPublishedRel.MediaPublished = null;
                    _chapterRepo.InsertOrUpdateChapterMediaPublished(chapterMediaPublishedRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaPublishedRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaPublishedRel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterMediaPublished(chapterMediaPublishedRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = chapterMediaPublishedRel.MediaPublishedId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterMediaPublished(int id)
        {
            _chapterRepo.DeleteChapterMediaPublished(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Media Correspondence

        public ActionResult GetChapterMediaCorrespondences(int chapterId, int mediaCorrespondenceId)
        {
            IQueryable<ChapterMediaCorrespondenceRel> chapterMediaCorrespondences;
            if (mediaCorrespondenceId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterMediaCorrespondences = _chapterRepo.GetChapterMediaCorrespondences(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "MediaCorrespondences";
                chapterMediaCorrespondences = _chapterRepo.GetChapterMediaCorrespondences(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaCorrespondenceList", chapterMediaCorrespondences);
            }
            return View(chapterMediaCorrespondences);
        }

        public ActionResult CreateChapterMediaCorrespondence(int chapterId, int mediaCorrespondenceId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new ChapterMediaCorrespondenceRel()
            {
                MediaCorrespondenceId = mediaCorrespondenceId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaCorrespondenceId == -1)
            {
                rel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                rel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaCorrespondence", rel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaCorrespondenceId")] ChapterMediaCorrespondenceRel chapterMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                //if (chapterMediaCorrespondenceRel.Chapter == null)
                //{
                chapterMediaCorrespondenceRel.MediaCorrespondence = null;
                _chapterRepo.InsertOrUpdateChapterMediaCorrespondence(chapterMediaCorrespondenceRel);
                _chapterRepo.Save();
                return null;
                //    return RedirectToAction("Details", "Chapters", new { id = chapterMediaCorrespondenceRel.ChapterId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //    chapterMediaCorrespondenceRel.Chapter = null;
                //    _chapterRepo.InsertOrUpdateChapterMediaCorrespondence(chapterMediaCorrespondenceRel);
                //    _chapterRepo.Save();
                //    return RedirectToAction("Details", "MediaCorrespondences", new { id = chapterMediaCorrespondenceRel.MediaCorrespondenceId });
                //}
            }
            return View(chapterMediaCorrespondenceRel);
        }

        public ActionResult EditChapterMediaCorrespondence(int id, string source)
        {
            var rel = _chapterRepo.GetChapterMediaCorrespondence(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = rel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaCorrespondence", rel);
            }
            return View(rel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaCorrespondenceId")] ChapterMediaCorrespondenceRel chapterMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaCorrespondenceRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaCorrespondenceRel.MediaCorrespondence = null;
                    _chapterRepo.InsertOrUpdateChapterMediaCorrespondence(chapterMediaCorrespondenceRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaCorrespondenceRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaCorrespondenceRel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterMediaCorrespondence(chapterMediaCorrespondenceRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = chapterMediaCorrespondenceRel.MediaCorrespondenceId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterMediaCorrespondence(int id)
        {
            _chapterRepo.DeleteChapterMediaCorrespondence(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Media WebsiteEGroup

        public ActionResult GetChapterMediaWebsiteEGroups(int chapterId, int mediaWebsiteEGroupId)
        {
            IQueryable<ChapterMediaWebsiteEGroupRel> chapterMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterMediaWebsiteEGroups = _chapterRepo.GetChapterMediaWebsiteEGroups(p => p.Chapter.Id.Equals(chapterId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                chapterMediaWebsiteEGroups = _chapterRepo.GetChapterMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaWebsiteEGroupList", chapterMediaWebsiteEGroups);
            }
            return View(chapterMediaWebsiteEGroups);
        }

        public ActionResult CreateChapterMediaWebsiteEGroup(int chapterId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var rel = new ChapterMediaWebsiteEGroupRel()
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                ChapterId = chapterId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                rel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }
            else
            {
                rel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaWebsiteEGroup", rel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateChapterMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,MediaWebsiteEGroupId")] ChapterMediaWebsiteEGroupRel chapterMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                //if (chapterMediaWebsiteEGroupRel.Chapter == null)
                //{
                //    chapterMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                _chapterRepo.InsertOrUpdateChapterMediaWebsiteEGroup(chapterMediaWebsiteEGroupRel);
                _chapterRepo.Save();
                //return RedirectToAction("Details", "Chapters", new { id = chapterMediaWebsiteEGroupRel.ChapterId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                //    chapterMediaWebsiteEGroupRel.Chapter = null;
                //    _chapterRepo.InsertOrUpdateChapterMediaWebsiteEGroup(chapterMediaWebsiteEGroupRel);
                //    _chapterRepo.Save();
                //    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = chapterMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
                //}
                return null;
            }
            return View();
        }

        public ActionResult EditChapterMediaWebsiteEGroup(int id, string source)
        {
            var rel = _chapterRepo.GetChapterMediaWebsiteEGroup(id);
            if (source == "Chapters")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = rel.ChapterId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = rel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterMediaWebsiteEGroup", rel);
            }
            return View(rel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,ChapterId")] ChapterMediaWebsiteEGroupRel chapterMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (chapterMediaWebsiteEGroupRel.Chapter == null)
                {
                    //reset the chapter object.  This is only added from Organization, not ChapterOrganizationRel.
                    chapterMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _chapterRepo.InsertOrUpdateChapterMediaWebsiteEGroup(chapterMediaWebsiteEGroupRel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Chapters", new { id = chapterMediaWebsiteEGroupRel.ChapterId });
                }
                //reset the organization object.  This is only added from Organization, not ChapterOrganizationRel.
                chapterMediaWebsiteEGroupRel.Chapter = null;
                _chapterRepo.InsertOrUpdateChapterMediaWebsiteEGroup(chapterMediaWebsiteEGroupRel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = chapterMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteChapterMediaWebsiteEGroup(int id)
        {
            _chapterRepo.DeleteChapterMediaWebsiteEGroup(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Subscriptions

        public ActionResult CreateChapterSubscription(int chapterId, int subscriptionId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var chapterSubscriptionRel = new ChapterSubscriptionRel
            {
                ChapterId = chapterId,
                SubscriptionId = subscriptionId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (chapterId == -1)
            {
                chapterSubscriptionRel.Chapter = new Chapter();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }
            else
            {
                chapterSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Chapters";
                ViewBag.ChapterId = chapterId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterSubscription", chapterSubscriptionRel);
            }

            return View();
        }

        //
        //POST: /Chapters/CreatePersonChapter
        [HttpPost]
        public ActionResult CreateChapterSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,SubscriptionId")] ChapterSubscriptionRel chaptersubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (chaptersubscriptionrel.Subscription == null)
                {
                    //reset the chapter object.  This is only added from Subscription, not ChapterSubscriptionRel.
                    chaptersubscriptionrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterSubscription(chaptersubscriptionrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = chaptersubscriptionrel.SubscriptionId });
                }
                //reset the subscription object.  This is only added from Subscription, not ChapterSubscriptionRel.
                chaptersubscriptionrel.Subscription = null;
                _chapterRepo.InsertOrUpdateChapterSubscription(chaptersubscriptionrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chaptersubscriptionrel.ChapterId });
            }
            return View();
        }

        //
        // GET: /ChapterPersonRels/EditChapterPerson/5
        public ActionResult EditChapterSubscription(int id, string source)
        {
            var chapterSubscriptionRel = _chapterRepo.GetChapterSubscription(id);
            if (source == "Subscriptions")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Chapter")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = chapterSubscriptionRel.SubscriptionId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Chapter") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.ChapterId = chapterSubscriptionRel.ChapterId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterSubscription", chapterSubscriptionRel);
            }
            return View(chapterSubscriptionRel);
        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditChapterSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,ChapterId,SubscriptionId")] ChapterSubscriptionRel chaptersubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (chaptersubscriptionrel.Subscription == null)
                {
                    //reset the chapter object.  This is only added from Subscription, not ChapterSubscriptionRel.
                    chaptersubscriptionrel.Chapter = null;
                    _chapterRepo.InsertOrUpdateChapterSubscription(chaptersubscriptionrel);
                    _chapterRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = chaptersubscriptionrel.SubscriptionId });
                }
                //reset the subscription object.  This is only added from Subscription, not ChapterSubscriptionRel.
                chaptersubscriptionrel.Subscription = null;
                _chapterRepo.InsertOrUpdateChapterSubscription(chaptersubscriptionrel);
                _chapterRepo.Save();
                return RedirectToAction("Details", "Chapters", new { id = chaptersubscriptionrel.ChapterId });
            }
            return View();
        }

        // GET: /Aliases/GetPersonAliases/1
        public ActionResult GetChapterSubscriptions(int chapterId, int subscriptionId)
        {
            IQueryable<ChapterSubscriptionRel> chapterSubscriptions;
            if (chapterId == -1)
            {
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Subscriptions";
                chapterSubscriptions = _chapterRepo.GetChapterSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }
            else
            {
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.ChapterId = chapterId;
                ViewBag.Controller = "Chapters";
                chapterSubscriptions = _chapterRepo.GetChapterSubscriptions(p => p.Chapter.Id.Equals(chapterId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterSubscriptionList", chapterSubscriptions);
            }
            return View(chapterSubscriptions);
        }

        [HttpPost]
        public void DeleteChapterSubscription(int id)
        {
            _chapterRepo.DeleteChapterSubscription(id);
            _chapterRepo.Save();
        }

        #endregion

        #region Status History

        public ActionResult GetChapterStatusHistories(int id)
        {
            var entities = _chapterRepo.GetChapterStatusHistories(p => p.ChapterId.Equals(id)).ToArray().OrderBy(c => c.ActiveYear);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterStatusHistoryList", entities);
            }
            return View("Index");
        }

        #endregion
    }
}