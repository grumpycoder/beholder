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
    public class ConfidentialityTypeService
    {
        private readonly IKeyedRepository<int, ConfidentialityType> _repo;
        private readonly ConfidentialityTypeValidator _validation;

        public ConfidentialityTypeService(IKeyedRepository<int, ConfidentialityType> repo)
        {
            _repo = repo;
            _validation = new ConfidentialityTypeValidator();
        }

        public IList<ConfidentialityType> All()
        {
            return _repo.All().ToList();
        }

        public ConfidentialityType FindBy(Expression<Func<ConfidentialityType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ConfidentialityType> FilterBy(Expression<Func<ConfidentialityType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ConfidentialityType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ConfidentialityType> items, out IEnumerable<string> brokenRules)
        {
            foreach (ConfidentialityType item in items)
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

        public bool Update(ConfidentialityType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ConfidentialityType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ConfidentialityType> entities)
        {
            return _repo.Delete(entities);
        }

        public ConfidentialityType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

