using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AliasChapterRelRepository : IAliasChapterRelRepository
    {
        private readonly ACDBContext context;

        public AliasChapterRelRepository(ACDBContext Context)
        {
            context = Context;
        }


        public IQueryable<AliasChapterRel> All
        {
            get { return context.AliasChapterRels.Where(x => x.Alias.DateDeleted == null && x.Chapter.DateDeleted == null && x.DateDeleted == null && x.Chapter.RemovalStatusId == null); }
        }

        public IQueryable<AliasChapterRel> AllIncluding(params Expression<Func<AliasChapterRel, object>>[] includeProperties)
        {
            IQueryable<AliasChapterRel> query = context.AliasChapterRels.Where(x => x.Alias.DateDeleted == null && x.Chapter.DateDeleted == null && x.DateDeleted == null && x.Chapter.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AliasChapterRel Find(int id)
        {
            var rel = context.AliasChapterRels.Find(id);
            if (rel.Alias.DateDeleted == null && rel.Chapter.DateDeleted == null && rel.DateDeleted == null && rel.Chapter.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdate(AliasChapterRel aliaschapterrel)
        {
            if (aliaschapterrel.Id == default(int))
            {
                // New entity
                context.AliasChapterRels.Add(aliaschapterrel);
            }
            else
            {
                // Existing entity
                context.Entry(aliaschapterrel.Alias).State = EntityState.Modified;
                context.Entry(aliaschapterrel).State = EntityState.Modified;
            }
        }

        public IQueryable<AliasChapterRel> GetAliases(Expression<Func<AliasChapterRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AliasChapterRels.Where(filter).Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Chapter.DateDeleted == null && x.Chapter.RemovalStatusId == null);
            }
            return context.AliasChapterRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Chapter.DateDeleted == null && x.Chapter.RemovalStatusId == null);
        }

        public void Delete(int id)
        {
            //delete alias and aliaschpater relationship.
            var aliaschapterrel = context.AliasChapterRels.Find(id);
            context.Aliases.Remove(aliaschapterrel.Alias);
            context.AliasChapterRels.Remove(aliaschapterrel);
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

    public interface IAliasChapterRelRepository : IDisposable
    {
        IQueryable<AliasChapterRel> All { get; }
        IQueryable<AliasChapterRel> AllIncluding(params Expression<Func<AliasChapterRel, object>>[] includeProperties);
        AliasChapterRel Find(int id);
        IQueryable<AliasChapterRel> GetAliases(Expression<Func<AliasChapterRel, bool>> filter);
        void InsertOrUpdate(AliasChapterRel aliaschapterrel);
        void Delete(int id);
        void Save();
    }
}