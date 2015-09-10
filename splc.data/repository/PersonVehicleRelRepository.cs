using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{ 
    public class PersonVehicleRelRepository : IPersonVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<PersonVehicleRel> All
        {
            get { return context.PersonVehicleRels; }
        }

        public IQueryable<PersonVehicleRel> GetPersonVehicles(Expression<Func<PersonVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonVehicleRels.Where(filter);
            }
            return context.PersonVehicleRels;
        }


        public IQueryable<PersonVehicleRel> AllIncluding(params Expression<Func<PersonVehicleRel, object>>[] includeProperties)
        {
            IQueryable<PersonVehicleRel> query = context.PersonVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PersonVehicleRel Find(int id)
        {
            return context.PersonVehicleRels.Find(id);
        }

        public IQueryable<PersonVehicleRel> Get(Expression<Func<PersonVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonVehicleRels.Where(filter);
            }
            return context.PersonVehicleRels;
        }

        public void InsertOrUpdate(PersonVehicleRel personvehiclerel)
        {
            if (personvehiclerel.Id == default(int)) {
                // New entity
                context.PersonVehicleRels.Add(personvehiclerel);
            } else {
                // Existing entity
                context.Entry(personvehiclerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var personvehiclerel = context.PersonVehicleRels.Find(id);
            context.PersonVehicleRels.Remove(personvehiclerel);
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

    public interface IPersonVehicleRelRepository : IDisposable
    {
        IQueryable<PersonVehicleRel> All { get; }
        IQueryable<PersonVehicleRel> AllIncluding(params Expression<Func<PersonVehicleRel, object>>[] includeProperties);
        PersonVehicleRel Find(int id);
        IQueryable<PersonVehicleRel> Get(Expression<Func<PersonVehicleRel, bool>> filter);
        IQueryable<PersonVehicleRel> GetPersonVehicles(Expression<Func<PersonVehicleRel, bool>> filter);
        void InsertOrUpdate(PersonVehicleRel personvehiclerel);
        void Delete(int id);
        void Save();
    }
}