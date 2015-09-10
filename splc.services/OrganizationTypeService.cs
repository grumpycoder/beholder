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
    public class OrganizationTypeService
    {
        private readonly IKeyedRepository<int, OrganizationType> _repo;
        private readonly OrganizationTypeValidator _validation;

        public OrganizationTypeService(IKeyedRepository<int, OrganizationType> repo)
        {
            _repo = repo;
            _validation = new OrganizationTypeValidator();
        }

        public IList<OrganizationType> All()
        {
            return _repo.All().ToList();
        }

        public OrganizationType FindBy(Expression<Func<OrganizationType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<OrganizationType> FilterBy(Expression<Func<OrganizationType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(OrganizationType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<OrganizationType> items, out IEnumerable<string> brokenRules)
        {
            foreach (OrganizationType item in items)
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

        public bool Update(OrganizationType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(OrganizationType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<OrganizationType> entities)
        {
            return _repo.Delete(entities);
        }

        public OrganizationType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
