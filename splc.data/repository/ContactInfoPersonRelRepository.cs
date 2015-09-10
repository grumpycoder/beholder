using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class ContactInfoPersonRelRepository : IContactInfoPersonRelRepository
    {
        private readonly ACDBContext context;

        public ContactInfoPersonRelRepository(ACDBContext Context)
        {
            context = Context; 
        }

        public IQueryable<ContactInfoPersonRel> All
        {
            get { return context.ContactInfoPersonRels.Where(x => x.DateDeleted == null); }
        }

        public IQueryable<ContactInfoPersonRel> AllIncluding(params Expression<Func<ContactInfoPersonRel, object>>[] includeProperties)
        {
            IQueryable<ContactInfoPersonRel> query = context.ContactInfoPersonRels.Where(x => x.DateDeleted == null);
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ContactInfoPersonRel Find(int id)
        {
            var ent = context.ContactInfoPersonRels.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public IQueryable<ContactInfoPersonRel> GetContactInfo(Expression<Func<ContactInfoPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactInfoPersonRels.Where(filter).Where(x => x.DateDeleted == null);
            }
            return context.ContactInfoPersonRels.Where(x => x.DateDeleted == null);
        }

        public void InsertOrUpdate(ContactInfoPersonRel contactinfopersonrel)
        {
            if (contactinfopersonrel.Id == default(int)) {
                // New entity
                context.ContactInfo.Add(contactinfopersonrel.ContactInfo);
                context.ContactInfoPersonRels.Add(contactinfopersonrel);
            } else {
                // Existing entity
                context.Entry(contactinfopersonrel).State = EntityState.Modified;
                context.Entry(contactinfopersonrel.ContactInfo).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contactinfopersonrel = context.ContactInfoPersonRels.Find(id);
            context.ContactInfoPersonRels.Remove(contactinfopersonrel);
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

    public interface IContactInfoPersonRelRepository : IDisposable
    {
        IQueryable<ContactInfoPersonRel> All { get; }
        IQueryable<ContactInfoPersonRel> AllIncluding(params Expression<Func<ContactInfoPersonRel, object>>[] includeProperties);
        ContactInfoPersonRel Find(int id);
        IQueryable<ContactInfoPersonRel> GetContactInfo(Expression<Func<ContactInfoPersonRel, bool>> filter);
        void InsertOrUpdate(ContactInfoPersonRel contactinfopersonrel);
        void Delete(int id);
        void Save();
    }
}