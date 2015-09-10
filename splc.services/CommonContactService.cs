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
    public class CommonContactService
    {
        private readonly IKeyedRepository<int, CommonContact> _repo;
        private readonly CommonContactValidator _validation;

        public CommonContactService(IKeyedRepository<int, CommonContact> repo)
        {
            _repo = repo;
            _validation = new CommonContactValidator();
        }

        public IList<CommonContact> All()
        {
            return _repo.All().ToList();
        }

        public CommonContact FindBy(Expression<Func<CommonContact, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<CommonContact> FilterBy(Expression<Func<CommonContact, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(CommonContact entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<CommonContact> items, out IEnumerable<string> brokenRules)
        {
            foreach (CommonContact item in items)
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

        public bool Update(CommonContact entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(CommonContact entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<CommonContact> entities)
        {
            return _repo.Delete(entities);
        }

        public CommonContact FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

