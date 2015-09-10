using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;
using System.Reflection;

namespace splc.data.repository
{
    public class MediaPublishedRepository : IMediaPublishedRepository
    {

        private readonly ACDBContext _ctx;

        public MediaPublishedRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public IQueryable<MediaPublished> GetMediaPublisheds()
        {
            return _ctx.MediaPublisheds.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaPublished> GetMediaPublisheds(Expression<Func<MediaPublished, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublisheds.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.MediaPublisheds.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaPublished> GetMediaPublisheds(User user)
        {
            return _ctx.MediaPublisheds.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<MediaPublished> GetMediaPublisheds(User user, Expression<Func<MediaPublished, bool>> filter)
        {
            IQueryable<MediaPublished> entity;
            if (filter != null)
            {
                entity = _ctx.MediaPublisheds.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            else
            {
                entity = _ctx.MediaPublisheds.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            return entity.Where(b => b.RemovalStatusId == null);
            //if (filter != null)
            //{
            //    return _ctx.MediaPublisheds.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.RemovalStatusId == null);
            //}
            //return _ctx.MediaPublisheds.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.RemovalStatusId == null);
        }

        public MediaPublished GetMediaPublished(User user, int id)
        {
            var published = _ctx.MediaPublisheds.Find(id);
            if (published.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && published.DateDeleted == null && published.RemovalStatusId == null)
            {
                return published;
            }
            return null;
        }

        public MediaPublished GetMediaPublished(int id)
        {
            var rel = _ctx.MediaPublisheds.Find(id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public MediaPublishedContext GetMediaPublishedContext(int id)
        {
            return _ctx.MediaPublishedContexts.Find(id);
        }

        public void InsertOrUpdate(MediaPublished mediaPublished)
        {
            if (mediaPublished.Id == default(int))
            {
                // Existing entity
                //if (mediaPublished.MediaPublishedContext != null)
                //{
                //    // New entity
                //    _ctx.MediaPublishedContexts.Add(mediaPublished.MediaPublishedContext);
                //}
                _ctx.MediaPublisheds.Add(mediaPublished);
            }
            else
            {
                // Existing entity
                //if (mediaPublished.MediaPublishedContext != null)
                //{
                //    if (mediaPublished.MediaPublishedContextId == null || mediaPublished.MediaPublishedContextId == default(int))
                //    {
                //        _ctx.MediaPublishedContexts.Add(mediaPublished.MediaPublishedContext);
                //        _ctx.Entry(mediaPublished).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        // Existing entity
                //        _ctx.Entry(mediaPublished.MediaPublishedContext).State = EntityState.Modified;
                //        _ctx.Entry(mediaPublished).State = EntityState.Modified;
                //    }
                //}
                //else
                //{
                    _ctx.Entry(mediaPublished).State = EntityState.Modified;
                //}
            }

        }

        public void Delete(int id)
        {
            var mediaPublished = _ctx.MediaPublisheds.Find(id);
            _ctx.MediaPublisheds.Remove(mediaPublished);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Event

        public MediaPublishedEventRel GetMediaPublishedEvent(int id)
        {
            var rel = _ctx.MediaPublishedEventRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.Event.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedEventRel> GetMediaPublishedEvents(Expression<Func<MediaPublishedEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedEventRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.Event.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedEventRels.Where(x => x.MediaPublished.DateDeleted == null && x.Event.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedEvent(MediaPublishedEventRel mediaPublishedeventrel)
        {
            if (mediaPublishedeventrel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedEventRels.Add(mediaPublishedeventrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedeventrel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedEvent(int id)
        {
            var mediaPublishedeventrel = _ctx.MediaPublishedEventRels.Find(id);
            _ctx.MediaPublishedEventRels.Remove(mediaPublishedeventrel);
        }

        #endregion


        #region MediaAudioVideo

        public MediaPublishedMediaAudioVideoRel GetMediaPublishedMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaPublishedMediaAudioVideoRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.MediaAudioVideo.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedMediaAudioVideoRel> GetMediaPublishedMediaAudioVideos(Expression<Func<MediaPublishedMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedMediaAudioVideoRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedMediaAudioVideoRels.Where(x => x.MediaPublished.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedMediaAudioVideo(MediaPublishedMediaAudioVideoRel mediaPublishedmediaAudioVideorel)
        {
            if (mediaPublishedmediaAudioVideorel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedMediaAudioVideoRels.Add(mediaPublishedmediaAudioVideorel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedmediaAudioVideorel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedMediaAudioVideo(int id)
        {
            var mediaPublishedmediaAudioVideorel = _ctx.MediaPublishedMediaAudioVideoRels.Find(id);
            _ctx.MediaPublishedMediaAudioVideoRels.Remove(mediaPublishedmediaAudioVideorel);
        }

        #endregion


        #region MediaCorrespondence

        public MediaPublishedMediaCorrespondenceRel GetMediaPublishedMediaCorrespondence(int id)
        {
            var rel = _ctx.MediaPublishedMediaCorrespondenceRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.MediaCorrespondence.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.MediaCorrespondence.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedMediaCorrespondenceRel> GetMediaPublishedMediaCorrespondences(Expression<Func<MediaPublishedMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedMediaCorrespondenceRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedMediaCorrespondenceRels.Where(x => x.MediaPublished.DateDeleted == null && x.MediaCorrespondence.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaCorrespondence.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedMediaCorrespondence(MediaPublishedMediaCorrespondenceRel mediaPublishedmediaCorrespondencerel)
        {
            if (mediaPublishedmediaCorrespondencerel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedMediaCorrespondenceRels.Add(mediaPublishedmediaCorrespondencerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedmediaCorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedMediaCorrespondence(int id)
        {
            var mediaPublishedmediaCorrespondencerel = _ctx.MediaPublishedMediaCorrespondenceRels.Find(id);
            _ctx.MediaPublishedMediaCorrespondenceRels.Remove(mediaPublishedmediaCorrespondencerel);
        }

        #endregion


        #region MediaImage

        public MediaPublishedMediaImageRel GetMediaPublishedMediaImage(int id)
        {
            var rel = _ctx.MediaPublishedMediaImageRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.MediaImage.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.MediaImage.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedMediaImageRel> GetMediaPublishedMediaImages(Expression<Func<MediaPublishedMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedMediaImageRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.MediaImage.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedMediaImageRels.Where(x => x.MediaPublished.DateDeleted == null && x.MediaImage.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedMediaImage(MediaPublishedMediaImageRel mediaPublishedmediaImagerel)
        {
            if (mediaPublishedmediaImagerel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedMediaImageRels.Add(mediaPublishedmediaImagerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedmediaImagerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedMediaImage(int id)
        {
            var mediaPublishedmediaImagerel = _ctx.MediaPublishedMediaImageRels.Find(id);
            _ctx.MediaPublishedMediaImageRels.Remove(mediaPublishedmediaImagerel);
        }

        #endregion


        #region MediaWebsiteEGroup

        public MediaPublishedMediaWebsiteEGroupRel GetMediaPublishedMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaPublishedMediaWebsiteEGroupRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedMediaWebsiteEGroupRel> GetMediaPublishedMediaWebsiteEGroups(Expression<Func<MediaPublishedMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedMediaWebsiteEGroupRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedMediaWebsiteEGroupRels.Where(x => x.MediaPublished.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedMediaWebsiteEGroup(MediaPublishedMediaWebsiteEGroupRel mediaPublishedmediaWebsiteEGrouprel)
        {
            if (mediaPublishedmediaWebsiteEGrouprel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedMediaWebsiteEGroupRels.Add(mediaPublishedmediaWebsiteEGrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedmediaWebsiteEGrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedMediaWebsiteEGroup(int id)
        {
            var mediaPublishedmediaWebsiteEGrouprel = _ctx.MediaPublishedMediaWebsiteEGroupRels.Find(id);
            _ctx.MediaPublishedMediaWebsiteEGroupRels.Remove(mediaPublishedmediaWebsiteEGrouprel);
        }

        #endregion


        #region MediaPublished

        public MediaPublishedMediaPublishedRel GetMediaPublishedMediaPublished(int id)
        {
            var rel = _ctx.MediaPublishedMediaPublishedRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.MediaPublished.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.MediaPublished.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedMediaPublishedRel> GetMediaPublishedMediaPublisheds(Expression<Func<MediaPublishedMediaPublishedRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedMediaPublishedRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.MediaPublished.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaPublished.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedMediaPublishedRels.Where(x => x.MediaPublished.DateDeleted == null && x.MediaPublished.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.MediaPublished.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedMediaPublished(MediaPublishedMediaPublishedRel mediaPublishedmediaPublishedrel)
        {
            if (mediaPublishedmediaPublishedrel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedMediaPublishedRels.Add(mediaPublishedmediaPublishedrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedmediaPublishedrel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedMediaPublished(int id)
        {
            var mediaPublishedmediaPublishedrel = _ctx.MediaPublishedMediaPublishedRels.Find(id);
            _ctx.MediaPublishedMediaPublishedRels.Remove(mediaPublishedmediaPublishedrel);
        }

        #endregion


        #region Vehicle

        public MediaPublishedVehicleRel GetMediaPublishedVehicle(int id)
        {
            var rel = _ctx.MediaPublishedVehicleRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedVehicleRel> GetMediaPublishedVehicles(Expression<Func<MediaPublishedVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedVehicleRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedVehicleRels.Where(x => x.MediaPublished.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedVehicle(MediaPublishedVehicleRel mediaPublishedvehiclerel)
        {
            if (mediaPublishedvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedVehicleRels.Add(mediaPublishedvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedVehicle(int id)
        {
            var mediaPublishedvehiclerel = _ctx.MediaPublishedVehicleRels.Find(id);
            _ctx.MediaPublishedVehicleRels.Remove(mediaPublishedvehiclerel);
        }

        #endregion


        #region Subscription

        public MediaPublishedSubscriptionRel GetMediaPublishedSubscription(int id)
        {
            var rel = _ctx.MediaPublishedSubscriptionRels.Find(id);
            if (rel.MediaPublished.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.MediaPublished.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaPublishedSubscriptionRel> GetMediaPublishedSubscriptions(Expression<Func<MediaPublishedSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaPublishedSubscriptionRels.Where(filter).Where(x => x.MediaPublished.DateDeleted == null && x.Subscription.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.MediaPublishedSubscriptionRels.Where(x => x.MediaPublished.DateDeleted == null && x.Subscription.DateDeleted == null && x.MediaPublished.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaPublishedSubscription(MediaPublishedSubscriptionRel mediaPublishedvehiclerel)
        {
            if (mediaPublishedvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaPublishedSubscriptionRels.Add(mediaPublishedvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaPublishedvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaPublishedSubscription(int id)
        {
            var mediaPublishedvehiclerel = _ctx.MediaPublishedSubscriptionRels.Find(id);
            _ctx.MediaPublishedSubscriptionRels.Remove(mediaPublishedvehiclerel);
        }

        #endregion


    }

    public interface IMediaPublishedRepository
    {
        IQueryable<MediaPublished> GetMediaPublisheds();
        IQueryable<MediaPublished> GetMediaPublisheds(Expression<Func<MediaPublished, bool>> filter);
        IQueryable<MediaPublished> GetMediaPublisheds(User user);
        IQueryable<MediaPublished> GetMediaPublisheds(User user, Expression<Func<MediaPublished, bool>> filter);
        MediaPublished GetMediaPublished(User user, int id);
        MediaPublished GetMediaPublished(int id);
        void InsertOrUpdate(MediaPublished mediaPublished);
        void Delete(int id);
        void Save();

        MediaPublishedEventRel GetMediaPublishedEvent(int id);
        IQueryable<MediaPublishedEventRel> GetMediaPublishedEvents(Expression<Func<MediaPublishedEventRel, bool>> filter);
        void InsertOrUpdateMediaPublishedEvent(MediaPublishedEventRel mediaPublishedeventrel);
        void DeleteMediaPublishedEvent(int id);

        MediaPublishedSubscriptionRel GetMediaPublishedSubscription(int id);
        IQueryable<MediaPublishedSubscriptionRel> GetMediaPublishedSubscriptions(Expression<Func<MediaPublishedSubscriptionRel, bool>> filter);
        void InsertOrUpdateMediaPublishedSubscription(MediaPublishedSubscriptionRel mediaPublishedSubscriptionrel);
        void DeleteMediaPublishedSubscription(int id);

        MediaPublishedMediaWebsiteEGroupRel GetMediaPublishedMediaWebsiteEGroup(int id);
        IQueryable<MediaPublishedMediaWebsiteEGroupRel> GetMediaPublishedMediaWebsiteEGroups(Expression<Func<MediaPublishedMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateMediaPublishedMediaWebsiteEGroup(MediaPublishedMediaWebsiteEGroupRel mediaPublishedMediaWebsiteEGrouprel);
        void DeleteMediaPublishedMediaWebsiteEGroup(int id);

        MediaPublishedMediaAudioVideoRel GetMediaPublishedMediaAudioVideo(int id);
        IQueryable<MediaPublishedMediaAudioVideoRel> GetMediaPublishedMediaAudioVideos(Expression<Func<MediaPublishedMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateMediaPublishedMediaAudioVideo(MediaPublishedMediaAudioVideoRel mediaPublishedMediaAudioVideorel);
        void DeleteMediaPublishedMediaAudioVideo(int id);

        MediaPublishedMediaCorrespondenceRel GetMediaPublishedMediaCorrespondence(int id);
        IQueryable<MediaPublishedMediaCorrespondenceRel> GetMediaPublishedMediaCorrespondences(Expression<Func<MediaPublishedMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateMediaPublishedMediaCorrespondence(MediaPublishedMediaCorrespondenceRel mediaPublishedMediaCorrespondencerel);
        void DeleteMediaPublishedMediaCorrespondence(int id);

        MediaPublishedMediaImageRel GetMediaPublishedMediaImage(int id);
        IQueryable<MediaPublishedMediaImageRel> GetMediaPublishedMediaImages(Expression<Func<MediaPublishedMediaImageRel, bool>> filter);
        void InsertOrUpdateMediaPublishedMediaImage(MediaPublishedMediaImageRel mediaPublishedMediaImagerel);
        void DeleteMediaPublishedMediaImage(int id);

        MediaPublishedMediaPublishedRel GetMediaPublishedMediaPublished(int chapterId);
        IQueryable<MediaPublishedMediaPublishedRel> GetMediaPublishedMediaPublisheds(Expression<Func<MediaPublishedMediaPublishedRel, bool>> filter);
        void InsertOrUpdateMediaPublishedMediaPublished(MediaPublishedMediaPublishedRel mediaPublishedMediaPublishedRel);
        void DeleteMediaPublishedMediaPublished(int id);

        MediaPublishedVehicleRel GetMediaPublishedVehicle(int id);
        IQueryable<MediaPublishedVehicleRel> GetMediaPublishedVehicles(Expression<Func<MediaPublishedVehicleRel, bool>> filter);
        void InsertOrUpdateMediaPublishedVehicle(MediaPublishedVehicleRel mediaPublishedvehiclerel);
        void DeleteMediaPublishedVehicle(int id);

        MediaPublishedContext GetMediaPublishedContext(int id);

    }
}