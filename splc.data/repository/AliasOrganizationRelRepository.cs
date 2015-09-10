using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class AliasOrganizationRelRepository : IAliasOrganizationRelRepository
    {

        private readonly ACDBContext context;

        public AliasOrganizationRelRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<AliasOrganizationRel> All
        {
            get { return context.AliasOrganizationRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null); }
        }

        public IQueryable<AliasOrganizationRel> AllIncluding(params Expression<Func<AliasOrganizationRel, object>>[] includeProperties)
        {
            IQueryable<AliasOrganizationRel> query = context.AliasOrganizationRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AliasOrganizationRel Find(int id)
        {
            var rel = context.AliasOrganizationRels.Find(id);
            if (rel.Alias.DateDeleted == null && rel.Organization.DateDeleted == null && rel.DateDeleted == null && rel.Organization.RemovalStatusId == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdate(AliasOrganizationRel aliasorganizationrel)
        {
            if (aliasorganizationrel.Id == default(int))
            {
                // New entity
                context.AliasOrganizationRels.Add(aliasorganizationrel);
            }
            else
            {
                // Existing entity
                context.Entry(aliasorganizationrel.Alias).State = EntityState.Modified;
                context.Entry(aliasorganizationrel).State = EntityState.Modified;
            }
        }

        public IQueryable<AliasOrganizationRel> GetAliases(Expression<Func<AliasOrganizationRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.AliasOrganizationRels.Where(filter).Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null);
            }
            return context.AliasOrganizationRels.Where(x => x.DateDeleted == null && x.Alias.DateDeleted == null && x.Organization.DateDeleted == null && x.Organization.RemovalStatusId == null);
        }

        public void Delete(int id)
        {
            var aliasorganizationrel = context.AliasOrganizationRels.Find(id);
            context.AliasOrganizationRels.Remove(aliasorganizationrel);
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

    public interface IAliasOrganizationRelRepository : IDisposable
    {
        IQueryable<AliasOrganizationRel> All { get; }
        IQueryable<AliasOrganizationRel> AllIncluding(params Expression<Func<AliasOrganizationRel, object>>[] includeProperties);
        AliasOrganizationRel Find(int id);
        IQueryable<AliasOrganizationRel> GetAliases(Expression<Func<AliasOrganizationRel, bool>> filter);
        void InsertOrUpdate(AliasOrganizationRel aliasorganizationrel);
        void Delete(int id);
        void Save();
    }
}