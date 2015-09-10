using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{ 
    public class MediaImageVehicleRelRepository : IMediaImageVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<MediaImageVehicleRel> All
        {
            get { return context.MediaImageVehicleRels; }
        }

        public IQueryable<MediaImageVehicleRel> GetMediaImageVehicles(Expression<Func<MediaImageVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaImageVehicleRels.Where(filter);
            }
            return context.MediaImageVehicleRels;
        }


        public IQueryable<MediaImageVehicleRel> AllIncluding(params Expression<Func<MediaImageVehicleRel, object>>[] includeProperties)
        {
            IQueryable<MediaImageVehicleRel> query = context.MediaImageVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MediaImageVehicleRel Find(int id)
        {
            return context.MediaImageVehicleRels.Find(id);
        }

        public IQueryable<MediaImageVehicleRel> Get(Expression<Func<MediaImageVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaImageVehicleRels.Where(filter);
            }
            return context.MediaImageVehicleRels;
        }

        public void InsertOrUpdate(MediaImageVehicleRel mediaImagevehiclerel)
        {
            if (mediaImagevehiclerel.Id == default(int)) {
                // New entity
                context.MediaImageVehicleRels.Add(mediaImagevehiclerel);
            } else {
                // Existing entity
                context.Entry(mediaImagevehiclerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mediaImagevehiclerel = context.MediaImageVehicleRels.Find(id);
            context.MediaImageVehicleRels.Remove(mediaImagevehiclerel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IMediaImageVehicleRelRepository : IDisposable
    {
        IQueryable<MediaImageVehicleRel> All { get; }
        IQueryable<MediaImageVehicleRel> AllIncluding(params Expression<Func<MediaImageVehicleRel, object>>[] includeProperties);
        MediaImageVehicleRel Find(int id);
        IQueryable<MediaImageVehicleRel> Get(Expression<Func<MediaImageVehicleRel, bool>> filter);
        IQueryable<MediaImageVehicleRel> GetMediaImageVehicles(Expression<Func<MediaImageVehicleRel, bool>> filter);
        void InsertOrUpdate(MediaImageVehicleRel mediaImagevehiclerel);
        void Delete(int id);
        void Save();
    }
}