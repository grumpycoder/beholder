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
    public class BeholderContactTypeService
    {
        private readonly IKeyedRepository<int, BeholderContactType> _repo;
        private readonly BeholderContactTypeValidator _validation;

        public BeholderContactTypeService(IKeyedRepository<int, BeholderContactType> repo)
        {
            _repo = repo;
            _validation = new BeholderContactTypeValidator();
        }

        public IList<BeholderContactType> All()
        {
            return _repo.All().ToList();
        }

        public BeholderContactType FindBy(Expression<Func<BeholderContactType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<BeholderContactType> FilterBy(Expression<Func<BeholderContactType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(BeholderContactType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<BeholderContactType> items, out IEnumerable<string> brokenRules)
        {
            foreach (BeholderContactType item in items)
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

        public bool Update(BeholderContactType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(BeholderContactType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<BeholderContactType> entities)
        {
            return _repo.Delete(entities);
        }

        public BeholderContactType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
