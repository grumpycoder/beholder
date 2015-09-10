using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class PrimaryStatusRepository : IPrimaryStatusRepository
    {
        readonly ACDBContext _context = new ACDBContext();

        public IQueryable<PrimaryStatus> All
        {
            get { return _context.PrimaryStatus; }
        }

        public IQueryable<PrimaryStatus> AllIncluding(params Expression<Func<PrimaryStatus, object>>[] includeProperties)
        {
            IQueryable<PrimaryStatus> query = _context.PrimaryStatus;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PrimaryStatus Find(int id)
        {
            return _context.PrimaryStatus.Find(id);
        }

        public void InsertOrUpdate(PrimaryStatus primarystatus)
        {
            if (primarystatus.Id == default(int)) {
                // New entity
                _context.PrimaryStatus.Add(primarystatus);
            } else {
                // Existing entity
                _context.Entry(primarystatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var primarystatus = _context.PrimaryStatus.Find(id);
            _context.PrimaryStatus.Remove(primarystatus);
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

    public interface IPrimaryStatusRepository : IDisposable
    {
        IQueryable<PrimaryStatus> All { get; }
        IQueryable<PrimaryStatus> AllIncluding(params Expression<Func<PrimaryStatus, object>>[] includeProperties);
        PrimaryStatus Find(int id);
        void InsertOrUpdate(PrimaryStatus primarystatus);
        void Delete(int id);
        void Save();
    }
}