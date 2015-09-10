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
    public class EventCommentRepository : IEventCommentRepository
    {
        readonly ACDBContext context;

        public EventCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<EventComment> All
        {
            get { return context.EventComments; }
        }

        public IQueryable<EventComment> AllIncluding(params Expression<Func<EventComment, object>>[] includeProperties)
        {
            IQueryable<EventComment> query = context.EventComments;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public EventComment Find(int id)
        {
            return context.EventComments.Find(id);
        }

        public IQueryable<EventComment> Get(Expression<Func<EventComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.EventComments.Where(filter);
            }
            return context.EventComments;
        }

        public void InsertOrUpdate(EventComment eventcomment)
        {
            if (eventcomment.Id == default(int)) {
                // New entity
                context.EventComments.Add(eventcomment);
            } else {
                // Existing entity
                context.Entry(eventcomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventcomment = context.EventComments.Find(id);
            context.EventComments.Remove(eventcomment);
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

    public interface IEventCommentRepository : IDisposable
    {
        IQueryable<EventComment> All { get; }
        IQueryable<EventComment> AllIncluding(params Expression<Func<EventComment, object>>[] includeProperties);
        EventComment Find(int id);
        IQueryable<EventComment> Get(Expression<Func<EventComment, bool>> filter);
        void InsertOrUpdate(EventComment eventcomment);
        void Delete(int id);
        void Save();
    }
}