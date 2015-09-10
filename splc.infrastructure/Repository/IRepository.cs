using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace splc.infrastructure.Repository
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        int Count { get; }

        bool Add(TEntity entity);
        bool Add(IEnumerable<TEntity> items);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(IEnumerable<TEntity> entities);
    }
}
