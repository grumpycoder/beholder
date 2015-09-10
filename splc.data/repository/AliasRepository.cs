using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class AliasRepository : IAliasRepository
    {
        private readonly ACDBContext context;

        public AliasRepository(ACDBContext Context)
        {
            context = Context; 
        }

        public IQueryable<Alias> All
        {
            get { return context.Aliases; }
        }

        public IQueryable<Alias> AllIncluding(params Expression<Func<Alias, object>>[] includeProperties)
        {
            IQueryable<Alias> query = context.Aliases;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Alias Find(int id)
        {
            return context.Aliases.Find(id);
        }

        public void InsertOrUpdate(Alias alias)
        {
            if (alias.Id == default(int)) {
                // New entity
                context.Aliases.Add(alias);
            } else {
                // Existing entity
                context.Entry(alias).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var alias = context.Aliases.Find(id);
            context.Aliases.Remove(alias);
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

    public interface IAliasRepository : IDisposable
    {
        IQueryable<Alias> All { get; }
        IQueryable<Alias> AllIncluding(params Expression<Func<Alias, object>>[] includeProperties);
        Alias Find(int id);
        void InsertOrUpdate(Alias alias);
        void Delete(int id);
        void Save();
    }
}