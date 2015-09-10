using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaImageRepository : IMediaImageRepository
    {
     
        private readonly ACDBContext _ctx;

        public MediaImageRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public IQueryable<MediaImage> GetMediaImages()
        {
            return _ctx.MediaImages.Where(b => b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<MediaImage> GetMediaImages(Expression<Func<MediaImage, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImages.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.MediaImages.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaImage> GetMediaImages(User user)
        {
            return _ctx.MediaImages.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<MediaImage> GetMediaImages(User user, Expression<Func<MediaImage, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImages.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return _ctx.MediaImages.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public MediaImage GetMediaImage(User user, int id)
        {
            var mediaImage =_ctx.MediaImages.Find(id);
            if (mediaImage.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel)
            {
                return mediaImage; 
            }
            return null; 
        }

        public MediaImage GetMediaImage(int id)
        {
            var rel = _ctx.MediaImages.Find(id);
            if (rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public Image GetImage(int id)
        {
            return _ctx.Images.Find(id);
        }

        public void InsertOrUpdate(MediaImage mediaimage)
        {
            if (mediaimage.Id == default(int))
            {
                // Existing entity
                if (mediaimage.Image != null)
                {
                    // New entity
                    _ctx.Images.Add(mediaimage.Image);
                }
                _ctx.MediaImages.Add(mediaimage);
            }
            else
            {
                // Existing entity
                if (mediaimage.Image != null)
                {
                    if (mediaimage.ImageId == null || mediaimage.ImageId == default(int))
                    {
                        _ctx.Images.Add(mediaimage.Image);
                        _ctx.Entry(mediaimage).State = EntityState.Modified;
                    }
                    else
                    {
                        // Existing entity
                        _ctx.Entry(mediaimage.Image).State = EntityState.Modified;
                        _ctx.Entry(mediaimage).State = EntityState.Modified;
                    }
                }
                else
                {
                    _ctx.Entry(mediaimage).State = EntityState.Modified;
                }
            }
        }

        public void Delete(int id)
        {
            var mediaimage = _ctx.MediaImages.Find(id);
            _ctx.MediaImages.Remove(mediaimage);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Vehicle

        public MediaImageVehicleRel GetMediaImageVehicle(int id)
        {
            var rel = _ctx.MediaImageVehicleRels.Find(id);
            if (rel.MediaImage.DateDeleted == null && rel.Vehicle.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaImageVehicleRel> GetMediaImageVehicles(Expression<Func<MediaImageVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImageVehicleRels.Where(filter).Where(x => x.MediaImage.DateDeleted == null && x.Vehicle.DateDeleted == null);
            }
            return _ctx.MediaImageVehicleRels.Where(x => x.MediaImage.DateDeleted == null && x.Vehicle.DateDeleted == null);
        }

        public void InsertOrUpdateMediaImageVehicle(MediaImageVehicleRel mediaImagevehiclerel)
        {
            if (mediaImagevehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaImageVehicleRels.Add(mediaImagevehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaImagevehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageVehicle(int id)
        {
            var mediaImagevehiclerel = _ctx.MediaImageVehicleRels.Find(id);
            _ctx.MediaImageVehicleRels.Remove(mediaImagevehiclerel);
        }

        #endregion


        #region MediaImage

        public MediaImage GetMediaImageMediaImage(int id)
        {
            var rel = _ctx.MediaImages.Find(id);
            if (rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaImageMediaImageRel> GetMediaImageMediaImages(User currentUser, int id)
        {
            //TODO: Filter by current user and datedeleted
            return _ctx.MediaImageMediaImageRels.Where(x => x.MediaImageId == id);
        }

        public void InsertOrUpdateMediaImageMediaImage(MediaImageMediaImageRel mediaImageMediaImageRel)
        {
            if (mediaImageMediaImageRel.Id == default(long))
            {
                // New entity
                _ctx.MediaImageMediaImageRels.Add(mediaImageMediaImageRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaImageMediaImageRel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageMediaImage(int id)
        {
            var mediaimagemediaimagerel = _ctx.MediaImageMediaImageRels.Find(id);
            _ctx.MediaImageMediaImageRels.Remove(mediaimagemediaimagerel);
        }

        #endregion


        # region MediaAudioVideo

        public MediaImageMediaAudioVideoRel GetMediaImageMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaImageMediaAudioVideoRels.Find(id);
            if (rel.MediaImage.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaImageMediaAudioVideoRel> GetMediaImageMediaAudioVideos(Expression<Func<MediaImageMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImageMediaAudioVideoRels.Where(filter).Where(x => x.MediaImage.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null);
            }
            return _ctx.MediaImageMediaAudioVideoRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null);
        }

        public void InsertOrUpdateMediaImageMediaAudioVideo(MediaImageMediaAudioVideoRel mediaImagemediaaudiovideorel)
        {
            if (mediaImagemediaaudiovideorel.Id == default(int))
            {
                // New entity
                _ctx.MediaImageMediaAudioVideoRels.Add(mediaImagemediaaudiovideorel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaImagemediaaudiovideorel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaImageMediaAudioVideoRels.Find(id);
            _ctx.MediaImageMediaAudioVideoRels.Remove(rel);
        }
        
        #endregion


        #region MediaWebsiteEGroup

        public MediaImageMediaWebsiteEGroupRel GetMediaImageMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaImageMediaWebsiteEGroupRels.Find(id);
            if (rel.MediaImage.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaImageMediaWebsiteEGroupRel> GetMediaImageMediaWebsiteEGroups(Expression<Func<MediaImageMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImageMediaWebsiteEGroupRels.Where(filter).Where(x => x.MediaImage.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null);
            }
            return _ctx.MediaImageMediaWebsiteEGroupRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null);
        }

        public void InsertOrUpdateMediaImageMediaWebsiteEGroup(MediaImageMediaWebsiteEGroupRel mediaImagemediawebsiteegrouprel)
        {
            if (mediaImagemediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.MediaImageMediaWebsiteEGroupRels.Add(mediaImagemediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaImagemediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.MediaImageMediaWebsiteEGroupRels.Find(id);
            _ctx.MediaImageMediaWebsiteEGroupRels.Remove(rel);
        }

        #endregion


        #region Subscription

        public MediaImageSubscriptionRel GetMediaImageSubscription(int id)
        {
            var rel = _ctx.MediaImageSubscriptionRels.Find(id);
            if (rel.MediaImage.DateDeleted == null && rel.Subscription.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaImageSubscriptionRel> GetMediaImageSubscriptions(Expression<Func<MediaImageSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaImageSubscriptionRels.Where(filter).Where(x => x.MediaImage.DateDeleted == null && x.Subscription.DateDeleted == null);
            }
            return _ctx.MediaImageSubscriptionRels.Where(x => x.MediaImage.DateDeleted == null && x.Subscription.DateDeleted == null);
        }

        public void InsertOrUpdateMediaImageSubscription(MediaImageSubscriptionRel mediaImagesubscriptionrel)
        {
            if (mediaImagesubscriptionrel.Id == default(int))
            {
                // New entity
                _ctx.MediaImageSubscriptionRels.Add(mediaImagesubscriptionrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaImagesubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaImageSubscription(int id)
        {
            var mediaImagesubscriptionrel = _ctx.MediaImageSubscriptionRels.Find(id);
            _ctx.MediaImageSubscriptionRels.Remove(mediaImagesubscriptionrel);
        }

        #endregion


    }

    public interface IMediaImageRepository
    {
        IQueryable<MediaImage> GetMediaImages();
        IQueryable<MediaImage> GetMediaImages(Expression<Func<MediaImage, bool>> filter);
        IQueryable<MediaImage> GetMediaImages(User user);
        IQueryable<MediaImage> GetMediaImages(User user, Expression<Func<MediaImage, bool>> filter);
        MediaImage GetMediaImage(User user, int id);
        MediaImage GetMediaImage(int id);
        void InsertOrUpdate(MediaImage mediaimage);
        void Delete(int id);
        void Save();

        MediaImageVehicleRel GetMediaImageVehicle(int id);
        IQueryable<MediaImageVehicleRel> GetMediaImageVehicles(Expression<Func<MediaImageVehicleRel, bool>> filter);
        void InsertOrUpdateMediaImageVehicle(MediaImageVehicleRel mediaImagevehiclerel);
        void DeleteMediaImageVehicle(int id);

        MediaImageMediaAudioVideoRel GetMediaImageMediaAudioVideo(int id);
        IQueryable<MediaImageMediaAudioVideoRel> GetMediaImageMediaAudioVideos(Expression<Func<MediaImageMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateMediaImageMediaAudioVideo(MediaImageMediaAudioVideoRel mediaImagemediaaudiovideorel);
        void DeleteMediaImageMediaAudioVideo(int id);

        MediaImage GetMediaImageMediaImage(int id);
        IQueryable<MediaImageMediaImageRel> GetMediaImageMediaImages(User currentUser, int id);
        void InsertOrUpdateMediaImageMediaImage(MediaImageMediaImageRel mediaImageMediaImageRel);
        void DeleteMediaImageMediaImage(int id);

        MediaImageMediaWebsiteEGroupRel GetMediaImageMediaWebsiteEGroup(int id);
        IQueryable<MediaImageMediaWebsiteEGroupRel> GetMediaImageMediaWebsiteEGroups(Expression<Func<MediaImageMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateMediaImageMediaWebsiteEGroup(MediaImageMediaWebsiteEGroupRel mediaImagemediawebsiteegrouprel);
        void DeleteMediaImageMediaWebsiteEGroup(int id);

        MediaImageSubscriptionRel GetMediaImageSubscription(int id);
        IQueryable<MediaImageSubscriptionRel> GetMediaImageSubscriptions(Expression<Func<MediaImageSubscriptionRel, bool>> filter);
        void InsertOrUpdateMediaImageSubscription(MediaImageSubscriptionRel mediaImagesubscriptionrel);
        void DeleteMediaImageSubscription(int id);

        Image GetImage(int id);

    }
}