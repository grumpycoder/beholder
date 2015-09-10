using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class ContactCommentRepository : IContactCommentRepository
    {
        readonly ACDBContext context;

        public ContactCommentRepository(ACDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ContactComment> All
        {
            get { return context.ContactComments; }
        }

        public IQueryable<ContactComment> AllIncluding(params Expression<Func<ContactComment, object>>[] includeProperties)
        {
            IQueryable<ContactComment> query = context.ContactComments;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<ContactComment> GetComments(Expression<Func<ContactComment, bool>> filter)
        {
            if (filter != null)
            {
                return context.ContactComments.Where(filter);
            }
            return context.ContactComments;
        }

        public ContactComment Find(int id)
        {
            return context.ContactComments.Find(id);
        }

        public void InsertOrUpdate(ContactComment contactcomment)
        {
            if (contactcomment.Id == default(int))
            {
                // New entity
                context.ContactComments.Add(contactcomment);
            }
            else
            {
                // Existing entity
                context.Entry(contactcomment).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contactcomment = context.ContactComments.Find(id);
            context.ContactComments.Remove(contactcomment);
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

    public interface IContactCommentRepository : IDisposable
    {
        IQueryable<ContactComment> All { get; }
        IQueryable<ContactComment> AllIncluding(params Expression<Func<ContactComment, object>>[] includeProperties);
        ContactComment Find(int id);
        IQueryable<ContactComment> GetComments(Expression<Func<ContactComment, bool>> filter);
        void InsertOrUpdate(ContactComment contactcomment);
        void Delete(int id);
        void Save();
    }
}