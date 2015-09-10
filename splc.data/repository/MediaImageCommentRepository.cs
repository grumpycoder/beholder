using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaImageCommentRepository : IMediaImageCommentRepository
    {
        readonly ACDBContext context;

        public MediaImageCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<MediaImageComment> All
        {
            get { return context.MediaImageComments; }
        }

        public IQueryable<MediaImageComment> AllIncluding(params Expression<Func<MediaImageComment, object>>[] includeProperties)
        {
            IQueryable<MediaImageComment> query = context.MediaImageComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<MediaImageComment> GetComments(Expression<Func<MediaImageComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaImageComments.Where(filter);
            }
            return context.MediaImageComments;
        }

        public MediaImageComment Find(int id)
        {
            return context.MediaImageComments.Find(id);
        }

        public void InsertOrUpdate(MediaImageComment mediaImagecomment)
        {
            if (mediaImagecomment.Id == default(int))
            {
                // New entity
                context.MediaImageComments.Add(mediaImagecomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediaImagecomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mediaImagecomment = context.MediaImageComments.Find(id);
            context.MediaImageComments.Remove(mediaImagecomment);
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

    public interface IMediaImageCommentRepository : IDisposable
    {
        IQueryable<MediaImageComment> All { get; }
        IQueryable<MediaImageComment> AllIncluding(params Expression<Func<MediaImageComment, object>>[] includeProperties);
        MediaImageComment Find(int id);
        IQueryable<MediaImageComment> GetComments(Expression<Func<MediaImageComment, bool>> filter);
        void InsertOrUpdate(MediaImageComment mediaImagecomment);
        void Delete(int id);
        void Save();
    }
}
