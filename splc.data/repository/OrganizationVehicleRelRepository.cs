using splc.domain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{ 
    public class OrganizationVehicleRelRepository : IOrganizationVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<OrganizationVehicleRel> All
        {
            get { return context.OrganizationVehicleRels; }
        }

        public IQueryable<OrganizationVehicleRel> GetOrganizationVehicles(Expression<Func<OrganizationVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationVehicleRels.Where(filter);
            }
            return context.OrganizationVehicleRels;
        }


        public IQueryable<OrganizationVehicleRel> AllIncluding(params Expression<Func<OrganizationVehicleRel, object>>[] includeProperties)
        {
            IQueryable<OrganizationVehicleRel> query = context.OrganizationVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationVehicleRel Find(int id)
        {
            return context.OrganizationVehicleRels.Find(id);
        }

        public IQueryable<OrganizationVehicleRel> Get(Expression<Func<OrganizationVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationVehicleRels.Where(filter);
            }
            return context.OrganizationVehicleRels;
        }

        public void InsertOrUpdate(OrganizationVehicleRel organizationvehiclerel)
        {
            if (organizationvehiclerel.Id == default(int)) {
                // New entity
                context.OrganizationVehicleRels.Add(organizationvehiclerel);
            } else {
                // Existing entity
                context.Entry(organizationvehiclerel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationvehiclerel = context.OrganizationVehicleRels.Find(id);
            context.OrganizationVehicleRels.Remove(organizationvehiclerel);
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

    public interface IOrganizationVehicleRelRepository : IDisposable
    {
        IQueryable<OrganizationVehicleRel> All { get; }
        IQueryable<OrganizationVehicleRel> AllIncluding(params Expression<Func<OrganizationVehicleRel, object>>[] includeProperties);
        OrganizationVehicleRel Find(int id);
        IQueryable<OrganizationVehicleRel> Get(Expression<Func<OrganizationVehicleRel, bool>> filter);
        IQueryable<OrganizationVehicleRel> GetOrganizationVehicles(Expression<Func<OrganizationVehicleRel, bool>> filter);
        void InsertOrUpdate(OrganizationVehicleRel organizationvehiclerel);
        void Delete(int id);
        void Save();
    }
}