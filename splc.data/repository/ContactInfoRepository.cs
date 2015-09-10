using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class ContactInfoRepository : IContactInfoRepository
    {
        private ACDBContext context; 

        public ContactInfoRepository(ACDBContext Context)
        {
            context = Context; 
        }

        public IQueryable<ContactInfo> All
        {
            get { return context.ContactInfo.Where(x => x.DateDeleted == null); }
        }

        public IQueryable<ContactInfo> AllIncluding(params Expression<Func<ContactInfo, object>>[] includeProperties)
        {
            IQueryable<ContactInfo> query = context.ContactInfo.Where(x => x.DateDeleted == null);
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ContactInfo Find(int id)
        {
            var ent = context.ContactInfo.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            return null;
        }

        public void InsertOrUpdate(ContactInfo contactinfo)
        {
            if (contactinfo.Id == default(int)) {
                // New entity
                context.ContactInfo.Add(contactinfo);
            } else {
                // Existing entity
                context.Entry(contactinfo).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contactinfo = context.ContactInfo.Find(id);
            context.ContactInfo.Remove(contactinfo);
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

    public interface IContactInfoRepository : IDisposable
    {
        IQueryable<ContactInfo> All { get; }
        IQueryable<ContactInfo> AllIncluding(params Expression<Func<ContactInfo, object>>[] includeProperties);
        ContactInfo Find(int id);
        void InsertOrUpdate(ContactInfo contactinfo);
        void Delete(int id);
        void Save();
    }
}