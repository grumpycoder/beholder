using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class OrganizationOrganizationRelRepository : IOrganizationOrganizationRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<OrganizationOrganizationRel> All
        {
            get { return context.OrganizationOrganizationRels; }
        }

        public IQueryable<OrganizationOrganizationRel> AllIncluding(params Expression<Func<OrganizationOrganizationRel, object>>[] includeProperties)
        {
            IQueryable<OrganizationOrganizationRel> query = context.OrganizationOrganizationRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationOrganizationRel Find(int id)
        {
            return context.OrganizationOrganizationRels.Find(id);
        }

        public void InsertOrUpdate(OrganizationOrganizationRel OrganizationOrganizationrel)
        {
            if (OrganizationOrganizationrel.Id == default(int)) {
                // New entity
                context.OrganizationOrganizationRels.Add(OrganizationOrganizationrel);
            } else {
                // Existing entity
                context.Entry(OrganizationOrganizationrel).State = EntityState.Modified;
            }
        }

        public IQueryable<OrganizationOrganizationRel> GetOrganizationOrganizations(Expression<Func<OrganizationOrganizationRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationOrganizationRels.Where(filter);
            }
            return context.OrganizationOrganizationRels;
        }

        public void Delete(int id)
        {
            var OrganizationOrganizationrel = context.OrganizationOrganizationRels.Find(id);
            context.OrganizationOrganizationRels.Remove(OrganizationOrganizationrel);
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

    public interface IOrganizationOrganizationRelRepository : IDisposable
    {
        IQueryable<OrganizationOrganizationRel> All { get; }
        IQueryable<OrganizationOrganizationRel> AllIncluding(params Expression<Func<OrganizationOrganizationRel, object>>[] includeProperties);
        OrganizationOrganizationRel Find(int id);
        IQueryable<OrganizationOrganizationRel> GetOrganizationOrganizations(Expression<Func<OrganizationOrganizationRel, bool>> filter);
        void InsertOrUpdate(OrganizationOrganizationRel OrganizationOrganizationrel);
        void Delete(int id);
        void Save();
    }
}