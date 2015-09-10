using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ACDBContext context;

        public CommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        #region Person
        
        public IQueryable<PersonComment> GetPersonComments(Expression<Func<PersonComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.PersonComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public PersonComment GetPersonComment(int id)
        {
            var ent = context.PersonComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdatePersonComments(PersonComment personcomment)
        {
            if (personcomment.Id == default(int))
            {
                // New entity
                context.PersonComments.Add(personcomment);
            }
            else
            {
                // Existing entity
                context.Entry(personcomment).State = EntityState.Modified;
            }
        }

        public void DeletePersonComment(int id)
        {
            var personcomment = context.PersonComments.Find(id);
            context.PersonComments.Remove(personcomment);
        }

        #endregion


        #region Chapter

        public IQueryable<ChapterComment> GetChapterComments(Expression<Func<ChapterComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.ChapterComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public ChapterComment GetChapterComment(int id)
        {
            var ent = context.ChapterComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateChapterComments(ChapterComment chaptercomment)
        {
            if (chaptercomment.Id == default(int))
            {
                // New entity
                context.ChapterComments.Add(chaptercomment);
            }
            else
            {
                // Existing entity
                context.Entry(chaptercomment).State = EntityState.Modified;
            }
        }

        public void DeleteChapterComment(int id)
        {
            var comment = context.ChapterComments.Find(id);
            context.ChapterComments.Remove(comment);
        }

        #endregion


        #region Contact

        public IQueryable<ContactComment> GetContactComments(Expression<Func<ContactComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.ContactComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public ContactComment GetContactComment(int id)
        {
            var ent = context.ContactComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateContactComments(ContactComment contactcomment)
        {
            if (contactcomment.Id == default(int))
            {
                // New entity
                context.ContactComments.Add(contactcomment);
            }
            else
            {
                // Existing entity
                context.Entry(contactcomment).State = EntityState.Modified;
            }
        }

        public void DeleteContactComment(int id)
        {
            var comment = context.ContactComments.Find(id);
            context.ContactComments.Remove(comment);
        }

        #endregion


        #region Event

        public IQueryable<EventComment> GetEventComments(Expression<Func<EventComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.EventComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public EventComment GetEventComment(int id)
        {
            var ent = context.EventComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateEventComments(EventComment eventcomment)
        {
            if (eventcomment.Id == default(int))
            {
                // New entity
                context.EventComments.Add(eventcomment);
            }
            else
            {
                // Existing entity
                context.Entry(eventcomment).State = EntityState.Modified;
            }
        }

        public void DeleteEventComment(int id)
        {
            var comment = context.EventComments.Find(id);
            context.EventComments.Remove(comment);
        }

        #endregion


        #region MediaAudioVideo

        public IQueryable<MediaAudioVideoComment> GetMediaAudioVideoComments(Expression<Func<MediaAudioVideoComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaAudioVideoComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.MediaAudioVideoComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public MediaAudioVideoComment GetMediaAudioVideoComment(int id)
        {
            var ent = context.MediaAudioVideoComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateMediaAudioVideoComments(MediaAudioVideoComment mediaaudiovideocomment)
        {
            if (mediaaudiovideocomment.Id == default(int))
            {
                // New entity
                context.MediaAudioVideoComments.Add(mediaaudiovideocomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediaaudiovideocomment).State = EntityState.Modified;
            }
        }

        public void DeleteMediaAudioVideoComment(int id)
        {
            var comment = context.MediaAudioVideoComments.Find(id);
            context.MediaAudioVideoComments.Remove(comment);
        }

        #endregion


        #region MediaCorrespondence

        public IQueryable<MediaCorrespondenceComment> GetMediaCorrespondenceComments(Expression<Func<MediaCorrespondenceComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaCorrespondenceComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.MediaCorrespondenceComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public MediaCorrespondenceComment GetMediaCorrespondenceComment(int id)
        {
            var ent = context.MediaCorrespondenceComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateMediaCorrespondenceComments(MediaCorrespondenceComment mediacorrespondencecomment)
        {
            if (mediacorrespondencecomment.Id == default(int))
            {
                // New entity
                context.MediaCorrespondenceComments.Add(mediacorrespondencecomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediacorrespondencecomment).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceComment(int id)
        {
            var comment = context.MediaCorrespondenceComments.Find(id);
            context.MediaCorrespondenceComments.Remove(comment);
        }

        #endregion


        #region MediaPublished

        public IQueryable<MediaPublishedComment> GetMediaPublishedComments(Expression<Func<MediaPublishedComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaPublishedComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.MediaPublishedComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public MediaPublishedComment GetMediaPublishedComment(int id)
        {
            var ent = context.MediaPublishedComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateMediaPublishedComments(MediaPublishedComment mediapublishedcomment)
        {
            if (mediapublishedcomment.Id == default(int))
            {
                // New entity
                context.MediaPublishedComments.Add(mediapublishedcomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediapublishedcomment).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedComment(int id)
        {
            var comment = context.MediaPublishedComments.Find(id);
            context.MediaPublishedComments.Remove(comment);
        }

        #endregion


        #region MediaImage

        public IQueryable<MediaImageComment> GetMediaImageComments(Expression<Func<MediaImageComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaImageComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.MediaImageComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public MediaImageComment GetMediaImageComment(int id)
        {
            var ent = context.MediaImageComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateMediaImageComments(MediaImageComment mediaimagecomment)
        {
            if (mediaimagecomment.Id == default(int))
            {
                // New entity
                context.MediaImageComments.Add(mediaimagecomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediaimagecomment).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageComment(int id)
        {
            var comment = context.MediaImageComments.Find(id);
            context.MediaImageComments.Remove(comment);
        }

        #endregion


        #region Organization

        public IQueryable<OrganizationComment> GetOrganizationComments(Expression<Func<OrganizationComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.OrganizationComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public OrganizationComment GetOrganizationComment(int id)
        {
            var ent = context.OrganizationComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateOrganizationComments(OrganizationComment organizationcomment)
        {
            if (organizationcomment.Id == default(int))
            {
                // New entity
                context.OrganizationComments.Add(organizationcomment);
            }
            else
            {
                // Existing entity
                context.Entry(organizationcomment).State = EntityState.Modified;
            }
        }

        public void DeleteOrganizationComment(int id)
        {
            var comment = context.OrganizationComments.Find(id);
            context.OrganizationComments.Remove(comment);
        }

        #endregion


        #region Vehicle

        public IQueryable<VehicleComment> GetVehicleComments(Expression<Func<VehicleComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.VehicleComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.VehicleComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public VehicleComment GetVehicleComment(int id)
        {
            var ent = context.VehicleComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateVehicleComments(VehicleComment vehiclecomment)
        {
            if (vehiclecomment.Id == default(int))
            {
                // New entity
                context.VehicleComments.Add(vehiclecomment);
            }
            else
            {
                // Existing entity
                context.Entry(vehiclecomment).State = EntityState.Modified;
            }
        }

        public void DeleteVehicleComment(int id)
        {
            var comment = context.VehicleComments.Find(id);
            context.VehicleComments.Remove(comment);
        }

        #endregion


        #region NewsSource

        public IQueryable<NewsSourceComment> GetNewsSourceComments(Expression<Func<NewsSourceComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.NewsSourceComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.NewsSourceComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public NewsSourceComment GetNewsSourceComment(int id)
        {
            var ent = context.NewsSourceComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateNewsSourceComments(NewsSourceComment newssourcecomment)
        {
            if (newssourcecomment.Id == default(int))
            {
                // New entity
                context.NewsSourceComments.Add(newssourcecomment);
            }
            else
            {
                // Existing entity
                context.Entry(newssourcecomment).State = EntityState.Modified;
            }
        }

        public void DeleteNewsSourceComment(int id)
        {
            var comment = context.NewsSourceComments.Find(id);
            context.NewsSourceComments.Remove(comment);
        }

        #endregion

        #region Subscription

        public IQueryable<SubscriptionComment> GetSubscriptionComments(Expression<Func<SubscriptionComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.SubscriptionComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.SubscriptionComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
        }

        public SubscriptionComment GetSubscriptionComment(int id)
        {
            var ent = context.SubscriptionComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateSubscriptionComments(SubscriptionComment subscriptioncomment)
        {
            if (subscriptioncomment.Id == default(int))
            {
                // New entity
                context.SubscriptionComments.Add(subscriptioncomment);
            }
            else
            {
                // Existing entity
                context.Entry(subscriptioncomment).State = EntityState.Modified;
            }
        }

        public void DeleteSubscriptionComment(int id)
        {
            var comment = context.SubscriptionComments.Find(id);
            context.SubscriptionComments.Remove(comment);
        }

        #endregion

        #region MediaWebsiteEGroup

        public IQueryable<MediaWebsiteEGroupComment> GetMediaWebsiteEGroupComments(Expression<Func<MediaWebsiteEGroupComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaWebsiteEGroupComments.Where(filter).Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified);
            }
            return context.MediaWebsiteEGroupComments.Where(x => x.DateDeleted == null).OrderByDescending(x => x.DateModified); ;
        }

        public MediaWebsiteEGroupComment GetMediaWebsiteEGroupComment(int id)
        {
            var ent = context.MediaWebsiteEGroupComments.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdateMediaWebsiteEGroupComments(MediaWebsiteEGroupComment mediawebsiteegroupcomment)
        {
            if (mediawebsiteegroupcomment.Id == default(int))
            {
                // New entity
                context.MediaWebsiteEGroupComments.Add(mediawebsiteegroupcomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediawebsiteegroupcomment).State = EntityState.Modified;
            }
        }

        public void DeleteMediaWebsiteEGroupComment(int id)
        {
            var mediawebsiteegroupcomment = context.MediaWebsiteEGroupComments.Find(id);
            context.MediaWebsiteEGroupComments.Remove(mediawebsiteegroupcomment);
        }

        #endregion

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface ICommentRepository 
    {
        PersonComment GetPersonComment(int id);
        IQueryable<PersonComment> GetPersonComments(Expression<Func<PersonComment, bool>> filter);
        void InsertOrUpdatePersonComments(PersonComment personcomment);
        void DeletePersonComment(int id);

        ChapterComment GetChapterComment(int id);
        IQueryable<ChapterComment> GetChapterComments(Expression<Func<ChapterComment, bool>> filter);
        void InsertOrUpdateChapterComments(ChapterComment chaptercomment);
        void DeleteChapterComment(int id);

        ContactComment GetContactComment(int id);
        IQueryable<ContactComment> GetContactComments(Expression<Func<ContactComment, bool>> filter);
        void InsertOrUpdateContactComments(ContactComment contactcomment);
        void DeleteContactComment(int id);

        EventComment GetEventComment(int id);
        IQueryable<EventComment> GetEventComments(Expression<Func<EventComment, bool>> filter);
        void InsertOrUpdateEventComments(EventComment eventcomment);
        void DeleteEventComment(int id);

        MediaAudioVideoComment GetMediaAudioVideoComment(int id);
        IQueryable<MediaAudioVideoComment> GetMediaAudioVideoComments(Expression<Func<MediaAudioVideoComment, bool>> filter);
        void InsertOrUpdateMediaAudioVideoComments(MediaAudioVideoComment mediaaudiovideocomment);
        void DeleteMediaAudioVideoComment(int id);

        NewsSourceComment GetNewsSourceComment(int id);
        IQueryable<NewsSourceComment> GetNewsSourceComments(Expression<Func<NewsSourceComment, bool>> filter);
        void InsertOrUpdateNewsSourceComments(NewsSourceComment newssourcecomment);
        void DeleteNewsSourceComment(int id);

        MediaCorrespondenceComment GetMediaCorrespondenceComment(int id);
        IQueryable<MediaCorrespondenceComment> GetMediaCorrespondenceComments(Expression<Func<MediaCorrespondenceComment, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceComments(MediaCorrespondenceComment mediacorrespondencecomment);
        void DeleteMediaCorrespondenceComment(int id);

        MediaPublishedComment GetMediaPublishedComment(int id);
        IQueryable<MediaPublishedComment> GetMediaPublishedComments(Expression<Func<MediaPublishedComment, bool>> filter);
        void InsertOrUpdateMediaPublishedComments(MediaPublishedComment mediapublishedcomment);
        void DeleteMediaPublishedComment(int id);

        MediaImageComment GetMediaImageComment(int id);
        IQueryable<MediaImageComment> GetMediaImageComments(Expression<Func<MediaImageComment, bool>> filter);
        void InsertOrUpdateMediaImageComments(MediaImageComment mediaimagecomment);
        void DeleteMediaImageComment(int id);

        OrganizationComment GetOrganizationComment(int id);
        IQueryable<OrganizationComment> GetOrganizationComments(Expression<Func<OrganizationComment, bool>> filter);
        void InsertOrUpdateOrganizationComments(OrganizationComment organizationcomment);
        void DeleteOrganizationComment(int id);

        VehicleComment GetVehicleComment(int id);
        IQueryable<VehicleComment> GetVehicleComments(Expression<Func<VehicleComment, bool>> filter);
        void InsertOrUpdateVehicleComments(VehicleComment vehiclecomment);
        void DeleteVehicleComment(int id);

        SubscriptionComment GetSubscriptionComment(int id);
        IQueryable<SubscriptionComment> GetSubscriptionComments(Expression<Func<SubscriptionComment, bool>> filter);
        void InsertOrUpdateSubscriptionComments(SubscriptionComment subscriptioncomment);
        void DeleteSubscriptionComment(int id);

        MediaWebsiteEGroupComment GetMediaWebsiteEGroupComment(int id);
        IQueryable<MediaWebsiteEGroupComment> GetMediaWebsiteEGroupComments(Expression<Func<MediaWebsiteEGroupComment, bool>> filter);
        void InsertOrUpdateMediaWebsiteEGroupComments(MediaWebsiteEGroupComment MediaWebsiteEGroupcomment);
        void DeleteMediaWebsiteEGroupComment(int id);

        void Save();

    }
}