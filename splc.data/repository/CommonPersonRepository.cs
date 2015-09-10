using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using splc.domain.Models;

namespace splc.data.repository
{ 
    public class CommonPersonRepository : ICommonPersonRepository
    {
        ACDBContext _ctx;

        public CommonPersonRepository(ACDBContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<CommonPerson> All
        {
            get { return _ctx.CommonPersons; }
        }

        public IQueryable<CommonPerson> AllIncluding(params Expression<Func<CommonPerson, object>>[] includeProperties)
        {
            IQueryable<CommonPerson> query = _ctx.CommonPersons;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public CommonPerson Find(int id)
        {
            return _ctx.CommonPersons.Find(id);
        }

        public void InsertOrUpdate(CommonPerson commonperson)
        {
            if (commonperson.Id == default(int)) {
                // New entity
                _ctx.CommonPersons.Add(commonperson);
            } else {
                // Existing entity
                _ctx.Entry(commonperson).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var commonperson = _ctx.CommonPersons.Find(id);
            _ctx.CommonPersons.Remove(commonperson);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public void Dispose() 
        {
            _ctx.Dispose();
        }
    }

    public interface ICommonPersonRepository : IDisposable
    {
        IQueryable<CommonPerson> All { get; }
        IQueryable<CommonPerson> AllIncluding(params Expression<Func<CommonPerson, object>>[] includeProperties);
        CommonPerson Find(int id);
        void InsertOrUpdate(CommonPerson commonperson);
        void Delete(int id);
        void Save();
    }
}