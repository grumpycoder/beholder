using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AddressChapterRelRepository : IAddressChapterRelRepository
    {

        private readonly ACDBContext context;

        public AddressChapterRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AddressChapterRel> All
        {
            get { return context.AddressChapterRels.Where(x => x.DateDeleted == null); }
        }

        public IQueryable<AddressChapterRel> AllIncluding(params Expression<Func<AddressChapterRel, object>>[] includeProperties)
        {
            IQueryable<AddressChapterRel> query = context.AddressChapterRels.Where(x => x.DateDeleted == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressChapterRel Find(int id)
        {
            return context.AddressChapterRels.Find(id);
        }


        public IQueryable<AddressChapterRel> GetAddresses(Expression<Func<AddressChapterRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AddressChapterRels.Where(filter).Where(x => x.DateDeleted == null);
            }
            return context.AddressChapterRels;
        }

        public void InsertOrUpdate(AddressChapterRel addresschapterrel)
        {
            if (addresschapterrel.Id == default(int))
            {
                // New entity
                context.Addresses.Add(addresschapterrel.Address);
                context.AddressChapterRels.Add(addresschapterrel);
            }
            else
            {
                // Existing entity
                context.Entry(addresschapterrel).State = EntityState.Modified;
                context.Entry(addresschapterrel.Address).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresschapterrel = context.AddressChapterRels.Find(id);
            var address = context.Addresses.Find(addresschapterrel.AddressId);
            context.Addresses.Remove(address);
            context.AddressChapterRels.Remove(addresschapterrel);
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

    public interface IAddressChapterRelRepository : IDisposable
    {
        IQueryable<AddressChapterRel> All { get; }
        IQueryable<AddressChapterRel> AllIncluding(params Expression<Func<AddressChapterRel, object>>[] includeProperties);
        AddressChapterRel Find(int id);
        IQueryable<AddressChapterRel> GetAddresses(Expression<Func<AddressChapterRel, bool>> filter);
        void InsertOrUpdate(AddressChapterRel addresschapterrel);
        void Delete(int id);
        void Save();
    }
}