using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class MediaImageMediaImageRelRepository : IMediaImageMediaImageRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<MediaImageMediaImageRel> All
        {
            get { return context.MediaImageMediaImageRels; }
        }

        public IQueryable<MediaImageMediaImageRel> AllIncluding(params Expression<Func<MediaImageMediaImageRel, object>>[] includeProperties)
        {
            IQueryable<MediaImageMediaImageRel> query = context.MediaImageMediaImageRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MediaImageMediaImageRel Find(int id)
        {
            return context.MediaImageMediaImageRels.Find(id);
        }

        public IQueryable<MediaImageMediaImageRel> GetMediaImageMediaImages(Expression<Func<MediaImageMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaImageMediaImageRels.Where(filter);
            }
            return context.MediaImageMediaImageRels;
        }

        public void InsertOrUpdate(MediaImageMediaImageRel mediaimagemediaimagerel)
        {
            if (mediaimagemediaimagerel.Id == default(long))
            {
                // New entity
                context.MediaImageMediaImageRels.Add(mediaimagemediaimagerel);
            }
            else
            {
                // Existing entity
                context.Entry(mediaimagemediaimagerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mediaimagemediaimagerel = context.MediaImageMediaImageRels.Find(id);
            context.MediaImageMediaImageRels.Remove(mediaimagemediaimagerel);
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

    public interface IMediaImageMediaImageRelRepository : IDisposable
    {
        IQueryable<MediaImageMediaImageRel> All { get; }
        IQueryable<MediaImageMediaImageRel> AllIncluding(params Expression<Func<MediaImageMediaImageRel, object>>[] includeProperties);
        MediaImageMediaImageRel Find(int id);
        IQueryable<MediaImageMediaImageRel> GetMediaImageMediaImages(Expression<Func<MediaImageMediaImageRel, bool>> filter);
        void InsertOrUpdate(MediaImageMediaImageRel mediaimagemediaimagerel);
        void Delete(int id);
        void Save();
    }
}