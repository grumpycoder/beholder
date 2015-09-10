using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class EventMediaImageRelRepository : IEventMediaImageRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<EventMediaImageRel> All
        {
            get { return context.EventMediaImageRels; }
        }

        public IQueryable<EventMediaImageRel> AllIncluding(params Expression<Func<EventMediaImageRel, object>>[] includeProperties)
        {
            IQueryable<EventMediaImageRel> query = context.EventMediaImageRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public EventMediaImageRel Find(int id)
        {
            return context.EventMediaImageRels.Find(id);
        }

        public IQueryable<EventMediaImageRel> GetEventMediaImages(Expression<Func<EventMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventMediaImageRels.Where(filter);
            }
            return context.EventMediaImageRels;
        }

        public void InsertOrUpdate(EventMediaImageRel eventmediaimagerel)
        {
            if (eventmediaimagerel.Id == default(int))
            {
                // New entity
                context.EventMediaImageRels.Add(eventmediaimagerel);
            }
            else
            {
                // Existing entity
                context.Entry(eventmediaimagerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventmediaimagerel = context.EventMediaImageRels.Find(id);
            context.EventMediaImageRels.Remove(eventmediaimagerel);
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

    public interface IEventMediaImageRelRepository : IDisposable
    {
        IQueryable<EventMediaImageRel> All { get; }
        IQueryable<EventMediaImageRel> AllIncluding(params Expression<Func<EventMediaImageRel, object>>[] includeProperties);
        EventMediaImageRel Find(int id);
        IQueryable<EventMediaImageRel> GetEventMediaImages(Expression<Func<EventMediaImageRel, bool>> filter);
        void InsertOrUpdate(EventMediaImageRel eventmediaimagesrel);
        void Delete(int id);
        void Save();
    }
}