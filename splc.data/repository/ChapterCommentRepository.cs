using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ChapterCommentRepository : IChapterCommentRepository
    {
        readonly ACDBContext context;

        public ChapterCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ChapterComment> All
        {
            get { return context.ChapterComments; }
        }

        public IQueryable<ChapterComment> AllIncluding(params Expression<Func<ChapterComment, object>>[] includeProperties)
        {
            IQueryable<ChapterComment> query = context.ChapterComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<ChapterComment> GetComments(Expression<Func<ChapterComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterComments.Where(filter);
            }
            return context.ChapterComments;
        }

        public ChapterComment Find(int id)
        {
            return context.ChapterComments.Find(id);
        }

        public void InsertOrUpdate(ChapterComment chaptercomment)
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

        public void Delete(int id)
        {
            var chaptercomment = context.ChapterComments.Find(id);
            context.ChapterComments.Remove(chaptercomment);
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

    public interface IChapterCommentRepository : IDisposable
    {
        IQueryable<ChapterComment> All { get; }
        IQueryable<ChapterComment> AllIncluding(params Expression<Func<ChapterComment, object>>[] includeProperties);
        ChapterComment Find(int id);
        IQueryable<ChapterComment> GetComments(Expression<Func<ChapterComment, bool>> filter);
        void InsertOrUpdate(ChapterComment chaptercomment);
        void Delete(int id);
        void Save();
    }
}
