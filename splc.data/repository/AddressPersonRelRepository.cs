using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressPersonRelRepository : IAddressPersonRelRepository
    {

        private readonly ACDBContext context;

        public AddressPersonRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AddressPersonRel> All
        {
            get { return context.AddressPersonRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.CommonPerson.DateDeleted == null); }
        }

        public IQueryable<AddressPersonRel> AllIncluding(params Expression<Func<AddressPersonRel, object>>[] includeProperties)
        {
            IQueryable<AddressPersonRel> query = context.AddressPersonRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.CommonPerson.DateDeleted == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressPersonRel Find(int id)
        {
            var rel = context.AddressPersonRels.Find(id);
            if (rel.Address.DateDeleted == null && rel.CommonPerson.DateDeleted == null && rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }


        public IQueryable<AddressPersonRel> GetAddresses(Expression<Func<AddressPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AddressPersonRels.Where(filter).Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.CommonPerson.DateDeleted == null);
            }
            return context.AddressPersonRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.CommonPerson.DateDeleted == null);
        }

        public void InsertOrUpdate(AddressPersonRel addresspersonrel)
        {
            if (addresspersonrel.Id == default(int))
            {
                // New entity
                context.Addresses.Add(addresspersonrel.Address);
                context.AddressPersonRels.Add(addresspersonrel);
            }
            else
            {
                // Existing entity
                context.Entry(addresspersonrel).State = EntityState.Modified;
                context.Entry(addresspersonrel.Address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresspersonrel = context.AddressPersonRels.Find(id);
            var address = context.Addresses.Find(addresspersonrel.AddressId);
            context.Addresses.Remove(address);
            context.AddressPersonRels.Remove(addresspersonrel);
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

    public interface IAddressPersonRelRepository : IDisposable
    {
        IQueryable<AddressPersonRel> All { get; }
        IQueryable<AddressPersonRel> AllIncluding(params Expression<Func<AddressPersonRel, object>>[] includeProperties);
        AddressPersonRel Find(int id);
        IQueryable<AddressPersonRel> GetAddresses(Expression<Func<AddressPersonRel, bool>> filter);
        void InsertOrUpdate(AddressPersonRel addresspersonrel);
        void Delete(int id);
        void Save();
    }
}