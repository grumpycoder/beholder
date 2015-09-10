using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class VehicleVehicleRelRepository : IVehicleVehicleRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<VehicleVehicleRel> All
        {
            get { return context.VehicleVehicleRels; }
        }

        public IQueryable<VehicleVehicleRel> AllIncluding(params Expression<Func<VehicleVehicleRel, object>>[] includeProperties)
        {
            IQueryable<VehicleVehicleRel> query = context.VehicleVehicleRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public VehicleVehicleRel Find(int id)
        {
            return context.VehicleVehicleRels.Find(id);
        }

        public void InsertOrUpdate(VehicleVehicleRel VehicleVehiclerel)
        {
            if (VehicleVehiclerel.Id == default(int)) {
                // New entity
                context.VehicleVehicleRels.Add(VehicleVehiclerel);
            } else {
                // Existing entity
                context.Entry(VehicleVehiclerel).State = EntityState.Modified;
            }
        }

        public IQueryable<VehicleVehicleRel> GetVehicleVehicles(Expression<Func<VehicleVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.VehicleVehicleRels.Where(filter);
            }
            return context.VehicleVehicleRels;
        }

        public void Delete(int id)
        {
            var VehicleVehiclerel = context.VehicleVehicleRels.Find(id);
            context.VehicleVehicleRels.Remove(VehicleVehiclerel);
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

    public interface IVehicleVehicleRelRepository : IDisposable
    {
        IQueryable<VehicleVehicleRel> All { get; }
        IQueryable<VehicleVehicleRel> AllIncluding(params Expression<Func<VehicleVehicleRel, object>>[] includeProperties);
        VehicleVehicleRel Find(int id);
        IQueryable<VehicleVehicleRel> GetVehicleVehicles(Expression<Func<VehicleVehicleRel, bool>> filter);
        void InsertOrUpdate(VehicleVehicleRel VehicleVehiclerel);
        void Delete(int id);
        void Save();
    }
}