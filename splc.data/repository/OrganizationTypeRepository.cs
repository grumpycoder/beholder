using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class OrganizationTypeRepository : IOrganizationTypeRepository
    {
        readonly ACDBContext _context = new ACDBContext();

        public IQueryable<OrganizationType> All
        {
            get { return _context.OrganizationTypes; }
        }

        public IQueryable<OrganizationType> AllIncluding(params Expression<Func<OrganizationType, object>>[] includeProperties)
        {
            IQueryable<OrganizationType> query = _context.OrganizationTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationType Find(int id)
        {
            return _context.OrganizationTypes.Find(id);
        }

        public void InsertOrUpdate(OrganizationType organizationtype)
        {
            if (organizationtype.Id == default(int)) {
                // New entity
                _context.OrganizationTypes.Add(organizationtype);
            } else {
                // Existing entity
                _context.Entry(organizationtype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationtype = _context.OrganizationTypes.Find(id);
            _context.OrganizationTypes.Remove(organizationtype);
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

    public interface IOrganizationTypeRepository : IDisposable
    {
        IQueryable<OrganizationType> All { get; }
        IQueryable<OrganizationType> AllIncluding(params Expression<Func<OrganizationType, object>>[] includeProperties);
        OrganizationType Find(int id);
        void InsertOrUpdate(OrganizationType organizationtype);
        void Delete(int id);
        void Save();
    }
}