using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ACDBContext context;

        public AddressRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<Address> All
        {
            get { return context.Addresses; }
        }

        public IQueryable<Address> AllIncluding(params Expression<Func<Address, object>>[] includeProperties)
        {
            IQueryable<Address> query = context.Addresses;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Address Find(int id)
        {
            return context.Addresses.Find(id);
        }

        public void InsertOrUpdate(Address address)
        {
            if (address.Id == default(int))
            {
                // New entity
                context.Addresses.Add(address);
            }
            else
            {
                // Existing entity
                context.Entry(address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var address = context.Addresses.Find(id);
            context.Addresses.Remove(address);
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

    public interface IAddressRepository : IDisposable
    {
        IQueryable<Address> All { get; }
        IQueryable<Address> AllIncluding(params Expression<Func<Address, object>>[] includeProperties);
        Address Find(int id);
        void InsertOrUpdate(Address address);
        void Delete(int id);
        void Save();
    }
}