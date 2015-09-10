using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class VehicleCommentRepository : IVehicleCommentRepository
    {
        readonly ACDBContext context;

        public VehicleCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<VehicleComment> All
        {
            get { return context.VehicleComments; }
        }

        public IQueryable<VehicleComment> AllIncluding(params Expression<Func<VehicleComment, object>>[] includeProperties)
        {
            IQueryable<VehicleComment> query = context.VehicleComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<VehicleComment> GetComments(Expression<Func<VehicleComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.VehicleComments.Where(filter);
            }
            return context.VehicleComments;
        }

        public VehicleComment Find(int id)
        {
            return context.VehicleComments.Find(id);
        }

        public void InsertOrUpdate(VehicleComment vehiclecomment)
        {
            if (vehiclecomment.Id == default(int))
            {
                // New entity
                context.VehicleComments.Add(vehiclecomment);
            }
            else
            {
                // Existing entity
                context.Entry(vehiclecomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var vehiclecomment = context.VehicleComments.Find(id);
            context.VehicleComments.Remove(vehiclecomment);
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

    public interface IVehicleCommentRepository : IDisposable
    {
        IQueryable<VehicleComment> All { get; }
        IQueryable<VehicleComment> AllIncluding(params Expression<Func<VehicleComment, object>>[] includeProperties);
        VehicleComment Find(int id);
        IQueryable<VehicleComment> GetComments(Expression<Func<VehicleComment, bool>> filter);
        void InsertOrUpdate(VehicleComment vehiclecomment);
        void Delete(int id);
        void Save();
    }
}
