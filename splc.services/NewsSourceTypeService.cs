using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using splc.infrastructure.Repository;
using splc.domain.Models;
using splc.domain.Validators;

namespace splc.services
{
    public class NewsSourceTypeService
    {
        private readonly IKeyedRepository<int, NewsSourceType> _repo;
        private readonly NewsSourceTypeValidator _validation;

        public NewsSourceTypeService(IKeyedRepository<int, NewsSourceType> repo)
        {
            _repo = repo;
            _validation = new NewsSourceTypeValidator();
        }

        public IList<NewsSourceType> All()
        {
            return _repo.All().ToList();
        }

        public NewsSourceType FindBy(Expression<Func<NewsSourceType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<NewsSourceType> FilterBy(Expression<Func<NewsSourceType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(NewsSourceType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<NewsSourceType> items, out IEnumerable<string> brokenRules)
        {
            foreach (NewsSourceType item in items)
            {
                if (!_validation.IsValid(item))
                {
                    brokenRules = _validation.BrokenRules(item);
                    return false;
                }
            }
            brokenRules = null;
            return _repo.Add(items);
        }

        public bool Update(NewsSourceType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(NewsSourceType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<NewsSourceType> entities)
        {
            return _repo.Delete(entities);
        }

        public NewsSourceType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
