using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class MaritialStatusRepository : IMaritialStatusRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<MaritialStatus> All
        {
            get { return context.MaritialStatus; }
        }

        public IQueryable<MaritialStatus> AllIncluding(params Expression<Func<MaritialStatus, object>>[] includeProperties)
        {
            IQueryable<MaritialStatus> query = context.MaritialStatus;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MaritialStatus Find(int id)
        {
            return context.MaritialStatus.Find(id);
        }

        public void InsertOrUpdate(MaritialStatus maritialstatus)
        {
            if (maritialstatus.Id == default(int)) {
                // New entity
                context.MaritialStatus.Add(maritialstatus);
            } else {
                // Existing entity
                context.Entry(maritialstatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var maritialstatus = context.MaritialStatus.Find(id);
            context.MaritialStatus.Remove(maritialstatus);
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

    public interface IMaritialStatusRepository : IDisposable
    {
        IQueryable<MaritialStatus> All { get; }
        IQueryable<MaritialStatus> AllIncluding(params Expression<Func<MaritialStatus, object>>[] includeProperties);
        MaritialStatus Find(int id);
        void InsertOrUpdate(MaritialStatus maritialstatus);
        void Delete(int id);
        void Save();
    }
}