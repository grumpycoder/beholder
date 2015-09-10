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
    public class AliasOrganizationRelService
    {
        private readonly IKeyedRepository<int, AliasOrganizationRel> _repo;
        private readonly AliasOrganizationRelValidator _validation;

        public AliasOrganizationRelService(IKeyedRepository<int, AliasOrganizationRel> repo)
        {
            _repo = repo;
            _validation = new AliasOrganizationRelValidator();
        }

        public IList<AliasOrganizationRel> All()
        {
            return _repo.All().ToList();
        }

        public AliasOrganizationRel FindBy(Expression<Func<AliasOrganizationRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AliasOrganizationRel> FilterBy(Expression<Func<AliasOrganizationRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AliasOrganizationRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AliasOrganizationRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AliasOrganizationRel item in items)
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

        public bool Update(AliasOrganizationRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AliasOrganizationRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AliasOrganizationRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AliasOrganizationRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

