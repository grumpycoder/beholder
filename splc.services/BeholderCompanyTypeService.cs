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
    public class BeholderCompanyTypeService
    {
        private readonly IKeyedRepository<int, BeholderCompanyType> _repo;
        private readonly BeholderCompanyTypeValidator _validation;

        public BeholderCompanyTypeService(IKeyedRepository<int, BeholderCompanyType> repo)
        {
            _repo = repo;
            _validation = new BeholderCompanyTypeValidator();
        }

        public IList<BeholderCompanyType> All()
        {
            return _repo.All().ToList();
        }

        public BeholderCompanyType FindBy(Expression<Func<BeholderCompanyType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<BeholderCompanyType> FilterBy(Expression<Func<BeholderCompanyType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(BeholderCompanyType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<BeholderCompanyType> items, out IEnumerable<string> brokenRules)
        {
            foreach (BeholderCompanyType item in items)
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

        public bool Update(BeholderCompanyType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(BeholderCompanyType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<BeholderCompanyType> entities)
        {
            return _repo.Delete(entities);
        }

        public BeholderCompanyType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

