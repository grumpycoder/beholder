using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaWebsiteEGroupRepository : IMediaWebsiteEGroupRepository
    {

        private readonly ACDBContext _ctx;

        public MediaWebsiteEGroupRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups()
        {
            return _ctx.MediaWebsiteEGroups.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(Expression<Func<MediaWebsiteEGroup, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaWebsiteEGroups.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.MediaWebsiteEGroups.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(User user)
        {
            return _ctx.MediaWebsiteEGroups.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(b => b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(User user, Expression<Func<MediaWebsiteEGroup, bool>> filter)
        {
            IQueryable<MediaWebsiteEGroup> entity;
            if (filter != null)
            {
                entity = _ctx.MediaWebsiteEGroups.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            else
            {
                entity = _ctx.MediaWebsiteEGroups.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel);
            }
            return entity.Where(b => b.RemovalStatusId == null);
            //if (filter != null)
            //{
            //    return _ctx.MediaWebsiteEGroups.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.RemovalStatusId == null);
            //}
            //return _ctx.MediaWebsiteEGroups.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.RemovalStatusId == null);
        }

        public MediaWebsiteEGroup GetMediaWebsiteEGroup(User user, int id)
        {
            var websiteEGroup = _ctx.MediaWebsiteEGroups.Find(id);
            if (websiteEGroup.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && websiteEGroup.DateDeleted == null && websiteEGroup.RemovalStatusId == null)
            {
                return websiteEGroup;
            }
            return null;
        }

        public MediaWebsiteEGroupContext GetMediaWebsiteEGroupContext(int id)
        {
            return _ctx.MediaWebsiteEGroupContexts.Find(id);
        }


        public MediaWebsiteEGroup GetMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaWebsiteEGroups.Find(id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdate(MediaWebsiteEGroup mediaWebsiteEGroup)
        {
            if (mediaWebsiteEGroup.Id == default(int))
            {
                // Existing entity
                //if (mediaWebsiteEGroup.MediaWebsiteEGroupContext != null)
                //{
                //    // New entity
                //    _ctx.MediaWebsiteEGroupContexts.Add(mediaWebsiteEGroup.MediaWebsiteEGroupContext);
                //}
                _ctx.MediaWebsiteEGroups.Add(mediaWebsiteEGroup);
            }
            else
            {
                // Existing entity
                //if (mediaWebsiteEGroup.MediaWebsiteEGroupContext != null)
                //{
                //    if (mediaWebsiteEGroup.MediaWebsiteEGroupContextId == null || mediaWebsiteEGroup.MediaWebsiteEGroupContextId == default(int))
                //    {
                //        _ctx.MediaWebsiteEGroupContexts.Add(mediaWebsiteEGroup.MediaWebsiteEGroupContext);
                //        _ctx.Entry(mediaWebsiteEGroup).State = EntityState.Modified;
                //    }
                //    else
                //    {
                //        // Existing entity
                //        _ctx.Entry(mediaWebsiteEGroup.MediaWebsiteEGroupContext).State = EntityState.Modified;
                //        _ctx.Entry(mediaWebsiteEGroup).State = EntityState.Modified;
                //    }
                //}
                //else
                //{
                    _ctx.Entry(mediaWebsiteEGroup).State = EntityState.Modified;
                //}
            }
        }

        public void Delete(int id)
        {
            var mediaWebsiteEGroup = _ctx.MediaWebsiteEGroups.Find(id);
            _ctx.MediaWebsiteEGroups.Remove(mediaWebsiteEGroup);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region MediaAudioVideo

        public MediaWebsiteEGroupMediaAudioVideoRel GetMediaWebsiteEGroupMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Find(id);
            if (rel.MediaWebsiteEGroup.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.MediaAudioVideo.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaWebsiteEGroupMediaAudioVideoRel> GetMediaWebsiteEGroupMediaAudioVideos(Expression<Func<MediaWebsiteEGroupMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Where(filter).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(MediaWebsiteEGroupMediaAudioVideoRel mediaWebsiteEGroupmediaAudioVideorel)
        {
            if (mediaWebsiteEGroupmediaAudioVideorel.Id == default(int))
            {
                // New entity
                _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Add(mediaWebsiteEGroupmediaAudioVideorel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaWebsiteEGroupmediaAudioVideorel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaWebsiteEGroupMediaAudioVideo(int id)
        {
            var mediaWebsiteEGroupmediaAudioVideorel = _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Find(id);
            _ctx.MediaWebsiteEGroupMediaAudioVideoRels.Remove(mediaWebsiteEGroupmediaAudioVideorel);
        }

        #endregion


        #region MediaWebsiteEGroup

        public MediaWebsiteEGroupMediaWebsiteEGroupRel GetMediaWebsiteEGroupMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Find(id);
            if (rel.MediaWebsiteEGroup.DateDeleted == null && rel.MediaWebsiteEGroup2.DateDeleted == null && rel.MediaWebsiteEGroup2.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaWebsiteEGroupMediaWebsiteEGroupRel> GetMediaWebsiteEGroupMediaWebsiteEGroups(Expression<Func<MediaWebsiteEGroupMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Where(filter).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup2.DateDeleted == null && x.MediaWebsiteEGroup2.RemovalStatusId == null);
            }
            return _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.MediaWebsiteEGroup2.DateDeleted == null && x.MediaWebsiteEGroup2.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaWebsiteEGroupMediaWebsiteEGroup(MediaWebsiteEGroupMediaWebsiteEGroupRel mediawebsiteegroupmediawebsiteegrouprel)
        {
            if (mediawebsiteegroupmediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Add(mediawebsiteegroupmediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediawebsiteegroupmediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaWebsiteEGroupMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Find(id);
            _ctx.MediaWebsiteEGroupMediaWebsiteEGroupRels.Remove(rel);
        }

        #endregion


        #region Vehicle

        public MediaWebsiteEGroupVehicleRel GetMediaWebsiteEGroupVehicle(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupVehicleRels.Find(id);
            if (rel.MediaWebsiteEGroup.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.MediaWebsiteEGroup.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaWebsiteEGroupVehicleRel> GetMediaWebsiteEGroupVehicles(Expression<Func<MediaWebsiteEGroupVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaWebsiteEGroupVehicleRels.Where(filter).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.MediaWebsiteEGroupVehicleRels.Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaWebsiteEGroupVehicle(MediaWebsiteEGroupVehicleRel mediaWebsiteEGroupvehiclerel)
        {
            if (mediaWebsiteEGroupvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaWebsiteEGroupVehicleRels.Add(mediaWebsiteEGroupvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaWebsiteEGroupvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaWebsiteEGroupVehicle(int id)
        {
            var mediaWebsiteEGroupvehiclerel = _ctx.MediaWebsiteEGroupVehicleRels.Find(id);
            _ctx.MediaWebsiteEGroupVehicleRels.Remove(mediaWebsiteEGroupvehiclerel);
        }

        #endregion


        #region Subscription

        public MediaWebsiteEGroupSubscriptionRel GetMediaWebsiteEGroupSubscription(int id)
        {
            var rel = _ctx.MediaWebsiteEGroupSubscriptionRels.Find(id);
            if (rel.MediaWebsiteEGroup.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.MediaWebsiteEGroup.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaWebsiteEGroupSubscriptionRel> GetMediaWebsiteEGroupSubscriptions(Expression<Func<MediaWebsiteEGroupSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaWebsiteEGroupSubscriptionRels.Where(filter).Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.MediaWebsiteEGroupSubscriptionRels.Where(x => x.MediaWebsiteEGroup.DateDeleted == null && x.MediaWebsiteEGroup.RemovalStatusId == null).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaWebsiteEGroupSubscription(MediaWebsiteEGroupSubscriptionRel mediaWebsiteEGroupvehiclerel)
        {
            if (mediaWebsiteEGroupvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaWebsiteEGroupSubscriptionRels.Add(mediaWebsiteEGroupvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaWebsiteEGroupvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaWebsiteEGroupSubscription(int id)
        {
            var mediaWebsiteEGroupvehiclerel = _ctx.MediaWebsiteEGroupSubscriptionRels.Find(id);
            _ctx.MediaWebsiteEGroupSubscriptionRels.Remove(mediaWebsiteEGroupvehiclerel);
        }

        #endregion


    }

    public interface IMediaWebsiteEGroupRepository
    {
        IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups();
        IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(Expression<Func<MediaWebsiteEGroup, bool>> filter);
        IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(User user);
        IQueryable<MediaWebsiteEGroup> GetMediaWebsiteEGroups(User user, Expression<Func<MediaWebsiteEGroup, bool>> filter);
        MediaWebsiteEGroup GetMediaWebsiteEGroup(User user, int id);
        MediaWebsiteEGroup GetMediaWebsiteEGroup(int id);
        void InsertOrUpdate(MediaWebsiteEGroup mediaWebsiteEGroup);
        void Delete(int id);
        void Save();

        MediaWebsiteEGroupSubscriptionRel GetMediaWebsiteEGroupSubscription(int id);
        IQueryable<MediaWebsiteEGroupSubscriptionRel> GetMediaWebsiteEGroupSubscriptions(Expression<Func<MediaWebsiteEGroupSubscriptionRel, bool>> filter);
        void InsertOrUpdateMediaWebsiteEGroupSubscription(MediaWebsiteEGroupSubscriptionRel mediaWebsiteEGroupSubscriptionrel);
        void DeleteMediaWebsiteEGroupSubscription(int id);

        MediaWebsiteEGroupMediaAudioVideoRel GetMediaWebsiteEGroupMediaAudioVideo(int id);
        IQueryable<MediaWebsiteEGroupMediaAudioVideoRel> GetMediaWebsiteEGroupMediaAudioVideos(Expression<Func<MediaWebsiteEGroupMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(MediaWebsiteEGroupMediaAudioVideoRel mediaWebsiteEGroupMediaAudioVideorel);
        void DeleteMediaWebsiteEGroupMediaAudioVideo(int id);

        MediaWebsiteEGroupMediaWebsiteEGroupRel GetMediaWebsiteEGroupMediaWebsiteEGroup(int chapterId);
        IQueryable<MediaWebsiteEGroupMediaWebsiteEGroupRel> GetMediaWebsiteEGroupMediaWebsiteEGroups(Expression<Func<MediaWebsiteEGroupMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateMediaWebsiteEGroupMediaWebsiteEGroup(MediaWebsiteEGroupMediaWebsiteEGroupRel mediawebsiteegroupmediawebsiteegrouprel);
        void DeleteMediaWebsiteEGroupMediaWebsiteEGroup(int id);

        MediaWebsiteEGroupVehicleRel GetMediaWebsiteEGroupVehicle(int id);
        IQueryable<MediaWebsiteEGroupVehicleRel> GetMediaWebsiteEGroupVehicles(Expression<Func<MediaWebsiteEGroupVehicleRel, bool>> filter);
        void InsertOrUpdateMediaWebsiteEGroupVehicle(MediaWebsiteEGroupVehicleRel mediaWebsiteEGroupvehiclerel);
        void DeleteMediaWebsiteEGroupVehicle(int id);

        MediaWebsiteEGroupContext GetMediaWebsiteEGroupContext(int id);


    }
}