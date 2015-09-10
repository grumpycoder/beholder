using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class ContactInfoTypeRepository : IContactInfoTypeRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<ContactInfoType> All
        {
            get { return context.ContactInfoTypes; }
        }

        public IQueryable<ContactInfoType> AllIncluding(params Expression<Func<ContactInfoType, object>>[] includeProperties)
        {
            IQueryable<ContactInfoType> query = context.ContactInfoTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ContactInfoType Find(int id)
        {
            return context.ContactInfoTypes.Find(id);
        }

        public void InsertOrUpdate(ContactInfoType contactinfotype)
        {
            if (contactinfotype.Id == default(int)) {
                // New entity
                context.ContactInfoTypes.Add(contactinfotype);
            } else {
                // Existing entity
                context.Entry(contactinfotype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contactinfotype = context.ContactInfoTypes.Find(id);
            context.ContactInfoTypes.Remove(contactinfotype);
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

    public interface IContactInfoTypeRepository : IDisposable
    {
        IQueryable<ContactInfoType> All { get; }
        IQueryable<ContactInfoType> AllIncluding(params Expression<Func<ContactInfoType, object>>[] includeProperties);
        ContactInfoType Find(int id);
        void InsertOrUpdate(ContactInfoType contactinfotype);
        void Delete(int id);
        void Save();
    }
}