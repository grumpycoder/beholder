using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ChapterContactInfoRelRepository : IChapterContactInfoRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterContactInfoRel> All
        {
            get { return context.ChapterContactInfoRels; }
        }

        public IQueryable<ChapterContactInfoRel> AllIncluding(params Expression<Func<ChapterContactInfoRel, object>>[] includeProperties)
        {
            IQueryable<ChapterContactInfoRel> query = context.ChapterContactInfoRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterContactInfoRel Find(int id)
        {
            return context.ChapterContactInfoRels.Find(id);
        }

        public IQueryable<ChapterContactInfoRel> GetContactInfo(Expression<Func<ChapterContactInfoRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterContactInfoRels.Where(filter);
            }
            return context.ChapterContactInfoRels;
        }

        public void InsertOrUpdate(ChapterContactInfoRel chaptercontactinforel)
        {
            if (chaptercontactinforel.Id == default(int)) {
                // New entity
                context.ContactInfo.Add(chaptercontactinforel.ContactInfo);
                context.ChapterContactInfoRels.Add(chaptercontactinforel);
            } else {
                // Existing entity
                context.Entry(chaptercontactinforel).State = EntityState.Modified;
                context.Entry(chaptercontactinforel.ContactInfo).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var chaptercontactinforel = context.ChapterContactInfoRels.Find(id);
            context.ChapterContactInfoRels.Remove(chaptercontactinforel);
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

    public interface IChapterContactInfoRelRepository : IDisposable
    {
        IQueryable<ChapterContactInfoRel> All { get; }
        IQueryable<ChapterContactInfoRel> AllIncluding(params Expression<Func<ChapterContactInfoRel, object>>[] includeProperties);
        ChapterContactInfoRel Find(int id);
        IQueryable<ChapterContactInfoRel> GetContactInfo(Expression<Func<ChapterContactInfoRel, bool>> filter);
        void InsertOrUpdate(ChapterContactInfoRel chaptercontactinforel);
        void Delete(int id);
        void Save();
    }
}