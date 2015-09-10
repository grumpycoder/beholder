using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{ 
    public class ChapterVehicleRelRepository : IChapterVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ChapterVehicleRel> All
        {
            get { return context.ChapterVehicleRels; }
        }

        public IQueryable<ChapterVehicleRel> GetChapterVehicles(Expression<Func<ChapterVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterVehicleRels.Where(filter);
            }
            return context.ChapterVehicleRels;
        }


        public IQueryable<ChapterVehicleRel> AllIncluding(params Expression<Func<ChapterVehicleRel, object>>[] includeProperties)
        {
            IQueryable<ChapterVehicleRel> query = context.ChapterVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChapterVehicleRel Find(int id)
        {
            return context.ChapterVehicleRels.Find(id);
        }

        public IQueryable<ChapterVehicleRel> Get(Expression<Func<ChapterVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ChapterVehicleRels.Where(filter);
            }
            return context.ChapterVehicleRels;
        }

        public void InsertOrUpdate(ChapterVehicleRel chaptervehiclerel)
        {
            if (chaptervehiclerel.Id == default(int)) {
                // New entity
                context.ChapterVehicleRels.Add(chaptervehiclerel);
            } else {
                // Existing entity
                context.Entry(chaptervehiclerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var chaptervehiclerel = context.ChapterVehicleRels.Find(id);
            context.ChapterVehicleRels.Remove(chaptervehiclerel);
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

    public interface IChapterVehicleRelRepository : IDisposable
    {
        IQueryable<ChapterVehicleRel> All { get; }
        IQueryable<ChapterVehicleRel> AllIncluding(params Expression<Func<ChapterVehicleRel, object>>[] includeProperties);
        ChapterVehicleRel Find(int id);
        IQueryable<ChapterVehicleRel> Get(Expression<Func<ChapterVehicleRel, bool>> filter);
        IQueryable<ChapterVehicleRel> GetChapterVehicles(Expression<Func<ChapterVehicleRel, bool>> filter);
        void InsertOrUpdate(ChapterVehicleRel chaptervehiclerel);
        void Delete(int id);
        void Save();
    }
}