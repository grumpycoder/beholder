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
    public class OrganizationService
    {
        private readonly IKeyedRepository<int, Organization> _repo;
        private readonly OrganizationValidator _validation;

        public OrganizationService(IKeyedRepository<int, Organization> repo)
        {
            _repo = repo;
            _validation = new OrganizationValidator();
        }

        public IList<Organization> All()
        {
            return _repo.All().ToList();
        }

        public Organization FindBy(Expression<Func<Organization, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Organization> FilterBy(Expression<Func<Organization, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Organization entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Organization> items, out IEnumerable<string> brokenRules)
        {
            foreach (Organization item in items)
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

        public bool Update(Organization entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Organization entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Organization> entities)
        {
            return _repo.Delete(entities);
        }

        public Organization FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

