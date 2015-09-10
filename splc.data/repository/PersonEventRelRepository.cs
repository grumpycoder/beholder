using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class PersonEventRelRepository : IPersonEventRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<PersonEventRel> All
        {
            get { return context.PersonEventRels; }
        }

        public IQueryable<PersonEventRel> GetPersonEvents(Expression<Func<PersonEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonEventRels.Where(filter);
            }
            return context.PersonEventRels;
        }

        public IQueryable<PersonEventRel> AllIncluding(params Expression<Func<PersonEventRel, object>>[] includeProperties)
        {
            IQueryable<PersonEventRel> query = context.PersonEventRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PersonEventRel Find(int id)
        {
            return context.PersonEventRels.Find(id);
        }

        public void InsertOrUpdate(PersonEventRel personeventrel)
        {
            if (personeventrel.Id == default(int))
            {
                // New entity
                context.PersonEventRels.Add(personeventrel);
            }
            else
            {
                // Existing entity
                context.Entry(personeventrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var personeventrel = context.PersonEventRels.Find(id);
            context.PersonEventRels.Remove(personeventrel);
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

    public interface IPersonEventRelRepository : IDisposable
    {
        IQueryable<PersonEventRel> All { get; }
        IQueryable<PersonEventRel> AllIncluding(params Expression<Func<PersonEventRel, object>>[] includeProperties);
        PersonEventRel Find(int id);
        IQueryable<PersonEventRel> GetPersonEvents(Expression<Func<PersonEventRel, bool>> filter);
        void InsertOrUpdate(PersonEventRel personeventrel);
        void Delete(int id);
        void Save();
    }
}