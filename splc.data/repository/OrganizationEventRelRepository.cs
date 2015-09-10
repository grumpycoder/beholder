using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class OrganizationEventRelRepository : IOrganizationEventRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<OrganizationEventRel> All
        {
            get { return context.OrganizationEventRels; }
        }

        public IQueryable<OrganizationEventRel> AllIncluding(params Expression<Func<OrganizationEventRel, object>>[] includeProperties)
        {
            IQueryable<OrganizationEventRel> query = context.OrganizationEventRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationEventRel Find(int id)
        {
            return context.OrganizationEventRels.Find(id);
        }

        public IQueryable<OrganizationEventRel> GetOrganizationEvents(Expression<Func<OrganizationEventRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationEventRels.Where(filter);
            }
            return context.OrganizationEventRels;
        }

        public void InsertOrUpdate(OrganizationEventRel organizationeventrel)
        {
            if (organizationeventrel.Id == default(int))
            {
                // New entity
                context.OrganizationEventRels.Add(organizationeventrel);
            }
            else
            {
                // Existing entity
                context.Entry(organizationeventrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationeventrel = context.OrganizationEventRels.Find(id);
            context.OrganizationEventRels.Remove(organizationeventrel);
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

    public interface IOrganizationEventRelRepository : IDisposable
    {
        IQueryable<OrganizationEventRel> All { get; }
        IQueryable<OrganizationEventRel> AllIncluding(params Expression<Func<OrganizationEventRel, object>>[] includeProperties);
        OrganizationEventRel Find(int id);
        IQueryable<OrganizationEventRel> GetOrganizationEvents(Expression<Func<OrganizationEventRel, bool>> filter);
        void InsertOrUpdate(OrganizationEventRel organizationeventrel);
        void Delete(int id);
        void Save();
    }
}