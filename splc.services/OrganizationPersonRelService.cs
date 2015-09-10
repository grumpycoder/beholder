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
    public class OrganizationPersonRelService
    {
        private readonly IKeyedRepository<int, OrganizationPersonRel> _repo;
        private readonly OrganizationPersonRelValidator _validation;

        public OrganizationPersonRelService(IKeyedRepository<int, OrganizationPersonRel> repo)
        {
            _repo = repo;
            _validation = new OrganizationPersonRelValidator();
        }

        public IList<OrganizationPersonRel> All()
        {
            return _repo.All().ToList();
        }

        public OrganizationPersonRel FindBy(Expression<Func<OrganizationPersonRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<OrganizationPersonRel> FilterBy(Expression<Func<OrganizationPersonRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(OrganizationPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<OrganizationPersonRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (OrganizationPersonRel item in items)
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

        public bool Update(OrganizationPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(OrganizationPersonRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<OrganizationPersonRel> entities)
        {
            return _repo.Delete(entities);
        }

        public OrganizationPersonRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

