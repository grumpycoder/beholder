using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class ChapterChapterRelRepository : IChapterChapterRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterChapterRel> All
        {
            get { return context.ChapterChapterRels; }
        }

        public IQueryable<ChapterChapterRel> AllIncluding(params Expression<Func<ChapterChapterRel, object>>[] includeProperties)
        {
            IQueryable<ChapterChapterRel> query = context.ChapterChapterRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterChapterRel Find(int id)
        {
            return context.ChapterChapterRels.Find(id);
        }

        public void InsertOrUpdate(ChapterChapterRel chapterchapterrel)
        {
            if (chapterchapterrel.Id == default(int)) {
                // New entity
                context.ChapterChapterRels.Add(chapterchapterrel);
            } else {
                // Existing entity
                context.Entry(chapterchapterrel).State = EntityState.Modified;
            }
        }

        public IQueryable<ChapterChapterRel> GetChapterChapters(Expression<Func<ChapterChapterRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterChapterRels.Where(filter);
            }
            return context.ChapterChapterRels;
        }

        public void Delete(int id)
        {
            var chapterchapterrel = context.ChapterChapterRels.Find(id);
            context.ChapterChapterRels.Remove(chapterchapterrel);
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

    public interface IChapterChapterRelRepository : IDisposable
    {
        IQueryable<ChapterChapterRel> All { get; }
        IQueryable<ChapterChapterRel> AllIncluding(params Expression<Func<ChapterChapterRel, object>>[] includeProperties);
        ChapterChapterRel Find(int id);
        IQueryable<ChapterChapterRel> GetChapterChapters(Expression<Func<ChapterChapterRel, bool>> filter);
        void InsertOrUpdate(ChapterChapterRel chapterchapterrel);
        void Delete(int id);
        void Save();
    }
}