using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using splc.domain.Models;
using splc.data;

namespace splc.data.repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ACDBContext _ctx;

        public VehicleRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public Vehicle GetVehicle(int id)
        {
            var ent = _ctx.Vehicles.Find(id);
            if (ent.DateDeleted == null)
            {
                return ent;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Vehicle> GetVehicles()
        {
            return _ctx.Vehicles.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Vehicle> GetVehicles(Expression<Func<Vehicle, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Vehicles.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Vehicles.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public void InsertOrUpdate(Vehicle vehicle)
        {
            if (vehicle.Id == default(int))
            {
                // New entity
                _ctx.Vehicles.Add(vehicle);
            }
            else
            {
                // Existing entity
                _ctx.Entry(vehicle).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var vehicle = _ctx.Vehicles.Find(id);
            _ctx.Vehicles.Remove(vehicle);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Vehicle

        public VehicleVehicleRel GetVehicleVehicle(int id)
        {
            var rel = _ctx.VehicleVehicleRels.Find(id);
            if (rel.Vehicle.DateDeleted == null && rel.Vehicle2.DateDeleted == null && rel.Vehicle.RemovalStatusId == null && rel.Vehicle2.RemovalStatusId == null)
            {
                return rel;
            }
            else
            {
                return null;
            }

        }

        public IQueryable<VehicleVehicleRel> GetVehicleVehicles(Expression<Func<VehicleVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.VehicleVehicleRels.Where(filter).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null).Where(x => x.Vehicle2.DateDeleted == null && x.Vehicle2.RemovalStatusId == null);
            }
            return _ctx.VehicleVehicleRels.Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null).Where(x => x.Vehicle2.DateDeleted == null && x.Vehicle2.RemovalStatusId == null);
        }

        public void InsertOrUpdateVehicleVehicle(VehicleVehicleRel vehicleVehicleRel)
        {
            if (vehicleVehicleRel.Id == default(int))
            {
                // New entity
                _ctx.VehicleVehicleRels.Add(vehicleVehicleRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(vehicleVehicleRel).State = EntityState.Modified;
            }
        }

        public void DeleteVehicleVehicle(int id)
        {
            var vehicleVehicle = _ctx.VehicleVehicleRels.Find(id);
            _ctx.VehicleVehicleRels.Remove(vehicleVehicle);
        }
        
        #endregion

    }

    public interface IVehicleRepository
    {
        Vehicle GetVehicle(int id);
        IQueryable<Vehicle> GetVehicles();
        IQueryable<Vehicle> GetVehicles(Expression<Func<Vehicle, bool>> filter);
        void InsertOrUpdate(Vehicle vehicle);
        void Delete(int id);
        void Save();

        VehicleVehicleRel GetVehicleVehicle(int id);
        IQueryable<VehicleVehicleRel> GetVehicleVehicles(Expression<Func<VehicleVehicleRel, bool>> filter);
        void InsertOrUpdateVehicleVehicle(VehicleVehicleRel vehicleVehicleRel);
        void DeleteVehicleVehicle(int id);
    }
}