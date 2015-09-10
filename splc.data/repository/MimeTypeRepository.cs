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
    public class MimeTypeRepository : IMimeTypeRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<MimeType> All
        {
            get { return context.MimeTypes; }
        }

        public IQueryable<MimeType> AllIncluding(params Expression<Func<MimeType, object>>[] includeProperties)
        {
            IQueryable<MimeType> query = context.MimeTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MimeType Find(int id)
        {
            return context.MimeTypes.Find(id);
        }

        public void InsertOrUpdate(MimeType mimetype)
        {
            if (mimetype.Id == default(int)) {
                // New entity
                context.MimeTypes.Add(mimetype);
            } else {
                // Existing entity
                context.Entry(mimetype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var mimetype = context.MimeTypes.Find(id);
            context.MimeTypes.Remove(mimetype);
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

    public interface IMimeTypeRepository : IDisposable
    {
        IQueryable<MimeType> All { get; }
        IQueryable<MimeType> AllIncluding(params Expression<Func<MimeType, object>>[] includeProperties);
        MimeType Find(int id);
        void InsertOrUpdate(MimeType mimetype);
        void Delete(int id);
        void Save();
    }
}