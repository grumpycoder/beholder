using splc.beholder.web.Models;
using splc.data;
using splc.data.repository;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class ApprovalController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IApprovalRepository _approvalRepo;
        private readonly ACDBContext _ctx;
        //TODO: Need refactoring to use repositories

        public ApprovalController(ACDBContext ctx, IPersonRepository personRepo, ILookupRepository lookupRepo, IApprovalRepository approvalRepo)
        {
            _ctx = ctx;
            _lookupRepo = lookupRepo;
            _approvalRepo = approvalRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovalResultReview(int? activeyear)
        {
            var year = activeyear ?? DateTime.Now.Year - 1;

            var orgList = _ctx.Organizations.Where(x => x.ActiveYear == year && x.ActiveStatusId == 1 && x.ApprovalStatusId == 1).ToList();
            var chapterList = _ctx.Chapters.Where(x => x.ActiveYear == year && x.ActiveStatusId == 1 && x.ApprovalStatusId == 1).ToList();
            var webList = _ctx.MediaWebsiteEGroups.Where(x => x.ActiveYear == year && x.ActiveStatusId == 1 && x.ApprovalStatusId == 1).ToList();

            var list = orgList.Select(item => new ApprovalViewModel
            {
                EntityType = "Organization",
                ActiveYear = item.ActiveYear,
                ApprovalStatus = item.ApprovalStatus,
                Name = item.OrganizationName,
                Id = item.Id
            }).ToList();

            list.AddRange(chapterList.Select(item => new ApprovalViewModel
            {
                EntityType = "Chapter",
                ActiveYear = item.ActiveYear,
                ApprovalStatus = item.ApprovalStatus,
                Name = item.ChapterName,
                Id = item.Id
            }));

            list.AddRange(webList.Select(item => new ApprovalViewModel
            {
                EntityType = "Website",
                ActiveYear = item.ActiveYear,
                ApprovalStatus = item.ApprovalStatus,
                Name = item.Name,
                Id = item.Id
            }));

            return View(list);
        }

        public ActionResult ApprovalReset(int activeyear)
        {
            var year = activeyear;
            using (var context = new ACDBContext())
            {
                using (var dbContextTrans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand("Beholder.p_ApprovalReset @activeYear, @userId", new SqlParameter("ActiveYear", year), new SqlParameter("userId", CurrentUser.Id));
                        context.SaveChanges();
                        dbContextTrans.Commit();
                    }
                    catch (Exception e)
                    {

                        dbContextTrans.Rollback();
                    }
                }
            }
            return RedirectToAction("ApprovalResultReview");
        }

        public ActionResult PendingRemovals()
        {

            var list = _ctx.PendingRemovals.ToList();

            //var orgList = _ctx.Organizations.Where(x => x.RemovalStatusId != null).ToList();
            //var personList = _ctx.BeholderPersons.Where(x => x.RemovalStatusId != null).ToList();
            //var chapterList = _ctx.Chapters.Where(x => x.RemovalStatusId != null).ToList();
            //var webList = _ctx.MediaWebsiteEGroups.Where(x => x.RemovalStatusId != null).ToList();


            //var list = orgList.Select(item => new PendingRemovalViewModel()
            //{
            //    EntityType = "Organization",
            //    RemovalReason = item.RemovalReason,
            //    Name = item.OrganizationName,
            //    Id = item.Id
            //}).ToList();

            //list.AddRange(personList.Select(item => new PendingRemovalViewModel
            //{
            //    EntityType = "Person",
            //    RemovalReason = item.RemovalReason,
            //    Name = item.CommonPerson.FullName,
            //    Id = item.Id
            //}));

            //list.AddRange(chapterList.Select(item => new PendingRemovalViewModel
            //{
            //    EntityType = "Chapter",
            //    RemovalReason = item.RemovalReason,
            //    Name = item.ChapterName,
            //    Id = item.Id
            //}));

            //list.AddRange(webList.Select(item => new PendingRemovalViewModel
            //{
            //    EntityType = "Website",
            //    RemovalReason = item.RemovalReason,
            //    Name = item.Name,
            //    Id = item.Id
            //}));


            return View(list);

        }

        public ActionResult ClearPendingRemoval(int id, string type)
        {
            switch (type.ToLower())
            {
                case "organization":
                    var org = _ctx.Organizations.Find(id);
                    org.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "person":
                    var person = _ctx.BeholderPersons.Find(id);
                    person.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "chapter":
                    var chapter = _ctx.Chapters.Find(id);
                    chapter.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "website":
                    var website = _ctx.MediaWebsiteEGroups.Find(id);
                    website.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "event":
                    var evt = _ctx.Events.Find(id);
                    evt.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "image":
                    var img = _ctx.MediaImages.Find(id);
                    img.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "audiovideo":
                    var av = _ctx.MediaAudioVideos.Find(id);
                    av.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "correspondence":
                    var c = _ctx.MediaCorrespondences.Find(id);
                    c.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "published":
                    var pub = _ctx.MediaPublisheds.Find(id);
                    pub.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "contact":
                    var contact = _ctx.Contacts.Find(id);
                    contact.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "subscription":
                    var sub = _ctx.Subscriptions.Find(id);
                    sub.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                case "vehicle":
                    var vehicle = _ctx.Vehicles.Find(id);
                    vehicle.RemovalStatusId = null;
                    _ctx.SaveChanges();

                    break;
                default:

                    break;
            }
            return RedirectToAction("PendingRemovals", "Approval");

        }

        public ActionResult ConfirmPendingRemoval(int id, string type)
        {
            switch (type.ToLower())
            {
                case "organization":
                    var org = _ctx.Organizations.Find(id);
                    org.RemovalStatusId = null;
                    _ctx.Organizations.Remove(org);
                    _ctx.SaveChanges();

                    break;
                case "person":
                    var person = _ctx.BeholderPersons.Find(id);
                    person.RemovalStatusId = null;
                    _ctx.BeholderPersons.Remove(person);
                    _ctx.SaveChanges();
                    break;
                case "chapter":
                    var chapter = _ctx.Chapters.Find(id);
                    chapter.RemovalStatusId = null;
                    _ctx.Chapters.Remove(chapter);
                    _ctx.SaveChanges();
                    break;
                case "website":
                    var website = _ctx.MediaWebsiteEGroups.Find(id);
                    website.RemovalStatusId = null;
                    _ctx.MediaWebsiteEGroups.Remove(website);
                    _ctx.SaveChanges();
                    break;
                case "event":
                    var evt = _ctx.Events.Find(id);
                    evt.RemovalStatusId = null;
                    _ctx.Events.Remove(evt);
                    _ctx.SaveChanges();
                    break;
                case "image":
                    var img = _ctx.MediaImages.Find(id);
                    img.RemovalStatusId = null;
                    _ctx.MediaImages.Remove(img);
                    _ctx.SaveChanges();
                    break;
                case "audiovideo":
                    var av = _ctx.MediaAudioVideos.Find(id);
                    av.RemovalStatusId = null;
                    _ctx.MediaAudioVideos.Remove(av);
                    _ctx.SaveChanges();
                    break;
                case "correspondence":
                    var c = _ctx.MediaCorrespondences.Find(id);
                    c.RemovalStatusId = null;
                    _ctx.MediaCorrespondences.Remove(c);
                    _ctx.SaveChanges();
                    break;
                case "published":
                    var pub = _ctx.MediaPublisheds.Find(id);
                    pub.RemovalStatusId = null;
                    _ctx.MediaPublisheds.Remove(pub);
                    _ctx.SaveChanges();
                    break;
                case "contact":
                    var contact = _ctx.Contacts.Find(id);
                    contact.RemovalStatusId = null;
                    _ctx.Contacts.Remove(contact);
                    _ctx.SaveChanges();
                    break;
                case "subscription":
                    var sub = _ctx.Subscriptions.Find(id);
                    sub.RemovalStatusId = null;
                    _ctx.SaveChanges();
                    _ctx.Subscriptions.Remove(sub);
                    _ctx.SaveChanges();
                    break;
                case "vehicle":
                    var vehicle = _ctx.Vehicles.Find(id);
                    vehicle.RemovalStatusId = null;
                    _ctx.SaveChanges();
                    _ctx.Vehicles.Remove(vehicle);
                    _ctx.SaveChanges();
                    break;
                default:
                    break;
            }
            return RedirectToAction("PendingRemovals", "Approval");
        }

        public ActionResult PersonApprovals()
        {
            var page = 1;
            var pageSize = 10;

            var list = _ctx.BeholderPersons.Where(x => x.ChapterPersonRels.Any(cp => cp.ApprovalStatusId == 4)).OrderBy(p => p.CommonPerson.LName).ToList();

            return View("PersonApprovals", list);
        }

        public ActionResult GetChapterPersonApprovals()
        {
            var list = _ctx.ChapterPersonRels.Where(x => x.ApprovalStatusId == 4);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterPersonList", list);
            }
            return PartialView("_ChapterPersonList", list);
        }

        [HttpPost]
        public void ApproveChapterPerson(int id)
        {
            var rel = _ctx.ChapterPersonRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        [HttpPost]
        public JsonResult RejectChapterPerson(int id)
        {
            var rel = _ctx.ChapterPersonRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
            return Json("Approved");
        }

        public ActionResult GetOrganizationPersonApprovals()
        {
            var list = _ctx.OrganizationPersonRels.Where(x => x.ApprovalStatusId == 4);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationPersonList", list);
            }
            return PartialView("_OrganizationPersonList", list);
        }

        [HttpPost]
        public void ApproveOrganizationPerson(int id)
        {
            var rel = _ctx.OrganizationPersonRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        [HttpPost]
        public ActionResult RejectOrganizationPerson(int id)
        {
            var rel = _ctx.OrganizationPersonRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
            return Json("Approved");
        }

        public ActionResult GetPersonPersonApprovals()
        {
            var list = _approvalRepo.GetPersonPersonApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonPersonList", list);
            }
            return PartialView("_PersonPersonList", list);
        }

        public void ApprovePersonPerson(long id)
        {
            _approvalRepo.ApprovePersonPerson(id);
        }

        public ActionResult RejectPersonPerson(long id)
        {
            _approvalRepo.RejectPersonPerson(id);
            return Json("Approved");
        }

        public ActionResult GetPersonContactApprovals()
        {
            var list = _approvalRepo.GetPersonContactApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonContactList", list);
            }
            return PartialView("_PersonContactList", list);
        }

        public void ApprovePersonContact(long id)
        {
            _approvalRepo.ApprovePersonContactApprovals(id);
        }

        public ActionResult RejectPersonContact(long id)
        {
            _approvalRepo.RejectPersonContactApprovals(id);
            return Json("Approved");
        }


        public ActionResult GetPersonCorrespondenceApprovals()
        {
            var list = _approvalRepo.GetPersonCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonCorrespondenceList", list);
            }
            return PartialView("_PersonCorrespondenceList", list);
        }

        public void ApprovePersonCorrespondence(long id)
        {
            _approvalRepo.ApprovePersonCorrespondence(id);
        }

        public ActionResult RejectPersonCorrespondence(long id)
        {
            _approvalRepo.RejectPersonCorrespondence(id);
            return Json("Approved");
        }


        public ActionResult GetPersonEventApprovals()
        {
            var list = _approvalRepo.GetPersonEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonEventList", list);
            }
            return PartialView("_PersonEventList", list);
        }

        public void ApprovePersonEvent(int id)
        {
            _approvalRepo.ApprovePersonEvent(id);
        }

        public ActionResult RejectPersonEvent(int id)
        {
            _approvalRepo.RejectPersonEvent(id);
            return Json("Approved");
        }

        public ActionResult GetPersonPublishedApprovals()
        {
            var list = _approvalRepo.GetPersonPublishedApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonPublishedList", list);
            }
            return PartialView("_PersonPublishedList", list);
        }

        public void ApprovePersonPublished(int id)
        {
            _approvalRepo.ApprovePersonPublished(id);
        }

        public ActionResult RejectPersonPublished(int id)
        {
            _approvalRepo.RejectPersonPublished(id);
            return Json("Approved");
        }

        public ActionResult GetPersonVehicleApprovals()
        {
            var list = _approvalRepo.GetPersonVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonVehicleList", list);
            }
            return PartialView("_PersonVehicleList", list);
        }

        public void ApprovePersonVehicle(int id)
        {
            _approvalRepo.ApprovePersonVehicle(id);
        }

        public ActionResult RejectPersonVehicle(int id)
        {
            _approvalRepo.RejectPersonVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetPersonMediaImageApprovals()
        {
            var list = _approvalRepo.GetPersonMediaImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaImageList", list);
            }
            return PartialView("_PersonMediaImageList", list);
        }

        public void ApprovePersonMediaImage(long id)
        {
            _approvalRepo.ApprovePersonMediaImage(id);
        }

        public ActionResult RejectPersonMediaImage(long id)
        {
            _approvalRepo.RejectPersonMediaImage(id);
            return Json("Approved");
        }

        public ActionResult GetPersonMediaAudioVideoApprovals()
        {
            var list = _approvalRepo.GetPersonMediaAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonMediaAudioVideoImageList", list);
            }
            return PartialView("_PersonMediaAudioVideoImageList", list);
        }

        public void ApprovePersonMediaAudioVideo(long id)
        {
            _approvalRepo.ApprovePersonMediaAudioVideo(id);
        }

        public ActionResult RejectPersonMediaAudioVideo(long id)
        {
            _approvalRepo.RejectPersonMediaAudioVideo(id);
            return Json("Approved");
        }

        public ActionResult GetChapterOrganizationApprovals()
        {
            var list = _approvalRepo.GetChapterOrganizationApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterOrganizationList", list);
            }
            return PartialView("_ChapterOrganizationList", list);
        }

        public void ApproveChapterOrganization(int id)
        {
            _approvalRepo.ApproveChapterOrganization(id);
        }

        public ActionResult RejectChapterOrganization(int id)
        {
            _approvalRepo.RejectChapterOrganization(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationOrganizationApprovals()
        {
            var list = _approvalRepo.GetOrganizationOrganizationApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationOrganizationList", list);
            }
            return PartialView("_OrganizationOrganizationList", list);
        }

        public void ApproveOrganizationOrganization(int id)
        {
            _approvalRepo.ApproveOrganizationOrganization(id);
        }

        public ActionResult RejectOrganizationOrganization(int id)
        {
            _approvalRepo.RejectOrganizationOrganization(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationEventApprovals()
        {
            var list = _approvalRepo.GetOrganizationEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationEventList", list);
            }
            return PartialView("_OrganizationEventList", list);
        }

        public void ApproveOrganizationEvent(int id)
        {
            _approvalRepo.ApproveOrganizationEvent(id);
        }

        public ActionResult RejectOrganizationEvent(int id)
        {
            _approvalRepo.RejectOrganizationEvent(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationVehicleApprovals()
        {
            var list = _approvalRepo.GetOrganizationVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationVehicleList", list);
            }
            return PartialView("_OrganizationVehicleList", list);
        }

        public void ApproveOrganizationVehicle(int id)
        {
            _approvalRepo.ApproveOrganizationVehicle(id);
        }

        public ActionResult RejectOrganizationVehicle(int id)
        {
            _approvalRepo.RejectOrganizationVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationMediaAudioVideoApprovals()
        {
            var list = _approvalRepo.GetOrganizationMediaAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaAudioVideoList", list);
            }
            return PartialView("_OrganizationMediaAudioVideoList", list);
        }

        public void ApproveOrganizationMediaAudioVideo(int id)
        {
            _approvalRepo.ApproveOrganizationMediaAudioVideo(id);
        }

        public ActionResult RejectOrganizationMediaAudioVideo(int id)
        {
            _approvalRepo.RejectOrganizationMediaAudioVideo(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationMediaImageApprovals()
        {
            var list = _approvalRepo.GetOrganizationMediaImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationMediaImageList", list);
            }
            return PartialView("_OrganizationMediaImageList", list);
        }

        public void ApproveOrganizationMediaImage(int id)
        {
            _approvalRepo.ApproveOrganizationMediaImage(id);
        }

        public ActionResult RejectOrganizationMediaImage(int id)
        {
            _approvalRepo.RejectOrganizationMediaImage(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationCorrespondenceApprovals()
        {
            var list = _approvalRepo.OrganizationCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationCorrespondenceList", list);
            }
            return PartialView("_OrganizationCorrespondenceList", list);
        }

        public void ApproveOrganizationCorrespondence(int id)
        {
            _approvalRepo.ApproveOrganizationCorrespondence(id);
        }

        public ActionResult RejectOrganizationCorrespondence(int id)
        {
            _approvalRepo.RejectOrganizationCorrespondence(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationPublishedApprovals()
        {
            var list = _approvalRepo.OrganizationPublishedApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationPublishedList", list);
            }
            return PartialView("_OrganizationPublishedList", list);
        }

        public void ApproveOrganizationPublished(int id)
        {
            _approvalRepo.ApproveOrganizationPublished(id);
        }

        public ActionResult RejectOrganizationPublished(int id)
        {
            _approvalRepo.RejectOrganizationPublished(id);
            return Json("Approved");
        }

        public ActionResult GetOrganizationSubscriptionApprovals()
        {
            var list = _approvalRepo.OrganizationSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationSubscriptionList", list);
            }
            return PartialView("_OrganizationSubscriptionList", list);
        }

        public void ApproveOrganizationSubscription(int id)
        {
            _approvalRepo.ApproveOrganizationSubscription(id);
        }

        public ActionResult RejectOrganizationSubscription(int id)
        {
            _approvalRepo.RejectOrganizationSubscription(id);
            return Json("Approved");
        }

        public ActionResult GetChapterChapterApprovals()
        {
            var list = _approvalRepo.GetChapterChapterApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterChapterList", list);
            }
            return PartialView("_ChapterChapterList", list);
        }

        public void ApproveChapterChapter(int id)
        {
            _approvalRepo.ApproveChapterChapter(id);
        }

        public ActionResult RejectChapterChapter(int id)
        {
            _approvalRepo.RejectChapterChapter(id);
            return Json("Approved");
        }

        public ActionResult GetChapterEventApprovals()
        {
            var list = _approvalRepo.GetChapterEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterEventList", list);
            }
            return PartialView("_ChapterEventList", list);
        }

        public void ApproveChapterEvent(int id)
        {
            _approvalRepo.ApproveChapterEvent(id);
        }

        public ActionResult RejectChapterEvent(int id)
        {
            _approvalRepo.RejectChapterEvent(id);
            return Json("Approved");
        }

        public ActionResult GetChapterVehicleApprovals()
        {
            var list = _approvalRepo.GetChapterVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterVehicleList", list);
            }
            return PartialView("_ChapterVehicleList", list);
        }

        public void ApproveChapterVehicle(int id)
        {
            _approvalRepo.ApproveChapterVehicle(id);
        }

        public ActionResult RejectChapterVehicle(int id)
        {
            _approvalRepo.RejectChapterVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetChapterMediaAudioVideoApprovals()
        {
            var list = _approvalRepo.GetChapterMediaAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaAudioVideoList", list);
            }
            return PartialView("_ChapterMediaAudioVideoList", list);
        }

        public void ApproveChapterMediaAudioVideo(int id)
        {
            _approvalRepo.ApproveChapterMediaAudioVideo(id);
        }

        public ActionResult RejectChapterMediaAudioVideo(int id)
        {
            _approvalRepo.RejectChapterMediaAudioVideo(id);
            return Json("Approved");
        }

        public ActionResult GetChapterMediaImageApprovals()
        {
            var list = _approvalRepo.GetChapterMediaImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaImageList", list);
            }
            return PartialView("_ChapterMediaImageList", list);
        }

        public void ApproveChapterMediaImage(int id)
        {
            _approvalRepo.ApproveChapterMediaImage(id);
        }

        public ActionResult RejectChapterMediaImage(int id)
        {
            _approvalRepo.RejectChapterMediaImage(id);
            return Json("Approved");
        }

        public ActionResult GetVehicleVehicleApprovals()
        {
            var list = _approvalRepo.GetVehicleVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleVehicleList", list);
            }
            return PartialView("_VehicleVehicleList", list);
        }

        public void ApproveVehicleVehicle(int id)
        {
            _approvalRepo.ApproveVehicleVehicle(id);
        }

        public ActionResult RejectVehicleVehicle(int id)
        {
            _approvalRepo.RejectVehicleVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetEventVehicleApprovals()
        {
            var list = _approvalRepo.GetEventVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventVehicleList", list);
            }
            return PartialView("_EventVehicleList", list);
        }

        public void ApproveEventVehicle(int id)
        {
            _approvalRepo.ApproveEventVehicle(id);
        }

        public ActionResult RejectEventVehicle(int id)
        {
            _approvalRepo.RejectEventVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetMediaAudioVideoVehicleApprovals()
        {
            var list = _approvalRepo.GetMediaAudioVideoVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaAudioVideoVehicleList", list);
            }
            return PartialView("_MediaAudioVideoVehicleList", list);
        }

        public void ApproveMediaAudioVideoVehicle(int id)
        {
            _approvalRepo.ApproveMediaAudioVideoVehicle(id);
        }

        public ActionResult RejectMediaAudioVideoVehicle(int id)
        {
            _approvalRepo.RejectMediaAudioVideoVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetMediaImageVehicleApprovals()
        {
            var list = _approvalRepo.GetMediaImageVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageVehicleList", list);
            }
            return PartialView("_MediaImageVehicleList", list);
        }

        public void ApproveMediaImageVehicle(int id)
        {
            _approvalRepo.ApproveMediaImageVehicle(id);
        }

        public ActionResult RejectMediaImageVehicle(int id)
        {
            _approvalRepo.RejectMediaImageVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetMediaImageMediaAudioVideoApprovals()
        {
            var list = _approvalRepo.GetMediaImageMediaAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageMediaAudioVideoList", list);
            }
            return PartialView("_MediaImageMediaAudioVideoList", list);
        }

        public void ApproveMediaImageMediaAudioVideo(int id)
        {
            _approvalRepo.ApproveMediaImageMediaAudioVideo(id);
        }

        public ActionResult RejectMediaImageMediaAudioVideo(int id)
        {
            _approvalRepo.RejectMediaImageMediaAudioVideo(id);
            return Json("Approved");
        }

        //ImageImage
        public ActionResult GetImageImageApprovals()
        {
            var list = _approvalRepo.GetImageImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ImageImageList", list);
            }
            return PartialView("_ImageImageList", list);
        }

        public void ApproveImageImage(int id)
        {
            _approvalRepo.ApproveImageImage(id);
        }

        public ActionResult RejectImageImage(int id)
        {
            _approvalRepo.RejectImageImage(id);
            return Json("Approved");
        }

        //ImageSubscription
        public ActionResult GetImageSubscriptionApprovals()
        {
            var list = _approvalRepo.GetImageSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ImageSubscriptionList", list);
            }
            return PartialView("_ImageSubscriptionList", list);
        }

        public void ApproveImageSubscription(int id)
        {
            _approvalRepo.ApproveImageSubscription(id);
        }

        public ActionResult RejectImageSubscription(int id)
        {
            _approvalRepo.RejectImageSubscription(id);
            return Json("Approved");
        }


        public ActionResult GetChapterMediaCorrespondenceApprovals()
        {
            var list = _approvalRepo.GetChapterMediaCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaCorrespondenceList", list);
            }
            return PartialView("_ChapterMediaCorrespondenceList", list);
        }

        public void ApproveChapterMediaCorrespondence(int id)
        {
            _approvalRepo.ApproveChapterMediaCorrespondence(id);
        }

        public ActionResult RejectChapterMediaCorrespondence(int id)
        {
            _approvalRepo.RejectChapterMediaCorrespondence(id);
            return Json("Approved");
        }

        public ActionResult GetChapterMediaPublishedApprovals()
        {
            var list = _approvalRepo.GetChapterMediaPublishedApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterMediaPublishedList", list);
            }
            return PartialView("_ChapterMediaPublishedList", list);
        }

        public void ApproveChapterMediaPublished(int id)
        {
            _approvalRepo.ApproveChapterMediaPublished(id);
        }

        public ActionResult RejectChapterMediaPublished(int id)
        {
            _approvalRepo.RejectChapterMediaPublished(id);
            return Json("Approved");
        }

        public ActionResult GetChapterSubscriptionApprovals()
        {
            var list = _approvalRepo.GetChapterSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterSubscriptionList", list);
            }
            return PartialView("_ChapterSubscriptionList", list);
        }

        public void ApproveChapterSubscription(int id)
        {
            _approvalRepo.ApproveChapterSubscription(id);
        }

        public ActionResult RejectChapterSubscription(int id)
        {
            _approvalRepo.RejectChapterSubscription(id);
            return Json("Approved");
        }

        public ActionResult GetContactContactApprovals()
        {
            var list = _approvalRepo.GetContactContactApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactContactList", list);
            }
            return PartialView("_ContactContactList", list);
        }

        public void ApproveContactContact(int id)
        {
            _approvalRepo.ApproveContactContact(id);
        }

        public ActionResult RejectContactContact(int id)
        {
            _approvalRepo.RejectContactContact(id);
            return Json("Approved");
        }

        public ActionResult GetContactMediaCorrespondenceApprovals()
        {
            var list = _approvalRepo.GetContactMediaCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactMediaCorrespondenceList", list);
            }
            return PartialView("_ContactMediaCorrespondenceList", list);
        }

        public void ApproveContactMediaCorrespondence(int id)
        {
            _approvalRepo.ApproveContactMediaCorrespondence(id);
        }

        public ActionResult RejectContactMediaCorrespondence(int id)
        {
            _approvalRepo.RejectContactMediaCorrespondence(id);
            return Json("Approved");
        }

        public ActionResult GetContactImageApprovals()
        {
            var list = _approvalRepo.GetContactImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactImageList", list);
            }
            return PartialView("_ContactImageList", list);
        }

        public void ApproveContactImage(int id)
        {
            _approvalRepo.ApproveContactImage(id);
        }

        public ActionResult RejectContactImage(int id)
        {
            _approvalRepo.RejectContactImage(id);
            return Json("Approved");
        }

        //public ActionResult GetContactPublishedApprovals()
        //{
        //    var list = _approvalRepo.GetContactPublishedApprovals();
        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_ContactPublishedList", list);
        //    }
        //    return PartialView("_ContactPublishedList", list);
        //}

        //public void ApproveContactPublished(int id)
        //{
        //    _approvalRepo.ApproveContactPublished(id);
        //}

        //public ActionResult RejectContactPublished(int id)
        //{
        //    _approvalRepo.RejectContactPublished(id);
        //    return Json("Approved");
        //}

        public ActionResult GetContactSubscriptionApprovals()
        {
            var list = _approvalRepo.GetContactSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactSubscriptionList", list);
            }
            return PartialView("_ContactSubscriptionList", list);
        }

        public void ApproveContactSubscription(int id)
        {
            _approvalRepo.ApproveContactSubscription(id);
        }

        public ActionResult RejectContactSubscription(int id)
        {
            _approvalRepo.RejectContactSubscription(id);
            return Json("Approved");
        }

        public ActionResult GetEventEventApprovals()
        {
            var list = _approvalRepo.GetEventEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventEventList", list);
            }
            return PartialView("_EventEventList", list);
        }

        public void ApproveEventEvent(int id)
        {
            _approvalRepo.ApproveEventEvent(id);
        }

        public ActionResult RejectEventEvent(int id)
        {
            _approvalRepo.RejectEventEvent(id);
            return Json("Approved");
        }

        public ActionResult GetEventAudioVideoApprovals()
        {
            var list = _approvalRepo.GetEventAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventAudioVideoList", list);
            }
            return PartialView("_EventAudioVideoList", list);
        }

        public void ApproveEventAudioVideo(int id)
        {
            _approvalRepo.ApproveEventAudioVideo(id);
        }

        public ActionResult RejectEventAudioVideo(int id)
        {
            _approvalRepo.RejectEventAudioVideo(id);
            return Json("Approved");
        }

        public ActionResult GetEventImageApprovals()
        {
            var list = _approvalRepo.GetEventImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventImageList", list);
            }
            return PartialView("_EventImageList", list);
        }

        public void ApproveEventImage(int id)
        {
            _approvalRepo.ApproveEventImage(id);
        }

        public ActionResult RejectEventImage(int id)
        {
            _approvalRepo.RejectEventImage(id);
            return Json("Approved");
        }

        public ActionResult GetEventSubscriptionApprovals()
        {
            var list = _approvalRepo.GetEventSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventSubscriptionList", list);
            }
            return PartialView("_EventSubscriptionList", list);
        }

        public void ApproveEventSubscription(int id)
        {
            _approvalRepo.ApproveEventSubscription(id);
        }

        public ActionResult RejectEventSubscription(int id)
        {
            _approvalRepo.RejectEventSubscription(id);
            return Json("Approved");
        }

        public ActionResult GetSubscriptionSubscriptionApprovals()
        {
            var list = _approvalRepo.GetSubscriptionSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionSubscriptionList", list);
            }
            return PartialView("_SubscriptionSubscriptionList", list);
        }

        public void ApproveSubscriptionSubscription(int id)
        {
            _approvalRepo.ApproveSubscriptionSubscription(id);
        }

        public ActionResult RejectSubscriptionSubscription(int id)
        {
            _approvalRepo.RejectSubscriptionSubscription(id);
            return Json("Approved");
        }

        public ActionResult GetSubscriptionVehicleApprovals()
        {
            var list = _approvalRepo.GetSubscriptionVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionVehicleList", list);
            }
            return PartialView("_SubscriptionVehicleList", list);
        }

        public void ApproveSubscriptionVehicle(int id)
        {
            _approvalRepo.ApproveSubscriptionVehicle(id);
        }

        public ActionResult RejectSubscriptionVehicle(int id)
        {
            _approvalRepo.RejectSubscriptionVehicle(id);
            return Json("Approved");
        }

        public ActionResult GetCorrespondenceEventApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceEventList", list);
            }
            return PartialView("_CorrespondenceEventList", list);
        }

        public void ApproveCorrespondenceEvent(int id)
        {
            _approvalRepo.ApproveCorrespondenceEvent(id);
        }

        public ActionResult RejectCorrespondenceEvent(int id)
        {
            _approvalRepo.RejectCorrespondenceEvent(id);
            return Json("Approved");
        }

        //CorrespondenceAudioVideo
        public ActionResult GetCorrespondenceAudioVideoApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceAudioVideoList", list);
            }
            return PartialView("_CorrespondenceAudioVideoList", list);
        }

        public void ApproveCorrespondenceAudioVideo(int id)
        {
            _approvalRepo.ApproveCorrespondenceAudioVideo(id);
        }

        public ActionResult RejectCorrespondenceAudioVideo(int id)
        {
            _approvalRepo.RejectCorrespondenceAudioVideo(id);
            return Json("Approved");
        }

        //CorrespondenceCorrespondence
        public ActionResult GetCorrespondenceCorrespondenceApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceCorrespondenceList", list);
            }
            return PartialView("_CorrespondenceCorrespondenceList", list);
        }

        public void ApproveCorrespondenceCorrespondence(int id)
        {
            _approvalRepo.ApproveCorrespondenceCorrespondence(id);
        }

        public ActionResult RejectCorrespondenceCorrespondence(int id)
        {
            _approvalRepo.RejectCorrespondenceCorrespondence(id);
            return Json("Approved");
        }

        //CorrespondenceImage
        public ActionResult GetCorrespondenceImageApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceImageList", list);
            }
            return PartialView("_CorrespondenceImageList", list);
        }

        public void ApproveCorrespondenceImage(int id)
        {
            _approvalRepo.ApproveCorrespondenceImage(id);
        }

        public ActionResult RejectCorrespondenceImage(int id)
        {
            _approvalRepo.RejectCorrespondenceImage(id);
            return Json("Approved");
        }

        //CorrespondenceSubscription
        public ActionResult GetCorrespondenceSubscriptionApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceSubscriptionList", list);
            }
            return PartialView("_CorrespondenceSubscriptionList", list);
        }

        public void ApproveCorrespondenceSubscription(int id)
        {
            _approvalRepo.ApproveCorrespondenceSubscription(id);
        }

        public ActionResult RejectCorrespondenceSubscription(int id)
        {
            _approvalRepo.RejectCorrespondenceSubscription(id);
            return Json("Approved");
        }

        //CorrespondenceVehicle
        public ActionResult GetCorrespondenceVehicleApprovals()
        {
            var list = _approvalRepo.GetCorrespondenceVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceVehicleList", list);
            }
            return PartialView("_CorrespondenceVehicleList", list);
        }

        public void ApproveCorrespondenceVehicle(int id)
        {
            _approvalRepo.ApproveCorrespondenceVehicle(id);
        }

        public ActionResult RejectCorrespondenceVehicle(int id)
        {
            _approvalRepo.RejectCorrespondenceVehicle(id);
            return Json("Approved");
        }

        //PublishedEvent
        public ActionResult GetPublishedEventApprovals()
        {
            var list = _approvalRepo.GetPublishedEventApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedEventList", list);
            }
            return PartialView("_PublishedEventList", list);
        }

        public void ApprovePublishedEvent(int id)
        {
            _approvalRepo.ApprovePublishedEvent(id);
        }

        public ActionResult RejectPublishedEvent(int id)
        {
            _approvalRepo.RejectPublishedEvent(id);
            return Json("Approved");
        }

        //PublishedAudioVideo
        public ActionResult GetPublishedAudioVideoApprovals()
        {
            var list = _approvalRepo.GetPublishedAudioVideoApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedAudioVideoList", list);
            }
            return PartialView("_PublishedAudioVideoList", list);
        }

        public void ApprovePublishedAudioVideo(int id)
        {
            _approvalRepo.ApprovePublishedAudioVideo(id);
        }

        public ActionResult RejectPublishedAudioVideo(int id)
        {
            _approvalRepo.RejectPublishedAudioVideo(id);
            return Json("Approved");
        }

        //PublishedCorrespondence
        public ActionResult GetPublishedCorrespondenceApprovals()
        {
            var list = _approvalRepo.GetPublishedCorrespondenceApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedCorrespondenceList", list);
            }
            return PartialView("_PublishedCorrespondenceList", list);
        }

        public void ApprovePublishedCorrespondence(int id)
        {
            _approvalRepo.ApprovePublishedCorrespondence(id);
        }

        public ActionResult RejectPublishedCorrespondence(int id)
        {
            _approvalRepo.RejectPublishedCorrespondence(id);
            return Json("Approved");
        }

        //PublishedImage
        public ActionResult GetPublishedImageApprovals()
        {
            var list = _approvalRepo.GetPublishedImageApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedImageList", list);
            }
            return PartialView("_PublishedImageList", list);
        }

        public void ApprovePublishedImage(int id)
        {
            _approvalRepo.ApprovePublishedImage(id);
        }

        public ActionResult RejectPublishedImage(int id)
        {
            _approvalRepo.RejectPublishedImage(id);
            return Json("Approved");
        }

        //PublishedPublished
        public ActionResult GetPublishedPublishedApprovals()
        {
            var list = _approvalRepo.GetPublishedPublishedApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedPublishedList", list);
            }
            return PartialView("_PublishedPublishedList", list);
        }

        public void ApprovePublishedPublished(int id)
        {
            _approvalRepo.ApprovePublishedPublished(id);
        }

        public ActionResult RejectPublishedPublished(int id)
        {
            _approvalRepo.RejectPublishedPublished(id);
            return Json("Approved");
        }

        //PublishedWebsiteEGroup
        public ActionResult GetPublishedWebsiteEGroupApprovals()
        {
            var list = _approvalRepo.GetPublishedWebsiteEGroupApprovals();
            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_PublishedWebsiteEGroupList", list);
            //}
            //return PartialView("_PublishedWebsiteEGroupList", list);
        }

        //public void ApprovePublishedWebsiteEGroup(int id)
        //{
        //    _approvalRepo.ApprovePublishedWebsiteEGroup(id);
        //}

        //public ActionResult RejectPublishedWebsiteEGroup(int id)
        //{
        //    _approvalRepo.RejectPublishedWebsiteEGroup(id);
        //    return Json("Approved");
        //}

        //PublishedSubscription
        public ActionResult GetPublishedSubscriptionApprovals()
        {
            var list = _approvalRepo.GetPublishedSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedSubscriptionList", list);
            }
            return PartialView("_PublishedSubscriptionList", list);
        }

        public void ApprovePublishedSubscription(int id)
        {
            _approvalRepo.ApprovePublishedSubscription(id);
        }

        public ActionResult RejectPublishedSubscription(int id)
        {
            _approvalRepo.RejectPublishedSubscription(id);
            return Json("Approved");
        }

        //PublishedVehicle
        public ActionResult GetPublishedVehicleApprovals()
        {
            var list = _approvalRepo.GetPublishedVehicleApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedVehicleList", list);
            }
            return PartialView("_PublishedVehicleList", list);
        }

        public void ApprovePublishedVehicle(int id)
        {
            _approvalRepo.ApprovePublishedVehicle(id);
        }

        public ActionResult RejectPublishedVehicle(int id)
        {
            _approvalRepo.RejectPublishedVehicle(id);
            return Json("Approved");
        }

        //WebsiteEGroupSubscription
        public ActionResult GetWebsiteEGroupSubscriptionApprovals()
        {
            var list = _approvalRepo.GetWebsiteEGroupSubscriptionApprovals();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_WebsiteEGroupSubscriptionList", list);
            }
            return PartialView("_WebsiteEGroupSubscriptionList", list);
        }

        public void ApproveWebsiteEGroupSubscription(int id)
        {
            _approvalRepo.ApproveWebsiteEGroupSubscription(id);
        }

        public ActionResult RejectWebsiteEGroupSubscription(int id)
        {
            _approvalRepo.RejectWebsiteEGroupSubscription(id);
            return Json("Approved");
        }

    }

}
