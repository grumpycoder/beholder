using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using splc.domain.Models;

namespace splc.data.repository
{
    public class NewsSourceRepository : INewsSourceRepository
    {
     
        private readonly ACDBContext _ctx;

        public NewsSourceRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<NewsSource> GetNewsSources()
        {
            return _ctx.NewsSources.Where(x => x.DateDeleted == null);
        }

        public IQueryable<NewsSource> GetNewsSources(Expression<Func<NewsSource, bool>> filter)
        {
            if (filter != null)
            {
                return _ctx.NewsSources.Where(filter).Where(x => x.DateDeleted == null);
            }
            return _ctx.NewsSources.Where(x => x.DateDeleted == null);
        }

        public NewsSource GetNewsSource(int id)
        {
            var rel = _ctx.NewsSources.Find(id);
            if (rel.DateDeleted == null)
            {
                return rel;
            }
            return null;
        }

        public void InsertOrUpdate(NewsSource newsSource)
        {
            if (newsSource.Id == default(int))
            {
                // New entity
                _ctx.NewsSources.Add(newsSource);
            }
            else
            {
                // Existing entity
                _ctx.Entry(newsSource).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var newsSource = _ctx.NewsSources.Find(id);
            _ctx.NewsSources.Remove(newsSource);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
   
    }

    public interface INewsSourceRepository
    {
        IQueryable<NewsSource> GetNewsSources();
        IQueryable<NewsSource> GetNewsSources(Expression<Func<NewsSource, bool>> filter);
        NewsSource GetNewsSource(int id);
        void InsertOrUpdate(NewsSource newsSource);
        void Delete(int id);
        void Save();

    }
}