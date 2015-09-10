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
    public class OrganizationMediaImageRelRepository : IOrganizationMediaImageRelRepository
    {
        ACDBContext context = new ACDBContext();

        public IQueryable<OrganizationMediaImageRel> All
        {
            get { return context.OrganizationMediaImageRels; }
        }

        public IQueryable<OrganizationMediaImageRel> AllIncluding(params Expression<Func<OrganizationMediaImageRel, object>>[] includeProperties)
        {
            IQueryable<OrganizationMediaImageRel> query = context.OrganizationMediaImageRels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<OrganizationMediaImageRel> Get(Expression<Func<OrganizationMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationMediaImageRels.Where(filter);
            }
            return context.OrganizationMediaImageRels;
        }
        
        public OrganizationMediaImageRel Find(long id)
        {
            return context.OrganizationMediaImageRels.Find(id);
        }

        public void InsertOrUpdate(OrganizationMediaImageRel organizationmediaimagerel)
        {
            if (organizationmediaimagerel.Id == default(long)) {
                // New entity
                //if (organizationmediaimagerel.Organization.Id == default(long))
                //{
                //    context.Organizations.Add(organizationmediaimagerel.Organization);
                //}
                //if (organizationmediaimagerel.MediaImage.Id == default(int))
                //{
                //    context.MediaImages.Add(organizationmediaimagerel.MediaImage);
                //}
                context.OrganizationMediaImageRels.Add(organizationmediaimagerel);
            }
            else
            {
                // Existing entity
                //context.Entry(organizationmediaimagerel.MediaImage).State = EntityState.Modified;
                //context.Entry(organizationmediaimagerel.Organization).State = EntityState.Modified;
                context.Entry(organizationmediaimagerel).State = EntityState.Modified;
            }
        }

        public void Delete(long id)
        {
            var organizationmediaimagerel = context.OrganizationMediaImageRels.Find(id);
            context.OrganizationMediaImageRels.Remove(organizationmediaimagerel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImages(Expression<Func<OrganizationMediaImageRel, bool>> filter)
        {
            if (filter != null)
            {
                return context.OrganizationMediaImageRels.Where(filter);
            }
            return context.OrganizationMediaImageRels;
        }


    }

    public interface IOrganizationMediaImageRelRepository : IDisposable
    {
        IQueryable<OrganizationMediaImageRel> All { get; }
        IQueryable<OrganizationMediaImageRel> AllIncluding(params Expression<Func<OrganizationMediaImageRel, object>>[] includeProperties);
        OrganizationMediaImageRel Find(long id);
        IQueryable<OrganizationMediaImageRel> Get(Expression<Func<OrganizationMediaImageRel, bool>> filter);
        IQueryable<OrganizationMediaImageRel> GetOrganizationMediaImages(Expression<Func<OrganizationMediaImageRel, bool>> filter);
        void InsertOrUpdate(OrganizationMediaImageRel organizationmediaimagerel);
        void Delete(long id);
        void Save();
    }
}