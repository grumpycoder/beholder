using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class ChapterOrganizationRelRepository : IChapterOrganizationRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterOrganizationRel> All
        {
            get { return context.ChapterOrganizationRels; }
        }

        public IQueryable<ChapterOrganizationRel> AllIncluding(params Expression<Func<ChapterOrganizationRel, object>>[] includeProperties)
        {
            IQueryable<ChapterOrganizationRel> query = context.ChapterOrganizationRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterOrganizationRel Find(int id)
        {
            return context.ChapterOrganizationRels.Find(id);
        }

        public void InsertOrUpdate(ChapterOrganizationRel chapterorganizationrel)
        {
            if (chapterorganizationrel.Id == default(int)) {
                // New entity
                context.ChapterOrganizationRels.Add(chapterorganizationrel);
            } else {
                // Existing entity
                context.Entry(chapterorganizationrel).State = EntityState.Modified;
            }
        }

        public IQueryable<ChapterOrganizationRel> GetChapterOrganizations(Expression<Func<ChapterOrganizationRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterOrganizationRels.Where(filter);
            }
            return context.ChapterOrganizationRels;
        }

        public void Delete(int id)
        {
            var chapterorganizationrel = context.ChapterOrganizationRels.Find(id);
            context.ChapterOrganizationRels.Remove(chapterorganizationrel);
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

    public interface IChapterOrganizationRelRepository : IDisposable
    {
        IQueryable<ChapterOrganizationRel> All { get; }
        IQueryable<ChapterOrganizationRel> AllIncluding(params Expression<Func<ChapterOrganizationRel, object>>[] includeProperties);
        ChapterOrganizationRel Find(int id);
        IQueryable<ChapterOrganizationRel> GetChapterOrganizations(Expression<Func<ChapterOrganizationRel, bool>> filter);
        void InsertOrUpdate(ChapterOrganizationRel chapterorganizationrel);
        void Delete(int id);
        void Save();
    }
}