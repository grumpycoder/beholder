using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class EventEventRelRepository : IEventEventRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<EventEventRel> All
        {
            get { return context.EventEventRels; }
        }

        public IQueryable<EventEventRel> AllIncluding(params Expression<Func<EventEventRel, object>>[] includeProperties)
        {
            IQueryable<EventEventRel> query = context.EventEventRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public EventEventRel Find(int id)
        {
            return context.EventEventRels.Find(id);
        }

        public IQueryable<EventEventRel> GetEventEvents(Expression<Func<EventEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventEventRels.Where(filter);
            }
            return context.EventEventRels;
        }

        public void InsertOrUpdate(EventEventRel eventeventrel)
        {
            if (eventeventrel.Id == default(long))
            {
                // New entity
                context.EventEventRels.Add(eventeventrel);
            }
            else
            {
                // Existing entity
                context.Entry(eventeventrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventeventrel = context.EventEventRels.Find(id);
            context.EventEventRels.Remove(eventeventrel);
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

    public interface IEventEventRelRepository : IDisposable
    {
        IQueryable<EventEventRel> All { get; }
        IQueryable<EventEventRel> AllIncluding(params Expression<Func<EventEventRel, object>>[] includeProperties);
        EventEventRel Find(int id);
        IQueryable<EventEventRel> GetEventEvents(Expression<Func<EventEventRel, bool>> filter);
        void InsertOrUpdate(EventEventRel eventeventrel);
        void Delete(int id);
        void Save();
    }
}