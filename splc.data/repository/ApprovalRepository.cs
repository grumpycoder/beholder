using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ApprovalRepository : IApprovalRepository
    {
        readonly ACDBContext _ctx;

        public ApprovalRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<PersonPersonRel> GetPersonPersonApprovals()
        {
            return _ctx.PersonPersonRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonPerson(long id)
        {
            var rel = _ctx.PersonPersonRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonPerson(long id)
        {
            var rel = _ctx.PersonPersonRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<PersonContactRel> GetPersonContactApprovals()
        {
            return _ctx.PersonContactRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonContactApprovals(long id)
        {
            var rel = _ctx.PersonContactRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonContactApprovals(long id)
        {
            var rel = _ctx.PersonContactRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<PersonEventRel> GetPersonEventApprovals()
        {
            return _ctx.PersonEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonEvent(long id)
        {
            var rel = _ctx.PersonEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonEvent(long id)
        {
            var rel = _ctx.PersonEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<PersonVehicleRel> GetPersonVehicleApprovals()
        {
            return _ctx.PersonVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonVehicle(long id)
        {
            var rel = _ctx.PersonVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonVehicle(long id)
        {
            var rel = _ctx.PersonVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<PersonMediaImageRel> GetPersonMediaImageApprovals()
        {
            return _ctx.PersonMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonMediaImage(long id)
        {
            var rel = _ctx.PersonMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonMediaImage(long id)
        {
            var rel = _ctx.PersonMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<PersonMediaAudioVideoRel> GetPersonMediaAudioVideoApprovals()
        {
            return _ctx.PersonMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonMediaAudioVideo(long id)
        {
            var rel = _ctx.PersonMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges(); throw new NotImplementedException();
        }

        public void RejectPersonMediaAudioVideo(long id)
        {
            var rel = _ctx.PersonMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterOrganizationRel> GetChapterOrganizationApprovals()
        {
            return _ctx.ChapterOrganizationRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterOrganization(long id)
        {
            var rel = _ctx.ChapterOrganizationRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterOrganization(long id)
        {
            var rel = _ctx.ChapterOrganizationRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationOrganizationRel> GetOrganizationOrganizationApprovals()
        {
            return _ctx.OrganizationOrganizationRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationOrganization(long id)
        {
            var rel = _ctx.OrganizationOrganizationRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationOrganization(long id)
        {
            var rel = _ctx.OrganizationOrganizationRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationEventRel> GetOrganizationEventApprovals()
        {
            return _ctx.OrganizationEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationEvent(long id)
        {
            var rel = _ctx.OrganizationEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationEvent(long id)
        {
            var rel = _ctx.OrganizationEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationVehicleRel> GetOrganizationVehicleApprovals()
        {
            return _ctx.OrganizationVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationVehicle(long id)
        {
            var rel = _ctx.OrganizationVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationVehicle(long id)
        {
            var rel = _ctx.OrganizationVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationMediaAudioVideoRel> GetOrganizationMediaAudioVideoApprovals()
        {
            return _ctx.OrganizationMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationMediaAudioVideo(long id)
        {
            var rel = _ctx.OrganizationMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationMediaAudioVideo(long id)
        {
            var rel = _ctx.OrganizationMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImageApprovals()
        {
            return _ctx.OrganizationMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationMediaImage(long id)
        {
            var rel = _ctx.OrganizationMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationMediaImage(long id)
        {
            var rel = _ctx.OrganizationMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterChapterRel> GetChapterChapterApprovals()
        {
            return _ctx.ChapterChapterRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterChapter(long id)
        {
            var rel = _ctx.ChapterChapterRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterChapter(long id)
        {
            var rel = _ctx.ChapterChapterRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterEventRel> GetChapterEventApprovals()
        {
            return _ctx.ChapterEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterEvent(long id)
        {
            var rel = _ctx.ChapterEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterEvent(long id)
        {
            var rel = _ctx.ChapterEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterVehicleRel> GetChapterVehicleApprovals()
        {
            return _ctx.ChapterVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterVehicle(long id)
        {
            var rel = _ctx.ChapterVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterVehicle(long id)
        {
            var rel = _ctx.ChapterVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterMediaAudioVideoRel> GetChapterMediaAudioVideoApprovals()
        {
            return _ctx.ChapterMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterMediaAudioVideo(long id)
        {
            var rel = _ctx.ChapterMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterMediaAudioVideo(long id)
        {
            var rel = _ctx.ChapterMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterMediaImageRel> GetChapterMediaImageApprovals()
        {
            return _ctx.ChapterMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterMediaImage(long id)
        {
            var rel = _ctx.ChapterMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterMediaImage(long id)
        {
            var rel = _ctx.ChapterMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<VehicleVehicleRel> GetVehicleVehicleApprovals()
        {
            return _ctx.VehicleVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveVehicleVehicle(long id)
        {
            var rel = _ctx.VehicleVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectVehicleVehicle(long id)
        {
            var rel = _ctx.VehicleVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<EventVehicleRel> GetEventVehicleApprovals()
        {
            return _ctx.EventVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveEventVehicle(long id)
        {
            var rel = _ctx.EventVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectEventVehicle(long id)
        {
            var rel = _ctx.EventVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaAudioVideoVehicleRel> GetMediaAudioVideoVehicleApprovals()
        {
            return _ctx.MediaAudioVideoVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveMediaAudioVideoVehicle(long id)
        {
            var rel = _ctx.MediaAudioVideoVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectMediaAudioVideoVehicle(long id)
        {
            var rel = _ctx.MediaAudioVideoVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaImageVehicleRel> GetMediaImageVehicleApprovals()
        {
            return _ctx.MediaImageVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveMediaImageVehicle(long id)
        {
            var rel = _ctx.MediaImageVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectMediaImageVehicle(long id)
        {
            var rel = _ctx.MediaImageVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaImageMediaAudioVideoRel> GetMediaImageMediaAudioVideoApprovals()
        {
            return _ctx.MediaImageMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveMediaImageMediaAudioVideo(long id)
        {
            var rel = _ctx.MediaImageMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectMediaImageMediaAudioVideo(long id)
        {
            var rel = _ctx.MediaImageMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterMediaCorrespondenceRel> GetChapterMediaCorrespondenceApprovals()
        {
            return _ctx.ChapterMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterMediaCorrespondence(long id)
        {
            var rel = _ctx.ChapterMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterMediaCorrespondence(long id)
        {
            var rel = _ctx.ChapterMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ChapterMediaPublishedRel> GetChapterMediaPublishedApprovals()
        {
            return _ctx.ChapterMediaPublishedRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterMediaPublished(int id)
        {
            var rel = _ctx.ChapterMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterMediaPublished(int id)
        {
            var rel = _ctx.ChapterMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges(); ;
        }

        public IQueryable<ChapterSubscriptionRel> GetChapterSubscriptionApprovals()
        {
            return _ctx.ChapterSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveChapterSubscription(int id)
        {
            var rel = _ctx.ChapterSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectChapterSubscription(int id)
        {
            var rel = _ctx.ChapterSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges(); ;
        }

        public IQueryable<ContactContactRel> GetContactContactApprovals()
        {
            return _ctx.ContactContactRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveContactContact(int id)
        {
            var rel = _ctx.ContactContactRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectContactContact(int id)
        {
            var rel = _ctx.ContactContactRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges(); ;
        }

        public IQueryable<ContactMediaCorrespondenceRel> GetContactMediaCorrespondenceApprovals()
        {
            return _ctx.ContactMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveContactMediaCorrespondence(int id)
        {
            var rel = _ctx.ContactMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectContactMediaCorrespondence(int id)
        {
            var rel = _ctx.ContactContactRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<EventEventRel> GetEventEventApprovals()
        {
            return _ctx.EventEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveEventEvent(int id)
        {
            var rel = _ctx.EventEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectEventEvent(int id)
        {
            var rel = _ctx.EventEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<EventMediaAudioVideoRel> GetEventAudioVideoApprovals()
        {
            return _ctx.EventMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveEventAudioVideo(int id)
        {
            var rel = _ctx.EventMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectEventAudioVideo(int id)
        {
            var rel = _ctx.EventMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<EventMediaImageRel> GetEventImageApprovals()
        {
            return _ctx.EventMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveEventImage(int id)
        {
            var rel = _ctx.EventMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectEventImage(int id)
        {
            var rel = _ctx.EventMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<EventSubscriptionRel> GetEventSubscriptionApprovals()
        {
            return _ctx.EventSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveEventSubscription(int id)
        {
            var rel = _ctx.EventSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectEventSubscription(int id)
        {
            var rel = _ctx.EventSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationMediaCorrespondenceRel> OrganizationCorrespondenceApprovals()
        {
            return _ctx.OrganizationMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationCorrespondence(int id)
        {
            var rel = _ctx.OrganizationMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationCorrespondence(int id)
        {
            var rel = _ctx.OrganizationMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationMediaPublishedRel> OrganizationPublishedApprovals()
        {
            return _ctx.OrganizationMediaPublishedRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationPublished(int id)
        {
            var rel = _ctx.OrganizationMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationPublished(int id)
        {
            var rel = _ctx.OrganizationMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<OrganizationSubscriptionRel> OrganizationSubscriptionApprovals()
        {
            return _ctx.OrganizationSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveOrganizationSubscription(int id)
        {
            var rel = _ctx.OrganizationSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectOrganizationSubscription(int id)
        {
            var rel = _ctx.OrganizationSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<ContactMediaImageRel> GetContactImageApprovals()
        {
            return _ctx.ContactMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveContactImage(int id)
        {
            var rel = _ctx.ContactMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectContactImage(int id)
        {
            var rel = _ctx.ContactMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        //public IQueryable<ContactMediaPublishedRel> GetContactPublishedApprovals()
        //{
        //    return _ctx.ContactMediaPublishedRels.Where(x => x.ApprovalStatusId == 4);
        //}

        //public void ApproveContactPublished(int id)
        //{
        //    var rel = _ctx.ContactMediaPublishedRels.Find(id);
        //    rel.ApprovalStatusId = 1;
        //    _ctx.SaveChanges();
        //}

        //public void RejectContactPublished(int id)
        //{
        //    var rel = _ctx.ContactMediaPublishedRels.Find(id);
        //    rel.ApprovalStatusId = 3;
        //    _ctx.SaveChanges();
        //}

        public IQueryable<ContactSubscriptionRel> GetContactSubscriptionApprovals()
        {
            return _ctx.ContactSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveContactSubscription(int id)
        {
            var rel = _ctx.ContactSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectContactSubscription(int id)
        {
            var rel = _ctx.ContactSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<SubscriptionSubscriptionRel> GetSubscriptionSubscriptionApprovals()
        {
            return _ctx.SubscriptionSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveSubscriptionSubscription(int id)
        {
            var rel = _ctx.SubscriptionSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectSubscriptionSubscription(int id)
        {
            var rel = _ctx.SubscriptionSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<SubscriptionVehicleRel> GetSubscriptionVehicleApprovals()
        {
            return _ctx.SubscriptionVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveSubscriptionVehicle(int id)
        {
            var rel = _ctx.SubscriptionVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectSubscriptionVehicle(int id)
        {
            var rel = _ctx.SubscriptionVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<PersonMediaPublishedRel> GetPersonPublishedApprovals()
        {
            return _ctx.PersonMediaPublishedRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonPublished(int id)
        {
            var rel = _ctx.PersonMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonPublished(int id)
        {
            var rel = _ctx.PersonMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<PersonMediaCorrespondenceRel> GetPersonCorrespondenceApprovals()
        {
            return _ctx.PersonMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePersonCorrespondence(long id)
        {
            var rel = _ctx.PersonMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPersonCorrespondence(long id)
        {
            var rel = _ctx.PersonMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaCorrespondenceEventRel> GetCorrespondenceEventApprovals()
        {
            return _ctx.MediaCorrespondenceEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceEvent(int id)
        {
            var rel = _ctx.MediaCorrespondenceEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceEvent(int id)
        {
            var rel = _ctx.MediaCorrespondenceEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaCorrespondenceMediaAudioVideoRel> GetCorrespondenceAudioVideoApprovals()
        {
            return _ctx.MediaCorrespondenceMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceAudioVideo(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceAudioVideo(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }

        public IQueryable<MediaCorrespondenceMediaCorrespondenceRel> GetCorrespondenceCorrespondenceApprovals()
        {
            return _ctx.MediaCorrespondenceMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceCorrespondence(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceCorrespondence(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaCorrespondenceMediaImageRel> GetCorrespondenceImageApprovals()
        {
            return _ctx.MediaCorrespondenceMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceImage(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceImage(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaCorrespondenceSubscriptionRel> GetCorrespondenceSubscriptionApprovals()
        {
            return _ctx.MediaCorrespondenceSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceSubscription(int id)
        {
            var rel = _ctx.MediaCorrespondenceSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceSubscription(int id)
        {
            var rel = _ctx.MediaCorrespondenceSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaCorrespondenceVehicleRel> GetCorrespondenceVehicleApprovals()
        {
            return _ctx.MediaCorrespondenceVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveCorrespondenceVehicle(int id)
        {
            var rel = _ctx.MediaCorrespondenceVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectCorrespondenceVehicle(int id)
        {
            var rel = _ctx.MediaCorrespondenceVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaImageMediaImageRel> GetImageImageApprovals()
        {
            return _ctx.MediaImageMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveImageImage(int id)
        {
            var rel = _ctx.MediaImageMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectImageImage(int id)
        {
            var rel = _ctx.MediaImageMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaImageSubscriptionRel> GetImageSubscriptionApprovals()
        {
            return _ctx.MediaImageSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveImageSubscription(int id)
        {
            var rel = _ctx.MediaImageSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectImageSubscription(int id)
        {
            var rel = _ctx.MediaImageSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedEventRel> GetPublishedEventApprovals()
        {
            return _ctx.MediaPublishedEventRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedEvent(int id)
        {
            var rel = _ctx.MediaPublishedEventRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedEvent(int id)
        {
            var rel = _ctx.MediaPublishedEventRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedMediaAudioVideoRel> GetPublishedAudioVideoApprovals()
        {
            return _ctx.MediaPublishedMediaAudioVideoRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedAudioVideo(int id)
        {
            var rel = _ctx.MediaPublishedMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedAudioVideo(int id)
        {
            var rel = _ctx.MediaPublishedMediaAudioVideoRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedMediaCorrespondenceRel> GetPublishedCorrespondenceApprovals()
        {
            return _ctx.MediaPublishedMediaCorrespondenceRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedCorrespondence(int id)
        {
            var rel = _ctx.MediaPublishedMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedCorrespondence(int id)
        {
            var rel = _ctx.MediaPublishedMediaCorrespondenceRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedMediaImageRel> GetPublishedImageApprovals()
        {
            return _ctx.MediaPublishedMediaImageRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedImage(int id)
        {
            var rel = _ctx.MediaPublishedMediaImageRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedImage(int id)
        {
            var rel = _ctx.MediaPublishedMediaImageRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedMediaPublishedRel> GetPublishedPublishedApprovals()
        {
            return _ctx.MediaPublishedMediaPublishedRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedPublished(int id)
        {
            var rel = _ctx.MediaPublishedMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedPublished(int id)
        {
            var rel = _ctx.MediaPublishedMediaPublishedRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedMediaWebsiteEGroupRel> GetPublishedWebsiteEGroupApprovals()
        {
            return _ctx.MediaPublishedMediaWebsiteEGroupRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaPublishedMediaWebsiteEGroupRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges();
        }

        public void RejectPublishedWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaPublishedMediaWebsiteEGroupRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedSubscriptionRel> GetPublishedSubscriptionApprovals()
        {
            return _ctx.MediaPublishedSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedSubscription(int id)
        {
            var rel = _ctx.MediaPublishedSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges(); throw new NotImplementedException();
        }

        public void RejectPublishedSubscription(int id)
        {
            var rel = _ctx.MediaPublishedSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaPublishedVehicleRel> GetPublishedVehicleApprovals()
        {
            return _ctx.MediaPublishedVehicleRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApprovePublishedVehicle(int id)
        {
            var rel = _ctx.MediaPublishedVehicleRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges(); throw new NotImplementedException();
        }

        public void RejectPublishedVehicle(int id)
        {
            var rel = _ctx.MediaPublishedVehicleRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }


        public IQueryable<MediaWebsiteEGroupSubscriptionRel> GetWebsiteEGroupSubscriptionApprovals()
        {
            return _ctx.MediaWebsiteEGroupSubscriptionRels.Where(x => x.ApprovalStatusId == 4);
        }

        public void ApproveWebsiteEGroupSubscription(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 1;
            _ctx.SaveChanges(); throw new NotImplementedException();
        }

        public void RejectWebsiteEGroupSubscription(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupSubscriptionRels.Find(id);
            rel.ApprovalStatusId = 3;
            _ctx.SaveChanges();
        }
    }

    public interface IApprovalRepository
    {

        IQueryable<PersonPersonRel> GetPersonPersonApprovals();
        void ApprovePersonPerson(long id);
        void RejectPersonPerson(long id);

        IQueryable<PersonContactRel> GetPersonContactApprovals();
        void ApprovePersonContactApprovals(long id);
        void RejectPersonContactApprovals(long id);

        IQueryable<PersonEventRel> GetPersonEventApprovals();
        void ApprovePersonEvent(long id);
        void RejectPersonEvent(long id);

        IQueryable<PersonVehicleRel> GetPersonVehicleApprovals();
        void ApprovePersonVehicle(long id);
        void RejectPersonVehicle(long id);

        IQueryable<PersonMediaImageRel> GetPersonMediaImageApprovals();
        void ApprovePersonMediaImage(long id);
        void RejectPersonMediaImage(long id);

        IQueryable<PersonMediaAudioVideoRel> GetPersonMediaAudioVideoApprovals();
        void ApprovePersonMediaAudioVideo(long id);
        void RejectPersonMediaAudioVideo(long id);

        IQueryable<ChapterOrganizationRel> GetChapterOrganizationApprovals();
        void ApproveChapterOrganization(long id);
        void RejectChapterOrganization(long id);

        IQueryable<OrganizationOrganizationRel> GetOrganizationOrganizationApprovals();
        void ApproveOrganizationOrganization(long id);
        void RejectOrganizationOrganization(long id);

        IQueryable<OrganizationEventRel> GetOrganizationEventApprovals();
        void ApproveOrganizationEvent(long id);
        void RejectOrganizationEvent(long id);

        IQueryable<OrganizationVehicleRel> GetOrganizationVehicleApprovals();
        void ApproveOrganizationVehicle(long id);
        void RejectOrganizationVehicle(long id);

        IQueryable<OrganizationMediaAudioVideoRel> GetOrganizationMediaAudioVideoApprovals();
        void ApproveOrganizationMediaAudioVideo(long id);
        void RejectOrganizationMediaAudioVideo(long id);

        IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImageApprovals();
        void ApproveOrganizationMediaImage(long id);
        void RejectOrganizationMediaImage(long id);

        IQueryable<ChapterChapterRel> GetChapterChapterApprovals();
        void ApproveChapterChapter(long id);
        void RejectChapterChapter(long id);

        IQueryable<ChapterEventRel> GetChapterEventApprovals();
        void ApproveChapterEvent(long id);
        void RejectChapterEvent(long id);

        IQueryable<ChapterVehicleRel> GetChapterVehicleApprovals();
        void ApproveChapterVehicle(long id);
        void RejectChapterVehicle(long id);

        IQueryable<ChapterMediaAudioVideoRel> GetChapterMediaAudioVideoApprovals();
        void ApproveChapterMediaAudioVideo(long id);
        void RejectChapterMediaAudioVideo(long id);

        IQueryable<ChapterMediaImageRel> GetChapterMediaImageApprovals();
        void ApproveChapterMediaImage(long id);
        void RejectChapterMediaImage(long id);

        IQueryable<VehicleVehicleRel> GetVehicleVehicleApprovals();
        void ApproveVehicleVehicle(long id);
        void RejectVehicleVehicle(long id);

        IQueryable<EventVehicleRel> GetEventVehicleApprovals();
        void ApproveEventVehicle(long id);
        void RejectEventVehicle(long id);

        IQueryable<MediaAudioVideoVehicleRel> GetMediaAudioVideoVehicleApprovals();
        void ApproveMediaAudioVideoVehicle(long id);
        void RejectMediaAudioVideoVehicle(long id);

        IQueryable<MediaImageVehicleRel> GetMediaImageVehicleApprovals();
        void ApproveMediaImageVehicle(long id);
        void RejectMediaImageVehicle(long id);

        IQueryable<MediaImageMediaAudioVideoRel> GetMediaImageMediaAudioVideoApprovals();
        void ApproveMediaImageMediaAudioVideo(long id);
        void RejectMediaImageMediaAudioVideo(long id);

        IQueryable<ChapterMediaCorrespondenceRel> GetChapterMediaCorrespondenceApprovals();
        void ApproveChapterMediaCorrespondence(long id);
        void RejectChapterMediaCorrespondence(long id);

        IQueryable<ChapterMediaPublishedRel> GetChapterMediaPublishedApprovals();
        void ApproveChapterMediaPublished(int id);
        void RejectChapterMediaPublished(int id);

        IQueryable<ChapterSubscriptionRel> GetChapterSubscriptionApprovals();
        void ApproveChapterSubscription(int id);
        void RejectChapterSubscription(int id);

        IQueryable<ContactContactRel> GetContactContactApprovals();
        void ApproveContactContact(int id);
        void RejectContactContact(int id);

        IQueryable<ContactMediaCorrespondenceRel> GetContactMediaCorrespondenceApprovals();
        void ApproveContactMediaCorrespondence(int id);
        void RejectContactMediaCorrespondence(int id);

        IQueryable<EventEventRel> GetEventEventApprovals();
        void ApproveEventEvent(int id);
        void RejectEventEvent(int id);

        IQueryable<EventMediaAudioVideoRel> GetEventAudioVideoApprovals();
        void ApproveEventAudioVideo(int id);
        void RejectEventAudioVideo(int id);

        IQueryable<EventMediaImageRel> GetEventImageApprovals();
        void ApproveEventImage(int id);
        void RejectEventImage(int id);

        IQueryable<EventSubscriptionRel> GetEventSubscriptionApprovals();
        void ApproveEventSubscription(int id);
        void RejectEventSubscription(int id);

        IQueryable<OrganizationMediaCorrespondenceRel> OrganizationCorrespondenceApprovals();
        void ApproveOrganizationCorrespondence(int id);
        void RejectOrganizationCorrespondence(int id);

        IQueryable<OrganizationMediaPublishedRel> OrganizationPublishedApprovals();
        void ApproveOrganizationPublished(int id);
        void RejectOrganizationPublished(int id);

        IQueryable<OrganizationSubscriptionRel> OrganizationSubscriptionApprovals();
        void ApproveOrganizationSubscription(int id);
        void RejectOrganizationSubscription(int id);

        IQueryable<ContactMediaImageRel> GetContactImageApprovals();
        void ApproveContactImage(int id);
        void RejectContactImage(int id);

        //IQueryable<ContactMediaPublishedRel> GetContactPublishedApprovals();
        //void ApproveContactPublished(int id);
        //void RejectContactPublished(int id);

        IQueryable<ContactSubscriptionRel> GetContactSubscriptionApprovals();
        void ApproveContactSubscription(int id);
        void RejectContactSubscription(int id);

        IQueryable<SubscriptionSubscriptionRel> GetSubscriptionSubscriptionApprovals();
        void ApproveSubscriptionSubscription(int id);
        void RejectSubscriptionSubscription(int id);

        IQueryable<SubscriptionVehicleRel> GetSubscriptionVehicleApprovals();
        void ApproveSubscriptionVehicle(int id);
        void RejectSubscriptionVehicle(int id);

        IQueryable<PersonMediaPublishedRel> GetPersonPublishedApprovals();
        void ApprovePersonPublished(int id);
        void RejectPersonPublished(int id);

        IQueryable<PersonMediaCorrespondenceRel> GetPersonCorrespondenceApprovals();
        void ApprovePersonCorrespondence(long id);
        void RejectPersonCorrespondence(long id);

        IQueryable<MediaCorrespondenceEventRel> GetCorrespondenceEventApprovals();
        void ApproveCorrespondenceEvent(int id);
        void RejectCorrespondenceEvent(int id);

        IQueryable<MediaCorrespondenceMediaAudioVideoRel> GetCorrespondenceAudioVideoApprovals();
        void ApproveCorrespondenceAudioVideo(int id);
        void RejectCorrespondenceAudioVideo(int id);

        IQueryable<MediaCorrespondenceMediaCorrespondenceRel> GetCorrespondenceCorrespondenceApprovals();
        void ApproveCorrespondenceCorrespondence(int id);
        void RejectCorrespondenceCorrespondence(int id);

        IQueryable<MediaCorrespondenceMediaImageRel> GetCorrespondenceImageApprovals();
        void ApproveCorrespondenceImage(int id);
        void RejectCorrespondenceImage(int id);

        IQueryable<MediaCorrespondenceSubscriptionRel> GetCorrespondenceSubscriptionApprovals();
        void ApproveCorrespondenceSubscription(int id);
        void RejectCorrespondenceSubscription(int id);

        IQueryable<MediaCorrespondenceVehicleRel> GetCorrespondenceVehicleApprovals();
        void ApproveCorrespondenceVehicle(int id);
        void RejectCorrespondenceVehicle(int id);

        IQueryable<MediaImageMediaImageRel> GetImageImageApprovals();
        void ApproveImageImage(int id);
        void RejectImageImage(int id);

        IQueryable<MediaImageSubscriptionRel> GetImageSubscriptionApprovals();
        void ApproveImageSubscription(int id);
        void RejectImageSubscription(int id);

        IQueryable<MediaPublishedEventRel> GetPublishedEventApprovals();
        void ApprovePublishedEvent(int id);
        void RejectPublishedEvent(int id);

        IQueryable<MediaPublishedMediaAudioVideoRel> GetPublishedAudioVideoApprovals();
        void ApprovePublishedAudioVideo(int id);
        void RejectPublishedAudioVideo(int id);

        IQueryable<MediaPublishedMediaCorrespondenceRel> GetPublishedCorrespondenceApprovals();
        void ApprovePublishedCorrespondence(int id);
        void RejectPublishedCorrespondence(int id);

        IQueryable<MediaPublishedMediaImageRel> GetPublishedImageApprovals();
        void ApprovePublishedImage(int id);
        void RejectPublishedImage(int id);

        IQueryable<MediaPublishedMediaPublishedRel> GetPublishedPublishedApprovals();
        void ApprovePublishedPublished(int id);
        void RejectPublishedPublished(int id);

        IQueryable<MediaPublishedMediaWebsiteEGroupRel> GetPublishedWebsiteEGroupApprovals();
        void ApprovePublishedWebsiteEGroup(int id);
        void RejectPublishedWebsiteEGroup(int id);

        IQueryable<MediaPublishedSubscriptionRel> GetPublishedSubscriptionApprovals();
        void ApprovePublishedSubscription(int id);
        void RejectPublishedSubscription(int id);

        IQueryable<MediaPublishedVehicleRel> GetPublishedVehicleApprovals();
        void ApprovePublishedVehicle(int id);
        void RejectPublishedVehicle(int id);

        IQueryable<MediaWebsiteEGroupSubscriptionRel> GetWebsiteEGroupSubscriptionApprovals();
        void ApproveWebsiteEGroupSubscription(int id);
        void RejectWebsiteEGroupSubscription(int id);
    }

}
