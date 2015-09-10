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
    public class RenewalPermissionTypeService
    {
        private readonly IKeyedRepository<int, RenewalPermmisionType> _repo;
        private readonly RenewalPermmisionTypeValidator _validation;

        public RenewalPermissionTypeService(IKeyedRepository<int, RenewalPermmisionType> repo)
        {
            _repo = repo;
            _validation = new RenewalPermmisionTypeValidator();
        }

        public IList<RenewalPermmisionType> All()
        {
            return _repo.All().ToList();
        }

        public RenewalPermmisionType FindBy(Expression<Func<RenewalPermmisionType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<RenewalPermmisionType> FilterBy(Expression<Func<RenewalPermmisionType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(RenewalPermmisionType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<RenewalPermmisionType> items, out IEnumerable<string> brokenRules)
        {
            foreach (RenewalPermmisionType item in items)
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

        public bool Update(RenewalPermmisionType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(RenewalPermmisionType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<RenewalPermmisionType> entities)
        {
            return _repo.Delete(entities);
        }

        public RenewalPermmisionType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
