using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class LicenseTypeRepository : ILicenseTypeRepository
    {
        readonly ACDBContext _context = new ACDBContext();

        public IQueryable<LicenseType> All
        {
            get { return _context.LicenseTypes; }
        }

        public IQueryable<LicenseType> AllIncluding(params Expression<Func<LicenseType, object>>[] includeProperties)
        {
            IQueryable<LicenseType> query = _context.LicenseTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public LicenseType Find(int id)
        {
            return _context.LicenseTypes.Find(id);
        }

        public void InsertOrUpdate(LicenseType licensetype)
        {
            if (licensetype.Id == default(int)) {
                // New entity
                _context.LicenseTypes.Add(licensetype);
            } else {
                // Existing entity
                _context.Entry(licensetype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var licensetype = _context.LicenseTypes.Find(id);
            _context.LicenseTypes.Remove(licensetype);
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

    public interface ILicenseTypeRepository : IDisposable
    {
        IQueryable<LicenseType> All { get; }
        IQueryable<LicenseType> AllIncluding(params Expression<Func<LicenseType, object>>[] includeProperties);
        LicenseType Find(int id);
        void InsertOrUpdate(LicenseType licensetype);
        void Delete(int id);
        void Save();
    }
}