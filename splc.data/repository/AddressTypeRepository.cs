using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class AddressTypeRepository : IAddressTypeRepository
    {
        readonly ACDBContext _context = new ACDBContext();

        public IQueryable<AddressType> All
        {
            get { return _context.AddressTypes; }
        }

        public IQueryable<AddressType> AllIncluding(params Expression<Func<AddressType, object>>[] includeProperties)
        {
            IQueryable<AddressType> query = _context.AddressTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressType Find(int id)
        {
            return _context.AddressTypes.Find(id);
        }

        public void InsertOrUpdate(AddressType addresstype)
        {
            if (addresstype.Id == default(int)) {
                // New entity
                _context.AddressTypes.Add(addresstype);
            } else {
                // Existing entity
                _context.Entry(addresstype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addresstype = _context.AddressTypes.Find(id);
            _context.AddressTypes.Remove(addresstype);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }

    public interface IAddressTypeRepository : IDisposable
    {
        IQueryable<AddressType> All { get; }
        IQueryable<AddressType> AllIncluding(params Expression<Func<AddressType, object>>[] includeProperties);
        AddressType Find(int id);
        void InsertOrUpdate(AddressType addresstype);
        void Delete(int id);
        void Save();
    }
}