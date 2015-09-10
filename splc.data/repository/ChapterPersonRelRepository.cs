using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ChapterPersonRelRepository : IChapterPersonRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterPersonRel> All
        {
            get { return context.ChapterPersonRels; }
        }

        public IQueryable<ChapterPersonRel> AllIncluding(params Expression<Func<ChapterPersonRel, object>>[] includeProperties)
        {
            IQueryable<ChapterPersonRel> query = context.ChapterPersonRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterPersonRel Find(long id)
        {
            return context.ChapterPersonRels.Find(id);
        }

        public IQueryable<ChapterPersonRel> GetChapterPersons(Expression<Func<ChapterPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterPersonRels.Where(filter);
            }
            return context.ChapterPersonRels;
        }

        public void InsertOrUpdate(ChapterPersonRel chapterpersonrel)
        {
            if (chapterpersonrel.Id == default(long))
            {
                // New entity
                context.ChapterPersonRels.Add(chapterpersonrel);
            }
            else
            {
                // Existing entity
                context.Entry(chapterpersonrel).State = EntityState.Modified;
            }
        }

        public void Delete(long id)
        {
            var chapterpersonrel = context.ChapterPersonRels.Find(id);
            context.ChapterPersonRels.Remove(chapterpersonrel);
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

    public interface IChapterPersonRelRepository : IDisposable
    {
        IQueryable<ChapterPersonRel> All { get; }
        IQueryable<ChapterPersonRel> AllIncluding(params Expression<Func<ChapterPersonRel, object>>[] includeProperties);
        ChapterPersonRel Find(long id);
        IQueryable<ChapterPersonRel> GetChapterPersons(Expression<Func<ChapterPersonRel, bool>> filter);
        void InsertOrUpdate(ChapterPersonRel chapterpersonrel);
        void Delete(long id);
        void Save();
    }
}