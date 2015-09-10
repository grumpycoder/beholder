using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressSubscriptionsRelRepository : IAddressSubscriptionsRelRepository
    {

        private readonly ACDBContext context;

        public AddressSubscriptionsRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AddressSubscriptionsRel> All
        {
            get { return context.AddressSubscriptionsRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null); }
        }

        public IQueryable<AddressSubscriptionsRel> AllIncluding(params Expression<Func<AddressSubscriptionsRel, object>>[] includeProperties)
        {
            IQueryable<AddressSubscriptionsRel> query = context.AddressSubscriptionsRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressSubscriptionsRel Find(int id)
        {
            var rel = context.AddressSubscriptionsRels.Find(id);
            if (rel.Address.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.DateDeleted == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<AddressSubscriptionsRel> GetAddresses(Expression<Func<AddressSubscriptionsRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AddressSubscriptionsRels.Where(filter).Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
            }
            return context.AddressSubscriptionsRels.Where(x => x.DateDeleted == null && x.Address.DateDeleted == null && x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdate(AddressSubscriptionsRel addresssubscriptionsrel)
        {
            if (addresssubscriptionsrel.Id == default(int))
            {
                // New entity
                context.Addresses.Add(addresssubscriptionsrel.Address);
                context.AddressSubscriptionsRels.Add(addresssubscriptionsrel);
            }
            else
            {
                // Existing entity
                context.Entry(addresssubscriptionsrel).State = EntityState.Modified;
                context.Entry(addresssubscriptionsrel.Address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresssubscriptionsrel = context.AddressSubscriptionsRels.Find(id);
            var address = context.Addresses.Find(addresssubscriptionsrel.AddressId);
            context.Addresses.Remove(address);
            context.AddressSubscriptionsRels.Remove(addresssubscriptionsrel);
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

    public interface IAddressSubscriptionsRelRepository : IDisposable
    {
        IQueryable<AddressSubscriptionsRel> All { get; }
        IQueryable<AddressSubscriptionsRel> AllIncluding(params Expression<Func<AddressSubscriptionsRel, object>>[] includeProperties);
        AddressSubscriptionsRel Find(int id);
        IQueryable<AddressSubscriptionsRel> GetAddresses(Expression<Func<AddressSubscriptionsRel, bool>> filter);
        void InsertOrUpdate(AddressSubscriptionsRel addresssubscriptionsrel);
        void Delete(int id);
        void Save();
    }
}