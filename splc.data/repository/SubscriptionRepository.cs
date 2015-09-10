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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ACDBContext _ctx;

        public SubscriptionRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public MediaImageSubscriptionRel GetPrimaryImage(int chapterId)
        {
            return _ctx.MediaImageSubscriptionRels.Where(x => x.MediaImage.DateDeleted == null && x.MediaImage.RemovalStatusId == null).FirstOrDefault(x => x.SubscriptionId.Equals(chapterId));
        }

        public Subscription GetSubscription(int id)
        {
            var rel = _ctx.Subscriptions.Find(id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            else
            {
                return null;
            }

        }

        public IQueryable<Subscription> GetSubscriptions()
        {
            return _ctx.Subscriptions.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Subscription> GetSubscriptions(Expression<Func<Subscription, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Subscriptions.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Subscriptions.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public void InsertOrUpdate(Subscription subscription)
        {
            if (subscription.Id == default(int))
            {
                // New entity
                _ctx.Subscriptions.Add(subscription);
            }
            else
            {
                // Existing entity
                _ctx.Entry(subscription).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var subscription = _ctx.Subscriptions.Find(id);
            _ctx.Subscriptions.Remove(subscription);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
        #endregion


        #region Subscription

        public SubscriptionSubscriptionRel GetSubscriptionSubscription(int id)
        {
            var rel = _ctx.SubscriptionSubscriptionRels.Find(id);
            if (rel.Subscription.DateDeleted == null && rel.Subscription.RemovalStatusId == null && rel.Subscription2.DateDeleted == null && rel.Subscription2.RemovalStatusId == null) 
            {
                return rel;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<SubscriptionSubscriptionRel> GetSubscriptionSubscriptions(Expression<Func<SubscriptionSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.SubscriptionSubscriptionRels.Where(filter).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null).Where(x => x.Subscription2.DateDeleted == null && x.Subscription2.RemovalStatusId == null);
            }
            return _ctx.SubscriptionSubscriptionRels.Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null).Where(x => x.Subscription2.DateDeleted == null && x.Subscription2.RemovalStatusId == null);
        }

        public void InsertOrUpdateSubscriptionSubscription(SubscriptionSubscriptionRel subscriptionSubscriptionRel)
        {
            if (subscriptionSubscriptionRel.Id == default(int))
            {
                // New entity
                _ctx.SubscriptionSubscriptionRels.Add(subscriptionSubscriptionRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(subscriptionSubscriptionRel).State = EntityState.Modified;
            }
        }

        public void DeleteSubscriptionSubscription(int id)
        {
            var subscriptionSubscription = _ctx.SubscriptionSubscriptionRels.Find(id);
            _ctx.SubscriptionSubscriptionRels.Remove(subscriptionSubscription);
        }

        #endregion


        #region Vehicles

        public SubscriptionVehicleRel GetSubscriptionVehicle(int id)
        {
            var rel = _ctx.SubscriptionVehicleRels.Find(id);
            if (rel.Subscription.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.Vehicle.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<SubscriptionVehicleRel> GetSubscriptionVehicles(Expression<Func<SubscriptionVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.SubscriptionVehicleRels.Where(filter).Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.SubscriptionVehicleRels.Where(x => x.Subscription.DateDeleted == null && x.Subscription.RemovalStatusId == null).Where(x => x.Vehicle.DateDeleted == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateSubscriptionVehicle(SubscriptionVehicleRel subscriptionvehiclerel)
        {
            if (subscriptionvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.SubscriptionVehicleRels.Add(subscriptionvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(subscriptionvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteSubscriptionVehicle(int id)
        {
            var rel = _ctx.SubscriptionVehicleRels.Find(id);
            _ctx.SubscriptionVehicleRels.Remove(rel);
        }


        #endregion


    }

    public interface ISubscriptionRepository
    {
        Subscription GetSubscription(int id);
        IQueryable<Subscription> GetSubscriptions();
        IQueryable<Subscription> GetSubscriptions(Expression<Func<Subscription, bool>> filter);
        void InsertOrUpdate(Subscription subscription);
        void Delete(int id);
        void Save();

        SubscriptionSubscriptionRel GetSubscriptionSubscription(int id);
        IQueryable<SubscriptionSubscriptionRel> GetSubscriptionSubscriptions(Expression<Func<SubscriptionSubscriptionRel, bool>> filter);
        void InsertOrUpdateSubscriptionSubscription(SubscriptionSubscriptionRel subscriptionSubscriptionRel);
        void DeleteSubscriptionSubscription(int id);

        SubscriptionVehicleRel GetSubscriptionVehicle(int id);
        IQueryable<SubscriptionVehicleRel> GetSubscriptionVehicles(Expression<Func<SubscriptionVehicleRel, bool>> filter);
        void InsertOrUpdateSubscriptionVehicle(SubscriptionVehicleRel subscriptionvehiclerel);
        void DeleteSubscriptionVehicle(int id);

        MediaImageSubscriptionRel GetPrimaryImage(int subscrioptionId);

    }
}