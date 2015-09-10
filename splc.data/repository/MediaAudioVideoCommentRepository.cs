using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class MediaAudioVideoCommentRepository : IMediaAudioVideoCommentRepository
    {
        readonly ACDBContext context;

        public MediaAudioVideoCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<MediaAudioVideoComment> All
        {
            get { return context.MediaAudioVideoComments; }
        }

        public IQueryable<MediaAudioVideoComment> AllIncluding(params Expression<Func<MediaAudioVideoComment, object>>[] includeProperties)
        {
            IQueryable<MediaAudioVideoComment> query = context.MediaAudioVideoComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<MediaAudioVideoComment> GetComments(Expression<Func<MediaAudioVideoComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.MediaAudioVideoComments.Where(filter);
            }
            return context.MediaAudioVideoComments;
        }

        public MediaAudioVideoComment Find(int id)
        {
            return context.MediaAudioVideoComments.Find(id);
        }

        public void InsertOrUpdate(MediaAudioVideoComment mediaAudioVideocomment)
        {
            if (mediaAudioVideocomment.Id == default(int))
            {
                // New entity
                context.MediaAudioVideoComments.Add(mediaAudioVideocomment);
            }
            else
            {
                // Existing entity
                context.Entry(mediaAudioVideocomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mediaAudioVideocomment = context.MediaAudioVideoComments.Find(id);
            context.MediaAudioVideoComments.Remove(mediaAudioVideocomment);
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

    public interface IMediaAudioVideoCommentRepository : IDisposable
    {
        IQueryable<MediaAudioVideoComment> All { get; }
        IQueryable<MediaAudioVideoComment> AllIncluding(params Expression<Func<MediaAudioVideoComment, object>>[] includeProperties);
        MediaAudioVideoComment Find(int id);
        IQueryable<MediaAudioVideoComment> GetComments(Expression<Func<MediaAudioVideoComment, bool>> filter);
        void InsertOrUpdate(MediaAudioVideoComment mediaAudioVideocomment);
        void Delete(int id);
        void Save();
    }
}
