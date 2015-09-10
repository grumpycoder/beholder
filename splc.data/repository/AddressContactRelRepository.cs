using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressContactRelRepository : IAddressContactRelRepository
    {

        private readonly ACDBContext context;

        public AddressContactRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AddressContactRel> All
        {
            get { return context.AddressContactRels.Where(x => x.DateDeleted == null); }
        }

        public IQueryable<AddressContactRel> AllIncluding(params Expression<Func<AddressContactRel, object>>[] includeProperties)
        {
            IQueryable<AddressContactRel> query = context.AddressContactRels.Where(x => x.DateDeleted == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressContactRel Find(int id)
        {
            return context.AddressContactRels.Find(id);
        }


        public IQueryable<AddressContactRel> GetAddresses(Expression<Func<AddressContactRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AddressContactRels.Where(filter).Where(x => x.DateDeleted == null);
            }
            return context.AddressContactRels.Where(x => x.DateDeleted == null);
        }

        public void InsertOrUpdate(AddressContactRel addresscontactrel)
        {
            if (addresscontactrel.Id == default(int))
            {
                // New entity
                context.Addresses.Add(addresscontactrel.Address);
                context.AddressContactRels.Add(addresscontactrel);
            }
            else
            {
                // Existing entity
                context.Entry(addresscontactrel).State = EntityState.Modified;
                context.Entry(addresscontactrel.Address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresscontactrel = context.AddressContactRels.Find(id);
            var address = context.Addresses.Find(addresscontactrel.AddressId);
            context.Addresses.Remove(address);
            context.AddressContactRels.Remove(addresscontactrel);
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

    public interface IAddressContactRelRepository : IDisposable
    {
        IQueryable<AddressContactRel> All { get; }
        IQueryable<AddressContactRel> AllIncluding(params Expression<Func<AddressContactRel, object>>[] includeProperties);
        AddressContactRel Find(int id);
        IQueryable<AddressContactRel> GetAddresses(Expression<Func<AddressContactRel, bool>> filter);
        void InsertOrUpdate(AddressContactRel addresscontactrel);
        void Delete(int id);
        void Save();
    }
}