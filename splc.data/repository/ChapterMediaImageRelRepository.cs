using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using splc.domain.Models;
using splc.data;

namespace splc.data.repository
{ 
    public class ChapterMediaImageRelRepository : IChapterMediaImageRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterMediaImageRel> All
        {
            get { return context.ChapterMediaImageRels; }
        }

        public IQueryable<ChapterMediaImageRel> AllIncluding(params Expression<Func<ChapterMediaImageRel, object>>[] includeProperties)
        {
            IQueryable<ChapterMediaImageRel> query = context.ChapterMediaImageRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<ChapterMediaImageRel> Get(Expression<Func<ChapterMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterMediaImageRels.Where(filter);
            }
            return context.ChapterMediaImageRels;
        }

        public ChapterMediaImageRel Find(int id)
        {
            return context.ChapterMediaImageRels.Find(id);
        }

        public void InsertOrUpdate(ChapterMediaImageRel chaptermediaimagerel)
        {
            if (chaptermediaimagerel.Id == default(int))
            {
                // New entity
                //if (organizationmediaimagerel.Organization.Id == default(long))
                //{
                //    context.Organizations.Add(organizationmediaimagerel.Organization);
                //}
                //if (chaptermediaimagerel.MediaImage.Id == default(int))
                //{
                //    context.MediaImages.Add(chaptermediaimagerel.MediaImage);
                //}
                context.ChapterMediaImageRels.Add(chaptermediaimagerel);
            }
            else
            {
                // Existing entity
                //context.Entry(chaptermediaimagerel.MediaImage).State = EntityState.Modified;
                //context.Entry(chaptermediaimagerel.Chapter).State = EntityState.Modified;
                context.Entry(chaptermediaimagerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var chaptermediaimagerel = context.ChapterMediaImageRels.Find(id);
            context.ChapterMediaImageRels.Remove(chaptermediaimagerel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
        public IQueryable<ChapterMediaImageRel> GetChapterMediaImages(Expression<Func<ChapterMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterMediaImageRels.Where(filter);
            }
            return context.ChapterMediaImageRels;
        }

    }

    public interface IChapterMediaImageRelRepository : IDisposable
    {
        IQueryable<ChapterMediaImageRel> All { get; }
        IQueryable<ChapterMediaImageRel> AllIncluding(params Expression<Func<ChapterMediaImageRel, object>>[] includeProperties);
        ChapterMediaImageRel Find(int id);
        IQueryable<ChapterMediaImageRel> Get(Expression<Func<ChapterMediaImageRel, bool>> filter);
        void InsertOrUpdate(ChapterMediaImageRel chaptermediaimagerel);
        IQueryable<ChapterMediaImageRel> GetChapterMediaImages(Expression<Func<ChapterMediaImageRel, bool>> filter);
        void Delete(int id);
        void Save();
    }
}