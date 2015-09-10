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
    public class LicenseTypeService
    {
        private readonly IKeyedRepository<int, LicenseType> _repo;
        private readonly LicenseTypeValidator _validation;

        public LicenseTypeService(IKeyedRepository<int, LicenseType> repo)
        {
            _repo = repo;
            _validation = new LicenseTypeValidator();
        }

        public IList<LicenseType> All()
        {
            return _repo.All().ToList();
        }

        public LicenseType FindBy(Expression<Func<LicenseType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<LicenseType> FilterBy(Expression<Func<LicenseType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(LicenseType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<LicenseType> items, out IEnumerable<string> brokenRules)
        {
            foreach (LicenseType item in items)
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

        public bool Update(LicenseType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(LicenseType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<LicenseType> entities)
        {
            return _repo.Delete(entities);
        }

        public LicenseType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

