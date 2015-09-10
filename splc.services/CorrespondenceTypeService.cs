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
    public class CorrespondenceTypeService
    {
        private readonly IKeyedRepository<int, CorrespondenceType> _repo;
        private readonly CorrespondenceTypeValidator _validation;

        public CorrespondenceTypeService(IKeyedRepository<int, CorrespondenceType> repo)
        {
            _repo = repo;
            _validation = new CorrespondenceTypeValidator();
        }

        public IList<CorrespondenceType> All()
        {
            return _repo.All().ToList();
        }

        public CorrespondenceType FindBy(Expression<Func<CorrespondenceType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<CorrespondenceType> FilterBy(Expression<Func<CorrespondenceType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(CorrespondenceType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<CorrespondenceType> items, out IEnumerable<string> brokenRules)
        {
            foreach (CorrespondenceType item in items)
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

        public bool Update(CorrespondenceType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(CorrespondenceType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<CorrespondenceType> entities)
        {
            return _repo.Delete(entities);
        }

        public CorrespondenceType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

