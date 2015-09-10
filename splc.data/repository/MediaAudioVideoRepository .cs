using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaAudioVideoRepository : IMediaAudioVideoRepository
    {
     
        private readonly ACDBContext _ctx;

        public MediaAudioVideoRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public IQueryable<MediaAudioVideo> GetMediaAudioVideos()
        {
            return _ctx.MediaAudioVideos.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaAudioVideo> GetMediaAudioVideos(Expression<Func<MediaAudioVideo, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaAudioVideos.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.MediaAudioVideos.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<MediaAudioVideo> GetMediaAudioVideos(User user)
        {
            return _ctx.MediaAudioVideos.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<MediaAudioVideo> GetMediaAudioVideos(User user, Expression<Func<MediaAudioVideo, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaAudioVideos.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return _ctx.MediaAudioVideos.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public MediaAudioVideo GetMediaAudioVideo(User user, int id)
        {
             var mediaAudioVideo = _ctx.MediaAudioVideos.Find(id);
             if (mediaAudioVideo.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && mediaAudioVideo.DateDeleted == null && mediaAudioVideo.RemovalStatusId == null)
             {
                 return mediaAudioVideo;
             }
             return null;
        }

        public MediaAudioVideo GetMediaAudioVideo(int id)
        {
            var rel = _ctx.MediaAudioVideos.Find(id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public Image GetImage(int id)
        {
            return _ctx.Images.Find(id);
        }

        public void InsertOrUpdate(MediaAudioVideo mediaAudioVideo)
        {
            if (mediaAudioVideo.Id == default(int))
            {
                // New entity
                _ctx.MediaAudioVideos.Add(mediaAudioVideo);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaAudioVideo).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mediaAudioVideo = _ctx.MediaAudioVideos.Find(id);
            _ctx.MediaAudioVideos.Remove(mediaAudioVideo);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Vehicle

        public MediaAudioVideoVehicleRel GetMediaAudioVideoVehicle(int id)
        {
            var rel = _ctx.MediaAudioVideoVehicleRels.Find(id);
            if (rel.MediaAudioVideo.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.MediaAudioVideo.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<MediaAudioVideoVehicleRel> GetMediaAudioVideoVehicles(Expression<Func<MediaAudioVideoVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.MediaAudioVideoVehicleRels.Where(filter).Where(x => x.MediaAudioVideo.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.MediaAudioVideoVehicleRels.Where(x => x.MediaAudioVideo.DateDeleted == null && x.Vehicle.DateDeleted == null && x.MediaAudioVideo.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateMediaAudioVideoVehicle(MediaAudioVideoVehicleRel mediaAudioVideoVehiclerel)
        {
            if (mediaAudioVideoVehiclerel.Id == default(int))
            {
                // New entity
                _ctx.MediaAudioVideoVehicleRels.Add(mediaAudioVideoVehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(mediaAudioVideoVehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteMediaAudioVideoVehicle(int id)
        {
            var mediaAudioVideovehiclerel = _ctx.MediaAudioVideoVehicleRels.Find(id);
            _ctx.MediaAudioVideoVehicleRels.Remove(mediaAudioVideovehiclerel);
        }

        #endregion

   
    }

    public interface IMediaAudioVideoRepository
    {
        IQueryable<MediaAudioVideo> GetMediaAudioVideos();
        IQueryable<MediaAudioVideo> GetMediaAudioVideos(Expression<Func<MediaAudioVideo, bool>> filter);
        IQueryable<MediaAudioVideo> GetMediaAudioVideos(User user);
        IQueryable<MediaAudioVideo> GetMediaAudioVideos(User user,Expression<Func<MediaAudioVideo, bool>> filter);
        MediaAudioVideo GetMediaAudioVideo(User user, int id);
        MediaAudioVideo GetMediaAudioVideo(int id);
        Image GetImage(int id);
        void InsertOrUpdate(MediaAudioVideo mediaAudioVideo);
        void Delete(int id);
        void Save();

        MediaAudioVideoVehicleRel GetMediaAudioVideoVehicle(int id);
        IQueryable<MediaAudioVideoVehicleRel> GetMediaAudioVideoVehicles(Expression<Func<MediaAudioVideoVehicleRel, bool>> filter);
        void InsertOrUpdateMediaAudioVideoVehicle(MediaAudioVideoVehicleRel mediaAudioVideoVehiclerel);
        void DeleteMediaAudioVideoVehicle(int id);

    }
}