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
    public class PersonMediaImageRelRepository : IPersonMediaImageRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<PersonMediaImageRel> All
        {
            get { return context.PersonMediaImageRels; }
        }

        public IQueryable<PersonMediaImageRel> AllIncluding(params Expression<Func<PersonMediaImageRel, object>>[] includeProperties)
        {
            IQueryable<PersonMediaImageRel> query = context.PersonMediaImageRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PersonMediaImageRel Find(long id)
        {
            return context.PersonMediaImageRels.Find(id);
        }

        public IQueryable<PersonMediaImageRel> Get(Expression<Func<PersonMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonMediaImageRels.Where(filter);
            }
            return context.PersonMediaImageRels;
        }        

        public void InsertOrUpdate(PersonMediaImageRel personmediaimagerel)
        {
            if (personmediaimagerel.Id == default(long)) {
                // New entity
                context.PersonMediaImageRels.Add(personmediaimagerel);
            } else {
                // Existing entity
                context.Entry(personmediaimagerel).State = EntityState.Modified;
            }
        }

        public void Delete(long id)
        {
            var personmediaimagerel = context.PersonMediaImageRels.Find(id);
            context.PersonMediaImageRels.Remove(personmediaimagerel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
        public IQueryable<PersonMediaImageRel> GetPersonMediaImages(Expression<Func<PersonMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.PersonMediaImageRels.Where(filter);
            }
            return context.PersonMediaImageRels;
        }
    }

    public interface IPersonMediaImageRelRepository : IDisposable
    {
        IQueryable<PersonMediaImageRel> All { get; }
        IQueryable<PersonMediaImageRel> AllIncluding(params Expression<Func<PersonMediaImageRel, object>>[] includeProperties);
        PersonMediaImageRel Find(long id);
        IQueryable<PersonMediaImageRel> Get(Expression<Func<PersonMediaImageRel, bool>> filter);
        IQueryable<PersonMediaImageRel> GetPersonMediaImages(Expression<Func<PersonMediaImageRel, bool>> filter);
        void InsertOrUpdate(PersonMediaImageRel personmediaimagerel);
        void Delete(long id);
        void Save();
    }
}