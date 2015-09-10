using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class OrganizationPersonRelRepository : IOrganizationPersonRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<OrganizationPersonRel> All
        {
            get { return context.OrganizationPersonRels; }
        }

        public IQueryable<OrganizationPersonRel> AllIncluding(params Expression<Func<OrganizationPersonRel, object>>[] includeProperties)
        {
            IQueryable<OrganizationPersonRel> query = context.OrganizationPersonRels;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<OrganizationPersonRel> GetOrganizationPersons(Expression<Func<OrganizationPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationPersonRels.Where(filter);
            }
            return context.OrganizationPersonRels;
        }

        public OrganizationPersonRel Find(int id)
        {
            return context.OrganizationPersonRels.Find(id);
        }

        public void InsertOrUpdate(OrganizationPersonRel organizationpersonrel)
        {
            if (organizationpersonrel.Id == default(int))
            {
                // New entity
                context.OrganizationPersonRels.Add(organizationpersonrel);
            }
            else
            {
                // Existing entity
                context.Entry(organizationpersonrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationpersonrel = context.OrganizationPersonRels.Find(id);
            context.OrganizationPersonRels.Remove(organizationpersonrel);
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

    public interface IOrganizationPersonRelRepository : IDisposable
    {
        IQueryable<OrganizationPersonRel> All { get; }
        IQueryable<OrganizationPersonRel> AllIncluding(params Expression<Func<OrganizationPersonRel, object>>[] includeProperties);
        OrganizationPersonRel Find(int id);
        IQueryable<OrganizationPersonRel> GetOrganizationPersons(Expression<Func<OrganizationPersonRel, bool>> filter);
        void InsertOrUpdate(OrganizationPersonRel organizationpersonrel);
        void Delete(int id);
        void Save();
    }
}