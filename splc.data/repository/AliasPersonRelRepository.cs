using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AliasPersonRelRepository : IAliasPersonRelRepository
    {

        private readonly ACDBContext context;

        public AliasPersonRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AliasPersonRel> All
        {
            get { return context.AliasPersonRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.CommonPerson.DateDeleted == null); }
        }

        public IQueryable<AliasPersonRel> AllIncluding(params Expression<Func<AliasPersonRel, object>>[] includeProperties)
        {
            IQueryable<AliasPersonRel> query = context.AliasPersonRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.CommonPerson.DateDeleted == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AliasPersonRel Find(int id)
        {
            var rel = context.AliasPersonRels.Find(id);
            if (rel.Alias.DateDeleted == null && rel.CommonPerson.DateDeleted == null && rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public IQueryable<AliasPersonRel> GetAliases(Expression<Func<AliasPersonRel, bool>> filter)
        {
            if (filter != null)
            {
                //return context.AliasPersonRels.Where(filter).Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.CommonPerson.DateDeleted == null);
                return context.AliasPersonRels.Where(filter).Where(x => x.DateDeleted == null || x.Alias.DateDeleted == null);
            }
            return context.AliasPersonRels; //.Where(x => x.DateDeleted == null || x.Alias.DateDeleted == null || x.CommonPerson.DateDeleted == null);
        }


        public void InsertOrUpdate(AliasPersonRel aliaspersonrel)
        {
            if (aliaspersonrel.Id == default(int))
            {
                // New entity
                context.Aliases.Add(aliaspersonrel.Alias);
                context.AliasPersonRels.Add(aliaspersonrel);
            }
            else
            {
                // Existing entity
                context.Entry(aliaspersonrel.Alias).State = EntityState.Modified;
                context.Entry(aliaspersonrel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var aliaspersonrel = context.AliasPersonRels.Find(id);
            var alias = context.Aliases.Find(aliaspersonrel.AliasId);
            context.AliasPersonRels.Remove(aliaspersonrel);
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

    public interface IAliasPersonRelRepository : IDisposable
    {
        IQueryable<AliasPersonRel> All { get; }
        IQueryable<AliasPersonRel> AllIncluding(params Expression<Func<AliasPersonRel, object>>[] includeProperties);
        AliasPersonRel Find(int id);
        IQueryable<AliasPersonRel> GetAliases(Expression<Func<AliasPersonRel, bool>> filter);
        void InsertOrUpdate(AliasPersonRel aliaspersonrel);
        void Delete(int id);
        void Save();
    }
}