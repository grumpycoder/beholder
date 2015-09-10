using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class ChapterEventRelRepository : IChapterEventRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterEventRel> All
        {
            get { return context.ChapterEventRels; }
        }

        public IQueryable<ChapterEventRel> AllIncluding(params Expression<Func<ChapterEventRel, object>>[] includeProperties)
        {
            IQueryable<ChapterEventRel> query = context.ChapterEventRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterEventRel Find(int id)
        {
            return context.ChapterEventRels.Find(id);
        }

        public IQueryable<ChapterEventRel> GetChapterEvents(Expression<Func<ChapterEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterEventRels.Where(filter);
            }
            return context.ChapterEventRels;
        }

        public void InsertOrUpdate(ChapterEventRel chaptereventrel)
        {
            if (chaptereventrel.Id == default(int))
            {
                // New entity
                context.ChapterEventRels.Add(chaptereventrel);
            }
            else
            {
                // Existing entity
                context.Entry(chaptereventrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var chaptereventrel = context.ChapterEventRels.Find(id);
            context.ChapterEventRels.Remove(chaptereventrel);
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

    public interface IChapterEventRelRepository : IDisposable
    {
        IQueryable<ChapterEventRel> All { get; }
        IQueryable<ChapterEventRel> AllIncluding(params Expression<Func<ChapterEventRel, object>>[] includeProperties);
        ChapterEventRel Find(int id);
        IQueryable<ChapterEventRel> GetChapterEvents(Expression<Func<ChapterEventRel, bool>> filter);
        void InsertOrUpdate(ChapterEventRel chaptereventrel);
        void Delete(int id);
        void Save();
    }
}