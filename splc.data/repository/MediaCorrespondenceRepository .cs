using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaCorrespondenceRepository : IMediaCorrespondenceRepository
    {
     
        private readonly ACDBContext _ctx;

        public MediaCorrespondenceRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public IQueryable<MediaCorrespondence> GetMediaCorrespondences()
        {
            return _ctx.MediaCorrespondences.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaCorrespondence> GetMediaCorrespondences(Expression<Func<MediaCorrespondence, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondences.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondences.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaCorrespondence> GetMediaCorrespondences(User user)
        {
            return _ctx.MediaCorrespondences.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }
         
        public IQueryable<MediaCorrespondence> GetMediaCorrespondences(User user, Expression<Func<MediaCorrespondence, bool>> filter)
        {
            IQueryable<MediaCorrespondence> entity;
            if (filter != null)
            {
                entity = _ctx.MediaCorrespondences.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            else
            {
                entity = _ctx.MediaCorrespondences.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            return entity.Where(b => b.RemovalStatusId == null);

            //if (filter != null)
            //{
            //    return _ctx.MediaCorrespondences.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null);
            //}
            //return _ctx.MediaCorrespondences.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null);
        }

        public MediaCorrespondence GetMediaCorrespondence(User user, int id)
        {
            var correspondence = _ctx.MediaCorrespondences.Find(id);
            if (correspondence.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && correspondence.DateDeleted == null && correspondence.RemovalStatusId == null)
            {
                return correspondence; 
            }
            return null; 
        }

        public MediaCorrespondence GetMediaCorrespondence(int id)
        {
            var rel = _ctx.MediaCorrespondences.Find(id);
            if (rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public MediaCorrespondenceContext GetMediaCorrespondenceContext(int id)
        {
            return _ctx.MediaCorrespondenceContexts.Find(id);
        }

        public void InsertOrUpdate(MediaCorrespondence mediaCorrespondence)
        {
            if (mediaCorrespondence.Id == default(int))
            {
                // Existing entity
                //if (mediaCorrespondence.MediaCorrespondenceContext != null)
                //{
                //    // New entity
                //    _ctx.MediaCorrespondenceContexts.Add(mediaCorrespondence.MediaCorrespondenceContext);
                //}
                _ctx.MediaCorrespondences.Add(mediaCorrespondence);
            }
            else
            {
                // Existing entity
                //if (mediaCorrespondence.MediaCorrespondenceContext != null)
                //{
                //    if (mediaCorrespondence.MediaCorrespondenceContextId == null || mediaCorrespondence.MediaCorrespondenceContextId == default(int))
                //    {
                //        _ctx.MediaCorrespondenceContexts.Add(mediaCorrespondence.MediaCorrespondenceContext);
                //        _ctx.Entry(mediaCorrespondence).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        // Existing entity
                //        _ctx.Entry(mediaCorrespondence.MediaCorrespondenceContext).State = EntityState.Modified;
                //        _ctx.Entry(mediaCorrespondence).State = EntityState.Modified;
                //    }
                //}
                //else
                //{
                    _ctx.Entry(mediaCorrespondence).State = EntityState.Modified;
                //}
            }
        }

        public void Delete(int id)
        {
            var mediaCorrespondence = _ctx.MediaCorrespondences.Find(id);
            _ctx.MediaCorrespondences.Remove(mediaCorrespondence);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Events

        public MediaCorrespondenceEventRel GetMediaCorrespondenceEvent(int id)
        {
            var rel = _ctx.MediaCorrespondenceEventRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.Event.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceEventRel> GetMediaCorrespondenceEvents(Expression<Func<MediaCorrespondenceEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceEventRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.Event.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceEventRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.Event.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceEvent(MediaCorrespondenceEventRel mediaCorrespondenceeventrel)
        {
            if (mediaCorrespondenceeventrel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceEventRels.Add(mediaCorrespondenceeventrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondenceeventrel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceEvent(int id)
        {
            var rel = _ctx.MediaCorrespondenceEventRels.Find(id);
            _ctx.MediaCorrespondenceEventRels.Remove(rel);
        }


        #endregion


        #region MediaAudioVideos

        public MediaCorrespondenceMediaAudioVideoRel GetMediaCorrespondenceMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaAudioVideoRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.MediaAudioVideo.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceMediaAudioVideoRel> GetMediaCorrespondenceMediaAudioVideos(Expression<Func<MediaCorrespondenceMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceMediaAudioVideoRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceMediaAudioVideoRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceMediaAudioVideo(MediaCorrespondenceMediaAudioVideoRel mediaCorrespondencemediaaudiovideorel)
        {
            if (mediaCorrespondencemediaaudiovideorel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceMediaAudioVideoRels.Add(mediaCorrespondencemediaaudiovideorel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencemediaaudiovideorel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaAudioVideoRels.Find(id);
            _ctx.MediaCorrespondenceMediaAudioVideoRels.Remove(rel);
        }


        #endregion


        #region MediaCorrespondences

        public MediaCorrespondenceMediaCorrespondenceRel GetMediaCorrespondenceMediaCorrespondence(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaCorrespondenceRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.MediaCorrespondence2.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.MediaCorrespondence2.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceMediaCorrespondenceRel> GetMediaCorrespondenceMediaCorrespondences(Expression<Func<MediaCorrespondenceMediaCorrespondenceRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceMediaCorrespondenceRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence2.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaCorrespondence2.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceMediaCorrespondenceRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaCorrespondence2.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaCorrespondence2.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceMediaCorrespondence(MediaCorrespondenceMediaCorrespondenceRel mediaCorrespondencemediacorrespondencerel)
        {
            if (mediaCorrespondencemediacorrespondencerel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceMediaCorrespondenceRels.Add(mediaCorrespondencemediacorrespondencerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencemediacorrespondencerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceMediaCorrespondence(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaCorrespondenceRels.Find(id);
            _ctx.MediaCorrespondenceMediaCorrespondenceRels.Remove(rel);
        }


        #endregion


        #region MediaImages

        public MediaCorrespondenceMediaImageRel GetMediaCorrespondenceMediaImage(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaImageRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.MediaImage.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.MediaImage.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceMediaImageRel> GetMediaCorrespondenceMediaImages(Expression<Func<MediaCorrespondenceMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceMediaImageRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaImage.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceMediaImageRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaImage.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceMediaImage(MediaCorrespondenceMediaImageRel mediaCorrespondencemediaimagerel)
        {
            if (mediaCorrespondencemediaimagerel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceMediaImageRels.Add(mediaCorrespondencemediaimagerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencemediaimagerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceMediaImage(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaImageRels.Find(id);
            _ctx.MediaCorrespondenceMediaImageRels.Remove(rel);
        }


        #endregion


        #region MediaWebsiteEGroups

        public MediaCorrespondenceMediaWebsiteEGroupRel GetMediaCorrespondenceMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceMediaWebsiteEGroupRel> GetMediaCorrespondenceMediaWebsiteEGroups(Expression<Func<MediaCorrespondenceMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(MediaCorrespondenceMediaWebsiteEGroupRel mediaCorrespondencemediawebsiteegrouprel)
        {
            if (mediaCorrespondencemediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Add(mediaCorrespondencemediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencemediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Find(id);
            _ctx.MediaCorrespondenceMediaWebsiteEGroupRels.Remove(rel);
        }


        #endregion


        #region Subscriptions

        public MediaCorrespondenceSubscriptionRel GetMediaCorrespondenceSubscription(int id)
        {
            var rel = _ctx.MediaCorrespondenceSubscriptionRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceSubscriptionRel> GetMediaCorrespondenceSubscriptions(Expression<Func<MediaCorrespondenceSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceSubscriptionRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.Subscription.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceSubscriptionRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.Subscription.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceSubscription(MediaCorrespondenceSubscriptionRel mediaCorrespondencesubscriptionrel)
        {
            if (mediaCorrespondencesubscriptionrel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceSubscriptionRels.Add(mediaCorrespondencesubscriptionrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencesubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceSubscription(int id)
        {
            var rel = _ctx.MediaCorrespondenceSubscriptionRels.Find(id);
            _ctx.MediaCorrespondenceSubscriptionRels.Remove(rel);
        }


        #endregion


        #region Vehicles

        public MediaCorrespondenceVehicleRel GetMediaCorrespondenceVehicle(int id)
        {
            var rel = _ctx.MediaCorrespondenceVehicleRels.Find(id);
            if (rel.MediaCorrespondence.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.MediaCorrespondence.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaCorrespondenceVehicleRel> GetMediaCorrespondenceVehicles(Expression<Func<MediaCorrespondenceVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaCorrespondenceVehicleRels.Where(filter).Where(x => x.MediaCorrespondence.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.MediaCorrespondenceVehicleRels.Where(x => x.MediaCorrespondence.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaCorrespondence.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaCorrespondenceVehicle(MediaCorrespondenceVehicleRel mediaCorrespondencevehiclerel)
        {
            if (mediaCorrespondencevehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaCorrespondenceVehicleRels.Add(mediaCorrespondencevehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaCorrespondencevehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaCorrespondenceVehicle(int id)
        {
            var rel = _ctx.MediaCorrespondenceVehicleRels.Find(id);
            _ctx.MediaCorrespondenceVehicleRels.Remove(rel);
        }


        #endregion


    }

    public interface IMediaCorrespondenceRepository
    {
        IQueryable<MediaCorrespondence> GetMediaCorrespondences();
        IQueryable<MediaCorrespondence> GetMediaCorrespondences(Expression<Func<MediaCorrespondence, bool>> filter);
        IQueryable<MediaCorrespondence> GetMediaCorrespondences(User user);
        IQueryable<MediaCorrespondence> GetMediaCorrespondences(User user, Expression<Func<MediaCorrespondence, bool>> filter);
        MediaCorrespondence GetMediaCorrespondence(User user, int id);
        MediaCorrespondence GetMediaCorrespondence(int id);
        void InsertOrUpdate(MediaCorrespondence mediaCorrespondence);
        void Delete(int id);
        void Save();


        MediaCorrespondenceEventRel GetMediaCorrespondenceEvent(int id);
        IQueryable<MediaCorrespondenceEventRel> GetMediaCorrespondenceEvents(Expression<Func<MediaCorrespondenceEventRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceEvent(MediaCorrespondenceEventRel mediaCorrespondenceeventrel);
        void DeleteMediaCorrespondenceEvent(int id);


        MediaCorrespondenceMediaAudioVideoRel GetMediaCorrespondenceMediaAudioVideo(int id);
        IQueryable<MediaCorrespondenceMediaAudioVideoRel> GetMediaCorrespondenceMediaAudioVideos(Expression<Func<MediaCorrespondenceMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceMediaAudioVideo(MediaCorrespondenceMediaAudioVideoRel mediacorrespondencemediaaudiovideorel);
        void DeleteMediaCorrespondenceMediaAudioVideo(int id);


        MediaCorrespondenceMediaCorrespondenceRel GetMediaCorrespondenceMediaCorrespondence(int id);
        IQueryable<MediaCorrespondenceMediaCorrespondenceRel> GetMediaCorrespondenceMediaCorrespondences(Expression<Func<MediaCorrespondenceMediaCorrespondenceRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceMediaCorrespondence(MediaCorrespondenceMediaCorrespondenceRel mediacorrespondencemediacorrespondencerel);
        void DeleteMediaCorrespondenceMediaCorrespondence(int id);


        MediaCorrespondenceMediaImageRel GetMediaCorrespondenceMediaImage(int id);
        IQueryable<MediaCorrespondenceMediaImageRel> GetMediaCorrespondenceMediaImages(Expression<Func<MediaCorrespondenceMediaImageRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceMediaImage(MediaCorrespondenceMediaImageRel mediacorrespondencemediaimagerel);
        void DeleteMediaCorrespondenceMediaImage(int id);


        MediaCorrespondenceMediaWebsiteEGroupRel GetMediaCorrespondenceMediaWebsiteEGroup(int id);
        IQueryable<MediaCorrespondenceMediaWebsiteEGroupRel> GetMediaCorrespondenceMediaWebsiteEGroups(Expression<Func<MediaCorrespondenceMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(MediaCorrespondenceMediaWebsiteEGroupRel mediacorrespondencemediawebsiteegrouprel);
        void DeleteMediaCorrespondenceMediaWebsiteEGroup(int id);


        MediaCorrespondenceSubscriptionRel GetMediaCorrespondenceSubscription(int id);
        IQueryable<MediaCorrespondenceSubscriptionRel> GetMediaCorrespondenceSubscriptions(Expression<Func<MediaCorrespondenceSubscriptionRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceSubscription(MediaCorrespondenceSubscriptionRel mediacorrespondencesubscriptionrel);
        void DeleteMediaCorrespondenceSubscription(int id);


        MediaCorrespondenceVehicleRel GetMediaCorrespondenceVehicle(int id);
        IQueryable<MediaCorrespondenceVehicleRel> GetMediaCorrespondenceVehicles(Expression<Func<MediaCorrespondenceVehicleRel, bool>> filter);
        void InsertOrUpdateMediaCorrespondenceVehicle(MediaCorrespondenceVehicleRel mediaCorrespondencevehiclerel);
        void DeleteMediaCorrespondenceVehicle(int id);

        MediaCorrespondenceContext GetMediaCorrespondenceContext(int id);

    }
}