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
    public class AliasCompanyRelService
    {
        private readonly IKeyedRepository<int, AliasCompanyRel> _repo;
        private readonly AliasCompanyRelValidator _validation;

        public AliasCompanyRelService(IKeyedRepository<int, AliasCompanyRel> repo)
        {
            _repo = repo;
            _validation = new AliasCompanyRelValidator();
        }

        public IList<AliasCompanyRel> All()
        {
            return _repo.All().ToList();
        }

        public AliasCompanyRel FindBy(Expression<Func<AliasCompanyRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AliasCompanyRel> FilterBy(Expression<Func<AliasCompanyRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AliasCompanyRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AliasCompanyRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AliasCompanyRel item in items)
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

        public bool Update(AliasCompanyRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AliasCompanyRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AliasCompanyRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AliasCompanyRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

