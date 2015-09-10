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
    public class CommonCompanyTypeService
    {
        private readonly IKeyedRepository<int, CommonCompanyType> _repo;
        private readonly CommonCompanyTypeValidator _validation;

        public CommonCompanyTypeService(IKeyedRepository<int, CommonCompanyType> repo)
        {
            _repo = repo;
            _validation = new CommonCompanyTypeValidator();
        }

        public IList<CommonCompanyType> All()
        {
            return _repo.All().ToList();
        }

        public CommonCompanyType FindBy(Expression<Func<CommonCompanyType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<CommonCompanyType> FilterBy(Expression<Func<CommonCompanyType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(CommonCompanyType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<CommonCompanyType> items, out IEnumerable<string> brokenRules)
        {
            foreach (CommonCompanyType item in items)
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

        public bool Update(CommonCompanyType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(CommonCompanyType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<CommonCompanyType> entities)
        {
            return _repo.Delete(entities);
        }

        public CommonCompanyType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
