using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace splc.data.repository
{
    public class EventRepository : IEventRepository
    {
        private ACDBContext _ctx;

        public EventRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        #region Main

        public Event GetEvent(int id)
        {
            var rel = _ctx.Events.Find(id);
            if (rel.DateDeleted == null && rel.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<Event> GetEvents()
        {
            return _ctx.Events.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public IQueryable<Event> GetEvents(Expression<Func<Event, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Events.Where(filter).Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            }
            return _ctx.Events.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
        }

        public Event GetEvent(User user, int id)
        {
            var e = _ctx.Events.Find(id);
            if (e.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && e.DateDeleted == null && e.RemovalStatusId == null)
            {
                return e;
            }
            return null;
        }

        public IQueryable<Event> GetEvents(User user)
        {
            return _ctx.Events.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public IQueryable<Event> GetEvents(User user, Expression<Func<Event, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.Events.Where(filter).Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
            }
            return _ctx.Events.Where(b => b.ConfidentialityType.SortOrder <= user.MaxConfidentialityLevel && b.DateDeleted == null && b.RemovalStatusId == null);
        }

        public void InsertOrUpdate(Event eventIncident)
        {
            if (eventIncident.Id == default(int))
            {
                // New entity
                _ctx.Events.Add(eventIncident);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventIncident).State = EntityState.Modified;
            }

        }

        public void InsertOrUpdate(Event eventIncident, List<EventType> eventTypes)
        {
            if (eventIncident.Id == default(int))
            {
                // New entity

                //if (eventTypes == null) return;

                //eventTypes.ForEach(x => eventIncident.EventEventTypeRels.Add(new EventEventTypeRel { EventId = eventIncident.Id, EventTypeId = x.Id }));
                //eventTypes.ForEach(x => eventIncident.EventEventTypeRels.Add(new EventEventTypeRel { EventTypeId = x.Id }));
                _ctx.Events.Add(eventIncident);

            }
            else
            {
                // Existing entity
                _ctx.EventEventTypeRels.RemoveRange(_ctx.EventEventTypeRels.Where(x => x.EventId == eventIncident.Id));
                if (eventTypes != null) eventTypes.ForEach(x => _ctx.EventEventTypeRels.Add(new EventEventTypeRel { EventId = eventIncident.Id, EventTypeId = x.Id }));

                _ctx.Entry(eventIncident).State = EntityState.Modified;
            }

        }

        public IQueryable<Event> All
        {
            get { return _ctx.Events.Where(x => x.DateDeleted == null && x.RemovalStatusId == null); }
        }

        public IQueryable<Event> AllIncluding(params Expression<Func<Event, object>>[] includeProperties)
        {
            IQueryable<Event> query = _ctx.Events.Where(x => x.DateDeleted == null && x.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Delete(int id)
        {
            var eventIncident = _ctx.Events.Find(id);
            _ctx.Events.Remove(eventIncident);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        #endregion


        #region Vehicle

        public EventVehicleRel GetEventVehicle(int id)
        {
            var rel = _ctx.EventVehicleRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.Vehicle.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.Vehicle.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventVehicleRel> GetEventVehicles(Expression<Func<EventVehicleRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventVehicleRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.Vehicle.DateDeleted == null && x.Event.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
            }
            return _ctx.EventVehicleRels.Where(x => x.Event.DateDeleted == null && x.Vehicle.DateDeleted == null && x.Event.RemovalStatusId == null && x.Vehicle.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventVehicle(EventVehicleRel eventvehiclerel)
        {
            if (eventvehiclerel.Id == default(int))
            {
                // New entity
                _ctx.EventVehicleRels.Add(eventvehiclerel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventvehiclerel).State = EntityState.Modified;
            }
        }

        public void DeleteEventVehicle(int id)
        {
            var eventVehicle = _ctx.EventVehicleRels.Find(id);
            _ctx.EventVehicleRels.Remove(eventVehicle);
        }

        #endregion


        #region Event

        public EventEventRel GetEventEvent(int id)
        {
            var rel = _ctx.EventEventRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.Event2.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.Event2.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdateEventEvent(EventEventRel eventEventRel)
        {
            if (eventEventRel.Id == default(long))
            {
                // New entity
                _ctx.EventEventRels.Add(eventEventRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventEventRel).State = EntityState.Modified;
            }
        }

        public void DeleteEventEvent(int id)
        {
            var eventEvent = _ctx.EventEventRels.Find(id);
            _ctx.EventEventRels.Remove(eventEvent);
        }

        #endregion


        #region MediaImage

        public EventMediaImageRel GetEventMediaImage(int id)
        {
            var rel = _ctx.EventMediaImageRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.MediaImage.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.MediaImage.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventMediaImageRel> GetEventMediaImages(Expression<Func<EventMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventMediaImageRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
            }
            return _ctx.EventMediaImageRels.Where(x => x.Event.DateDeleted == null && x.MediaImage.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaImage.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventMedia(EventMediaImageRel eventMediaImageRel)
        {
            if (eventMediaImageRel.Id == default(int))
            {
                // New entity
                _ctx.EventMediaImageRels.Add(eventMediaImageRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventMediaImageRel).State = EntityState.Modified;
            }
        }

        public void DeleteEventMedia(int id)
        {
            var eventMedia = _ctx.EventMediaImageRels.Find(id);
            _ctx.EventMediaImageRels.Remove(eventMedia);
        }

        #endregion


        #region MediaAudioVideo

        public EventMediaAudioVideoRel GetEventMediaAudioVideo(int id)
        {
            var rel = _ctx.EventMediaAudioVideoRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.MediaAudioVideo.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.MediaAudioVideo.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventMediaAudioVideoRel> GetEventMediaAudioVideos(Expression<Func<EventMediaAudioVideoRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventMediaAudioVideoRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
            }
            return _ctx.EventMediaAudioVideoRels.Where(x => x.Event.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.MediaAudioVideo.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaAudioVideo.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventMediaAudioVideo(EventMediaAudioVideoRel eventMediaAudioVideoRel)
        {
            if (eventMediaAudioVideoRel.Id == default(long))
            {
                // New entity
                _ctx.EventMediaAudioVideoRels.Add(eventMediaAudioVideoRel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventMediaAudioVideoRel).State = EntityState.Modified;
            }
        }

        public void DeleteEventMediaAudioVideo(int id)
        {
            var eventMedia = _ctx.EventMediaAudioVideoRels.Find(id);
            _ctx.EventMediaAudioVideoRels.Remove(eventMedia);
        }
        #endregion


        #region MediaWebsiteEGroup

        public EventMediaWebsiteEGroupRel GetEventMediaWebsiteEGroup(int id)
        {
            var rel = _ctx.EventMediaWebsiteEGroupRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.MediaWebsiteEGroup.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.MediaWebsiteEGroup.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventMediaWebsiteEGroupRel> GetEventMediaWebsiteEGroups(Expression<Func<EventMediaWebsiteEGroupRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventMediaWebsiteEGroupRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
            }
            return _ctx.EventMediaWebsiteEGroupRels.Where(x => x.Event.DateDeleted == null && x.MediaWebsiteEGroup.DateDeleted == null && x.Event.RemovalStatusId == null && x.MediaWebsiteEGroup.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventMediaWebsiteEGroup(EventMediaWebsiteEGroupRel eventmediawebsiteegrouprel)
        {
            if (eventmediawebsiteegrouprel.Id == default(int))
            {
                // New entity
                _ctx.EventMediaWebsiteEGroupRels.Add(eventmediawebsiteegrouprel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventmediawebsiteegrouprel).State = EntityState.Modified;
            }
        }

        public void DeleteEventMediaWebsiteEGroup(int id)
        {
            var eventMediaWebsiteEGroup = _ctx.EventMediaWebsiteEGroupRels.Find(id);
            _ctx.EventMediaWebsiteEGroupRels.Remove(eventMediaWebsiteEGroup);
        }
        #endregion


        #region Subscription
        public EventSubscriptionRel GetEventSubscription(int id)
        {
            var rel = _ctx.EventSubscriptionRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.Subscription.DateDeleted == null && rel.Event.RemovalStatusId == null && rel.Subscription.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventSubscriptionRel> GetEventSubscriptions(Expression<Func<EventSubscriptionRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventSubscriptionRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.Subscription.DateDeleted == null && x.Event.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
            }
            return _ctx.EventSubscriptionRels.Where(x => x.Event.DateDeleted == null && x.Subscription.DateDeleted == null && x.Event.RemovalStatusId == null && x.Subscription.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventSubscription(EventSubscriptionRel eventsubscriptionrel)
        {
            if (eventsubscriptionrel.Id == default(int))
            {
                // New entity
                _ctx.EventSubscriptionRels.Add(eventsubscriptionrel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventsubscriptionrel).State = EntityState.Modified;
            }
        }

        public void DeleteEventSubscription(int id)
        {
            var eventSubscription = _ctx.EventSubscriptionRels.Find(id);
            _ctx.EventSubscriptionRels.Remove(eventSubscription);
        }
        #endregion


        #region EventTypes
        public EventEventTypeRel GetEventEventType(int id)
        {
            var rel = _ctx.EventEventTypeRels.Find(id);
            if (rel.Event.DateDeleted == null && rel.EventType.DateDeleted == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<EventEventTypeRel> GetEventEventTypeRels(Expression<Func<EventEventTypeRel, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventEventTypeRels.Where(filter).Where(x => x.Event.DateDeleted == null && x.EventType.DateDeleted == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.EventEventTypeRels.Where(x => x.Event.DateDeleted == null && x.EventType.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public void InsertOrUpdateEventEventType(EventEventTypeRel eventeventtyperel)
        {
            if (eventeventtyperel.Id == default(int))
            {
                // New entity
                _ctx.EventEventTypeRels.Add(eventeventtyperel);
            }
            else
            {
                // Existing entity
                _ctx.Entry(eventeventtyperel).State = EntityState.Modified;
            }
        }

        public void DeleteEventEventType(int id)
        {
            var rel = _ctx.EventEventTypeRels.Find(id);
            _ctx.EventEventTypeRels.Remove(rel);
        }
        #endregion


        #region StatusHistory

        public IQueryable<EventStatusHistory> GetEventStatusHistories(Expression<Func<EventStatusHistory, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.EventStatusHistories.Where(filter).Where(x => x.Event.DateDeleted == null && x.DateDeleted == null && x.Event.RemovalStatusId == null);
            }
            return _ctx.EventStatusHistories.Where(x => x.Event.DateDeleted == null && x.DateDeleted == null && x.Event.RemovalStatusId == null);
        }

        public IQueryable<EventReport> GetEventsReport()
        {
            return _ctx.EventReports;
        }

        public EventStatusHistory GetEventStatusHistory(int id)
        {
            var rel = _ctx.EventStatusHistories.Find(id);
            if (rel.Event.DateDeleted == null && rel.DateDeleted == null && rel.Event.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        #endregion


    }

    public interface IEventRepository
    {
        Event GetEvent(int id);
        IQueryable<Event> GetEvents();
        IQueryable<Event> GetEvents(Expression<Func<Event, bool>> filter);
        Event GetEvent(User user, int id);
        IQueryable<Event> GetEvents(User user);
        IQueryable<Event> GetEvents(User user, Expression<Func<Event, bool>> filter);
        void InsertOrUpdate(Event eventIncident);
        void InsertOrUpdate(Event eventIncident, List<EventType> eventTypes);
        void Delete(int id);
        void Save();

        EventVehicleRel GetEventVehicle(int id);
        IQueryable<EventVehicleRel> GetEventVehicles(Expression<Func<EventVehicleRel, bool>> filter);
        void InsertOrUpdateEventVehicle(EventVehicleRel eventvehiclerel);
        void DeleteEventVehicle(int id);

        EventEventRel GetEventEvent(int id);
        void InsertOrUpdateEventEvent(EventEventRel eventEventRel);
        void DeleteEventEvent(int id);

        EventMediaImageRel GetEventMediaImage(int id);
        IQueryable<EventMediaImageRel> GetEventMediaImages(Expression<Func<EventMediaImageRel, bool>> filter);
        void InsertOrUpdateEventMedia(EventMediaImageRel eventMediaImageRel);
        void DeleteEventMedia(int id);

        EventMediaAudioVideoRel GetEventMediaAudioVideo(int id);
        IQueryable<EventMediaAudioVideoRel> GetEventMediaAudioVideos(Expression<Func<EventMediaAudioVideoRel, bool>> filter);
        void InsertOrUpdateEventMediaAudioVideo(EventMediaAudioVideoRel eventMediaAudioVideoRel);
        void DeleteEventMediaAudioVideo(int id);

        EventSubscriptionRel GetEventSubscription(int id);
        IQueryable<EventSubscriptionRel> GetEventSubscriptions(Expression<Func<EventSubscriptionRel, bool>> filter);
        void InsertOrUpdateEventSubscription(EventSubscriptionRel eventsubscriptionrel);
        void DeleteEventSubscription(int id);

        EventMediaWebsiteEGroupRel GetEventMediaWebsiteEGroup(int id);
        IQueryable<EventMediaWebsiteEGroupRel> GetEventMediaWebsiteEGroups(Expression<Func<EventMediaWebsiteEGroupRel, bool>> filter);
        void InsertOrUpdateEventMediaWebsiteEGroup(EventMediaWebsiteEGroupRel eventmediawebsiteegrouprel);
        void DeleteEventMediaWebsiteEGroup(int id);

        EventStatusHistory GetEventStatusHistory(int id);
        IQueryable<EventStatusHistory> GetEventStatusHistories(Expression<Func<EventStatusHistory, bool>> filter);

        IQueryable<EventReport> GetEventsReport();
    }
}