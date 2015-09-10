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
    public class BusinessClassService
    {
        private readonly IKeyedRepository<int, BusinessClass> _repo;
        private readonly BusinessClassValidator _validation;

        public BusinessClassService(IKeyedRepository<int, BusinessClass> repo)
        {
            _repo = repo;
            _validation = new BusinessClassValidator();
        }

        public IList<BusinessClass> All()
        {
            return _repo.All().ToList();
        }

        public BusinessClass FindBy(Expression<Func<BusinessClass, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<BusinessClass> FilterBy(Expression<Func<BusinessClass, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(BusinessClass entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<BusinessClass> items, out IEnumerable<string> brokenRules)
        {
            foreach (BusinessClass item in items)
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

        public bool Update(BusinessClass entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(BusinessClass entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<BusinessClass> entities)
        {
            return _repo.Delete(entities);
        }

        public BusinessClass FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

