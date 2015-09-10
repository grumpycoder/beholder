using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using splc.data.repository;
using splc.domain.Models;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentRepository _commentRepo;

        public CommentsController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        #region Person

        public ActionResult GetPersonComments(int id)
        {
            ViewBag.PersonId = id;
            ViewBag.BeholderPersonId = id;
            var comments = _commentRepo.GetPersonComments(p => p.BeholderPersonId.Equals(id)).ToArray();
            return PartialView("_PersonCommentList", comments);
        }

        public ActionResult CreatePersonComment(int personId)
        {
            var comment = new PersonComment() { BeholderPersonId = personId };
            return PartialView("_CreateOrEditPersonComment", comment);
        }

        [HttpPost]
        public ActionResult CreatePersonComment(PersonComment personcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdatePersonComments(personcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditPersonComment(int id)
        {
            var comment = _commentRepo.GetPersonComment(id);
            return PartialView("_CreateOrEditPersonComment", comment);
        }

        [HttpPost]
        public ActionResult EditPersonComment(PersonComment personcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdatePersonComments(personcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeletePersonComment(int id)
        {
            _commentRepo.DeletePersonComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Organization

        public ActionResult GetOrganizationComments(int id)
        {
            ViewBag.OrganizationId = id;
            var comments = _commentRepo.GetOrganizationComments(p => p.OrganizationId.Equals(id)).ToArray();
            return PartialView("_OrganizationCommentList", comments);
        }

        public ActionResult CreateOrganizationComment(int organizationId)
        {
            var comment = new OrganizationComment { OrganizationId = organizationId };
            return PartialView("_CreateOrEditOrganizationComment", comment);
        }

        [HttpPost]
        public ActionResult CreateOrganizationComment(OrganizationComment organizationcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateOrganizationComments(organizationcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditOrganizationComment(int id)
        {
            var comment = _commentRepo.GetOrganizationComment(id);
            return PartialView("_CreateOrEditOrganizationComment", comment);
        }

        [HttpPost]
        public ActionResult EditOrganizationComment(OrganizationComment organizationcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateOrganizationComments(organizationcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteOrganizationComment(int id)
        {
            _commentRepo.DeleteOrganizationComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Chapter

        public ActionResult GetChapterComments(int id)
        {
            ViewBag.ChapterId = id;
            var comments = _commentRepo.GetChapterComments(p => p.ChapterId.Equals(id)).ToArray();
            return PartialView("_ChapterCommentList", comments);
        }

        public ActionResult CreateChapterComment(int chapterId)
        {
            var comment = new ChapterComment() { ChapterId = chapterId };
            return PartialView("_CreateOrEditChapterComment", comment);
        }

        [HttpPost]
        public ActionResult CreateChapterComment(ChapterComment chaptercomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateChapterComments(chaptercomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditChapterComment(int id)
        {
            var comment = _commentRepo.GetChapterComment(id);
            return PartialView("_CreateOrEditChapterComment", comment);
        }

        [HttpPost]
        public ActionResult EditChapterComment(ChapterComment chaptercomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateChapterComments(chaptercomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteChapterComment(int id)
        {
            _commentRepo.DeleteChapterComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Events

        public ActionResult GetEventComments(int id)
        {
            ViewBag.EventId = id;
            var comments = _commentRepo.GetEventComments(c => c.EventId.Equals(id)).ToArray();
            return PartialView("_EventCommentList", comments);
        }

        public ActionResult CreateEventComment(int eventId)
        {
            var comment = new EventComment() { EventId = eventId };
            return PartialView("_CreateOrEditEventComment", comment);
        }

        [HttpPost]
        public ActionResult CreateEventComment(EventComment eventcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateEventComments(eventcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditEventComment(int id)
        {
            var comment = _commentRepo.GetEventComment(id);
            return PartialView("_CreateOrEditEventComment", comment);
        }

        [HttpPost]
        public ActionResult EditEventComment(EventComment eventcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateEventComments(eventcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteEventComment(int id)
        {
            _commentRepo.DeleteEventComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Media Image

        public ActionResult GetMediaImageComments(int id)
        {
            ViewBag.MediaImageId = id;
            var comments = _commentRepo.GetMediaImageComments(p => p.MediaImageId.Equals(id)).ToArray();
            return PartialView("_MediaImageCommentList", comments);
        }

        public ActionResult CreateMediaImageComment(int mediaImageId)
        {
            var comment = new MediaImageComment() { MediaImageId = mediaImageId };
            return PartialView("_CreateOrEditMediaImageComment", comment);
        }

        [HttpPost]
        public ActionResult CreateMediaImageComment(MediaImageComment mediaImagecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaImageComments(mediaImagecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditMediaImageComment(int id)
        {
            var comment = _commentRepo.GetMediaImageComment(id);
            return PartialView("_CreateOrEditMediaImageComment", comment);
        }

        [HttpPost]
        public ActionResult EditMediaImageComment(MediaImageComment mediaImagecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaImageComments(mediaImagecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteMediaImageComment(int id)
        {
            _commentRepo.DeleteMediaImageComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Media Audio Video

        public ActionResult GetMediaAudioVideoComments(int id)
        {
            ViewBag.MediaAudioVideoId = id;
            var comments = _commentRepo.GetMediaAudioVideoComments(p => p.MediaAudioVideoId.Equals(id)).ToArray();
            return PartialView("_MediaAudioVideoCommentList", comments);
        }

        public ActionResult CreateMediaAudioVideoComment(int mediaaudiovideoid)
        {
            var comment = new MediaAudioVideoComment() { MediaAudioVideoId = mediaaudiovideoid };
            return PartialView("_CreateOrEditMediaAudioVideoComment", comment);
        }

        [HttpPost]
        public ActionResult CreateMediaAudioVideoComment(MediaAudioVideoComment mediaAudioVideocomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaAudioVideoComments(mediaAudioVideocomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditMediaAudioVideoComment(int id)
        {
            var comment = _commentRepo.GetMediaAudioVideoComment(id);
            return PartialView("_CreateOrEditMediaAudioVideoComment", comment);
        }

        [HttpPost]
        public ActionResult EditMediaAudioVideoComment(MediaAudioVideoComment mediaAudioVideocomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaAudioVideoComments(mediaAudioVideocomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteMediaAudioVideoComment(int id)
        {
            _commentRepo.DeleteMediaAudioVideoComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Vehicle

        public ActionResult GetVehicleComments(int id)
        {
            ViewBag.VehicleId = id;
            var comments = _commentRepo.GetVehicleComments(c => c.VehicleId.Equals(id)).ToArray();
            return PartialView("_VehicleCommentList", comments);
        }

        public ActionResult CreateVehicleComment(int vehicleId)
        {
            var comment = new VehicleComment() { VehicleId = vehicleId };
            return PartialView("_CreateOrEditVehicleComment", comment);
        }

        [HttpPost]
        public ActionResult CreateVehicleComment(VehicleComment vehiclecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateVehicleComments(vehiclecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditVehicleComment(int id)
        {
            var comment = _commentRepo.GetVehicleComment(id);
            return PartialView("_CreateOrEditVehicleComment", comment);
        }

        [HttpPost]
        public ActionResult EditVehicleComment(VehicleComment vehiclecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateVehicleComments(vehiclecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteVehicleComment(int id)
        {
            _commentRepo.DeleteVehicleComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Contact

        public ActionResult GetContactComments(int id)
        {
            ViewBag.ContactId = id;
            var comments = _commentRepo.GetContactComments(p => p.ContactId.Equals(id)).ToArray();
            return PartialView("_ContactCommentList", comments);
        }

        public ActionResult CreateContactComment(int contactId)
        {
            var comment = new ContactComment() { ContactId = contactId };
            return PartialView("_CreateOrEditContactComment", comment);
        }

        [HttpPost]
        public ActionResult CreateContactComment(ContactComment contactcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateContactComments(contactcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditContactComment(int id)
        {
            var comment = _commentRepo.GetContactComment(id);
            return PartialView("_CreateOrEditContactComment", comment);
        }

        [HttpPost]
        public ActionResult EditContactComment(ContactComment contactcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateContactComments(contactcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteContactComment(int id)
        {
            _commentRepo.DeleteContactComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Media Correspondence

        public ActionResult GetMediaCorrespondenceComments(int id)
        {
            ViewBag.MediaCorrespondenceId = id;
            var comments = _commentRepo.GetMediaCorrespondenceComments(p => p.MediaCorrespondenceId.Equals(id)).ToArray();
            return PartialView("_MediaCorrespondenceCommentList", comments);
        }

        public ActionResult CreateMediaCorrespondenceComment(int mediaCorrespondenceId)
        {
            var comment = new MediaCorrespondenceComment() { MediaCorrespondenceId = mediaCorrespondenceId };
            return PartialView("_CreateOrEditMediaCorrespondenceComment", comment);
        }

        [HttpPost]
        public ActionResult CreateMediaCorrespondenceComment(MediaCorrespondenceComment mediaCorrespondencecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaCorrespondenceComments(mediaCorrespondencecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditMediaCorrespondenceComment(int id)
        {
            var comment = _commentRepo.GetMediaCorrespondenceComment(id);
            return PartialView("_CreateOrEditMediaCorrespondenceComment", comment);
        }

        [HttpPost]
        public ActionResult EditMediaCorrespondenceComment(MediaCorrespondenceComment mediaCorrespondencecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaCorrespondenceComments(mediaCorrespondencecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceComment(int id)
        {
            _commentRepo.DeleteMediaCorrespondenceComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Media Published

        public ActionResult GetMediaPublishedComments(int id)
        {
            ViewBag.MediaPublishedId = id;
            var comments = _commentRepo.GetMediaPublishedComments(p => p.MediaPublishedId.Equals(id)).ToArray();
            return PartialView("_MediaPublishedCommentList", comments);
        }

        public ActionResult CreateMediaPublishedComment(int mediaPublishedId)
        {
            var comment = new MediaPublishedComment() { MediaPublishedId = mediaPublishedId };
            return PartialView("_CreateOrEditMediaPublishedComment", comment);
        }

        [HttpPost]
        public ActionResult CreateMediaPublishedComment(MediaPublishedComment mediaPublishedcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaPublishedComments(mediaPublishedcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditMediaPublishedComment(int id)
        {
            var comment = _commentRepo.GetMediaPublishedComment(id);
            return PartialView("_CreateOrEditMediaPublishedComment", comment);
        }

        [HttpPost]
        public ActionResult EditMediaPublishedComment(MediaPublishedComment mediaPublishedcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaPublishedComments(mediaPublishedcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteMediaPublishedComment(int id)
        {
            _commentRepo.DeleteMediaPublishedComment(id);
            _commentRepo.Save();
        }

        #endregion


        #region Subscription

        public ActionResult GetSubscriptionComments(int id)
        {
            ViewBag.SubscriptionId = id;
            var comments = _commentRepo.GetSubscriptionComments(p => p.SubscriptionId.Equals(id)).ToArray();
            return PartialView("_SubscriptionCommentList", comments);
        }

        public ActionResult CreateSubscriptionComment(int subscriptionId)
        {
            var comment = new SubscriptionComment { SubscriptionId = subscriptionId };
            return PartialView("_CreateOrEditSubscriptionComment", comment);
        }

        [HttpPost]
        public ActionResult CreateSubscriptionComment(SubscriptionComment subscriptioncomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateSubscriptionComments(subscriptioncomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditSubscriptionComment(int id)
        {
            var comment = _commentRepo.GetSubscriptionComment(id);
            return PartialView("_CreateOrEditSubscriptionComment", comment);
        }

        [HttpPost]
        public ActionResult EditSubscriptionComment(SubscriptionComment subscriptioncomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateSubscriptionComments(subscriptioncomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteSubscriptionComment(int id)
        {
            _commentRepo.DeleteSubscriptionComment(id);
            _commentRepo.Save();
        }

        #endregion

        #region MediaWebsiteEGroup

        public ActionResult GetMediaWebsiteEGroupComments(int id)
        {
            ViewBag.MediaWebsiteEGroupId = id;
            ViewBag.BeholderMediaWebsiteEGroupId = id;
            var comments = _commentRepo.GetMediaWebsiteEGroupComments(p => p.MediaWebsiteEGroupId.Equals(id)).ToArray();
            return PartialView("_MediaWebsiteEGroupCommentList", comments);
        }

        public ActionResult CreateMediaWebsiteEGroupComment(int mediaWebsiteEGroupId)
        {
            var comment = new MediaWebsiteEGroupComment() { MediaWebsiteEGroupId = mediaWebsiteEGroupId };
            return PartialView("_CreateOrEditMediaWebsiteEGroupComment", comment);
        }

        [HttpPost]
        public ActionResult CreateMediaWebsiteEGroupComment(MediaWebsiteEGroupComment MediaWebsiteEGroupcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaWebsiteEGroupComments(MediaWebsiteEGroupcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditMediaWebsiteEGroupComment(int id)
        {
            var comment = _commentRepo.GetMediaWebsiteEGroupComment(id);
            return PartialView("_CreateOrEditMediaWebsiteEGroupComment", comment);
        }

        [HttpPost]
        public ActionResult EditMediaWebsiteEGroupComment(MediaWebsiteEGroupComment MediaWebsiteEGroupcomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateMediaWebsiteEGroupComments(MediaWebsiteEGroupcomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteMediaWebsiteEGroupComment(int id)
        {
            _commentRepo.DeleteMediaWebsiteEGroupComment(id);
            _commentRepo.Save();
        }

        #endregion

        #region NewsSource

        public ActionResult GetNewsSourceComments(int id)
        {
            ViewBag.NewsSourceId = id;
            var comments = _commentRepo.GetNewsSourceComments(p => p.NewsSourceId.Equals(id)).ToArray();
            return PartialView("_NewsSourceCommentList", comments);
        }

        public ActionResult CreateNewsSourceComment(int newssourceid)
        {
            var comment = new NewsSourceComment() { NewsSourceId = newssourceid };
            return PartialView("_CreateOrEditNewsSourceComment", comment);
        }

        [HttpPost]
        public ActionResult CreateNewsSourceComment(NewsSourceComment newsSourceComment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateNewsSourceComments(newsSourceComment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditNewsSourceComment(int id)
        {
            var comment = _commentRepo.GetNewsSourceComment(id);
            return PartialView("_CreateOrEditNewsSourceComment", comment);
        }

        [HttpPost]
        public ActionResult EditNewsSourceComment(NewsSourceComment NewsSourcecomment)
        {
            if (ModelState.IsValid)
            {
                _commentRepo.InsertOrUpdateNewsSourceComments(NewsSourcecomment);
                _commentRepo.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void DeleteNewsSourceComment(int id)
        {
            _commentRepo.DeleteNewsSourceComment(id);
            _commentRepo.Save();
        }

        #endregion

    }
}