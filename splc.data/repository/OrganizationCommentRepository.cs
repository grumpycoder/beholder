using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class OrganizationCommentRepository : IOrganizationCommentRepository
    {
        readonly ACDBContext context;

        public OrganizationCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<OrganizationComment> All
        {
            get { return context.OrganizationComments; }
        }

        public IQueryable<OrganizationComment> AllIncluding(params Expression<Func<OrganizationComment, object>>[] includeProperties)
        {
            IQueryable<OrganizationComment> query = context.OrganizationComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<OrganizationComment> GetComments(Expression<Func<OrganizationComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationComments.Where(filter);
            }
            return context.OrganizationComments;
        }

        public OrganizationComment Find(int id)
        {
            return context.OrganizationComments.Find(id);
        }

        public void InsertOrUpdate(OrganizationComment organizationcomment)
        {
            if (organizationcomment.Id == default(int))
            {
                // New entity
                context.OrganizationComments.Add(organizationcomment);
            }
            else
            {
                // Existing entity
                context.Entry(organizationcomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationcomment = context.OrganizationComments.Find(id);
            context.OrganizationComments.Remove(organizationcomment);
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

    public interface IOrganizationCommentRepository : IDisposable
    {
        IQueryable<OrganizationComment> All { get; }
        IQueryable<OrganizationComment> AllIncluding(params Expression<Func<OrganizationComment, object>>[] includeProperties);
        OrganizationComment Find(int id);
        IQueryable<OrganizationComment> GetComments(Expression<Func<OrganizationComment, bool>> filter);
        void InsertOrUpdate(OrganizationComment organizationcomment);
        void Delete(int id);
        void Save();
    }
}
