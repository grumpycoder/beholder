using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{ 
    public class EventVehicleRelRepository : IEventVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<EventVehicleRel> All
        {
            get { return context.EventVehicleRels; }
        }

        public IQueryable<EventVehicleRel> GetEventVehicles(Expression<Func<EventVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventVehicleRels.Where(filter);
            }
            return context.EventVehicleRels;
        }


        public IQueryable<EventVehicleRel> AllIncluding(params Expression<Func<EventVehicleRel, object>>[] includeProperties)
        {
            IQueryable<EventVehicleRel> query = context.EventVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public EventVehicleRel Find(int id)
        {
            return context.EventVehicleRels.Find(id);
        }

        public IQueryable<EventVehicleRel> Get(Expression<Func<EventVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventVehicleRels.Where(filter);
            }
            return context.EventVehicleRels;
        }

        public void InsertOrUpdate(EventVehicleRel eventvehiclerel)
        {
            if (eventvehiclerel.Id == default(int)) {
                // New entity
                context.EventVehicleRels.Add(eventvehiclerel);
            } else {
                // Existing entity
                context.Entry(eventvehiclerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventvehiclerel = context.EventVehicleRels.Find(id);
            context.EventVehicleRels.Remove(eventvehiclerel);
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

    public interface IEventVehicleRelRepository : IDisposable
    {
        IQueryable<EventVehicleRel> All { get; }
        IQueryable<EventVehicleRel> AllIncluding(params Expression<Func<EventVehicleRel, object>>[] includeProperties);
        EventVehicleRel Find(int id);
        IQueryable<EventVehicleRel> Get(Expression<Func<EventVehicleRel, bool>> filter);
        IQueryable<EventVehicleRel> GetEventVehicles(Expression<Func<EventVehicleRel, bool>> filter);
        void InsertOrUpdate(EventVehicleRel eventvehiclerel);
        void Delete(int id);
        void Save();
    }
}