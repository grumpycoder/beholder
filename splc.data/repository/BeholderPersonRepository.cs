using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class BeholderPersonRepository : IBeholderPersonRepository
    {

        private readonly ACDBContext context;

        public BeholderPersonRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<DropdownPerson> GetDropdown(Expression<Func<DropdownPerson, bool>> filter)
        {
            if (filter != null)
            {
                return context.DropdownPersons.Where(filter);
            }
            return context.DropdownPersons;
        }

        public IQueryable<BeholderPerson> All
        {
            get { return context.BeholderPersons; }
        }

        public IQueryable<BeholderPerson> AllIncluding(params Expression<Func<BeholderPerson, object>>[] includeProperties)
        {
            IQueryable<BeholderPerson> query = context.BeholderPersons;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<BeholderPerson> Get(User user, Expression<Func<BeholderPerson, bool>> filter)
        {
            if (filter != null)
            {
                return context.BeholderPersons.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return context.BeholderPersons;
        }

        public BeholderPerson Get(User user, int id)
        {
            var person = context.BeholderPersons.Find(id);
            if (person.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && person.DateDeleted == null && person.RemovalStatusId == null)
            {
                return person;
            }
            return null;
        }

        public void InsertOrUpdate(BeholderPerson beholderperson)
        {
            if (beholderperson.Id == default(int))
            {
                // New entity
                context.CommonPersons.Add(beholderperson.CommonPerson);
                context.BeholderPersons.Add(beholderperson);
            }
            else
            {
                // Existing entity
                context.Entry(beholderperson).State = EntityState.Modified;
                context.Entry(beholderperson.CommonPerson).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var beholderperson = context.BeholderPersons.Find(id);
            context.BeholderPersons.Remove(beholderperson);
        }

        public void Save()
        {
            context.SaveChanges();
        }



        public BeholderPerson Find(User user, int id)
        {
            var person = context.BeholderPersons.Find(id);
            if (person.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && person.DateDeleted == null && person.RemovalStatusId == null)
            {
                return person;
            }
            return null;
        }

        public BeholderPerson Find(int id)
        {
            var person = context.BeholderPersons.Find(id);
            if (person.DateDeleted == null)
            {
                return person;
            }
            return null;
        }
        
        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IBeholderPersonRepository : IDisposable
    {

        IQueryable<BeholderPerson> All { get; }
        IQueryable<BeholderPerson> AllIncluding(params Expression<Func<BeholderPerson, object>>[] includeProperties);
        IQueryable<DropdownPerson> GetDropdown(Expression<Func<DropdownPerson, bool>> filter);

        BeholderPerson Get(User user, int id);
        IQueryable<BeholderPerson> Get(User user, Expression<Func<BeholderPerson, bool>> filter);

        void InsertOrUpdate(BeholderPerson beholderperson);
        void Delete(int id);
        void Save();

        BeholderPerson Find(User user, int id);
        BeholderPerson Find(int id);

    }
}