using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class SearchRepository : ISearchRepository
    {

        private readonly ACDBContext context;

        public SearchRepository(ACDBContext Context)
        {
            context = Context;
        }

        public IQueryable<Search> All
        {
            get { return context.Search; }
        }

        public IQueryable<Search> AllIncluding(params Expression<Func<Search, object>>[] includeProperties)
        {
            IQueryable<Search> query = context.Search;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<Search> Get(Expression<Func<Search, bool>> filter)
        {
            if (filter != null)
            {
                return context.Search.Where(filter);
            }
            return context.Search;
        }

        public Search Find(int id)
        {
            return context.Search.Find(id);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface ISearchRepository : IDisposable
    {
        IQueryable<Search> All { get; }
        IQueryable<Search> AllIncluding(params Expression<Func<Search, object>>[] includeProperties);
        IQueryable<Search> Get(Expression<Func<Search, bool>> filter);
        Search Find(int id);
        void Dispose();
    }
}