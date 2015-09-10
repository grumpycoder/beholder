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
    public class CommonContactTypeService
    {
        private readonly IKeyedRepository<int, CommonContactType> _repo;
        private readonly CommonContactTypeValidator _validation;

        public CommonContactTypeService(IKeyedRepository<int, CommonContactType> repo)
        {
            _repo = repo;
            _validation = new CommonContactTypeValidator();
        }

        public IList<CommonContactType> All()
        {
            return _repo.All().ToList();
        }

        public CommonContactType FindBy(Expression<Func<CommonContactType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<CommonContactType> FilterBy(Expression<Func<CommonContactType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(CommonContactType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<CommonContactType> items, out IEnumerable<string> brokenRules)
        {
            foreach (CommonContactType item in items)
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

        public bool Update(CommonContactType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(CommonContactType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<CommonContactType> entities)
        {
            return _repo.Delete(entities);
        }

        public CommonContactType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

