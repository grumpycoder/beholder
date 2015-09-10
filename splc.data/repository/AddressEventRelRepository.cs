using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressEventRelRepository : IAddressEventRelRepository
    {

        private readonly ACDBContext context;

        public AddressEventRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AddressEventRel> All
        {
            get { return context.AddressEventRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Event.DateDeleted == null && x.Event.RemovalStatusId == null); }
        }

        public IQueryable<AddressEventRel> AllIncluding(params Expression<Func<AddressEventRel, object>>[] includeProperties)
        {
            IQueryable<AddressEventRel> query = context.AddressEventRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        //
        public AddressEventRel Find(int id)
        {
            var rel = context.AddressEventRels.Find(id);
            if (rel.Address.DateDeleted == null && rel.Event.DateDeleted == null && rel.DateDeleted == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }


        public IQueryable<AddressEventRel> GetAddresses(Expression<Func<AddressEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AddressEventRels.Where(filter).Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
            }
            return context.AddressEventRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Event.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdate(AddressEventRel addresseventrel)
        {
            if (addresseventrel.Id == default(int))
            {
                // New entity
                context.Addresses.Add(addresseventrel.Address);
                context.AddressEventRels.Add(addresseventrel);
            }
            else
            {
                // Existing entity
                context.Entry(addresseventrel).State = EntityState.Modified;
                context.Entry(addresseventrel.Address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresseventrel = context.AddressEventRels.Find(id);
            var address = context.Addresses.Find(addresseventrel.AddressId);
            context.Addresses.Remove(address);
            context.AddressEventRels.Remove(addresseventrel);
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

    public interface IAddressEventRelRepository : IDisposable
    {
        IQueryable<AddressEventRel> All { get; }
        IQueryable<AddressEventRel> AllIncluding(params Expression<Func<AddressEventRel, object>>[] includeProperties);
        AddressEventRel Find(int id);
        IQueryable<AddressEventRel> GetAddresses(Expression<Func<AddressEventRel, bool>> filter);
        void InsertOrUpdate(AddressEventRel addresseventrel);
        void Delete(int id);
        void Save();
    }
}