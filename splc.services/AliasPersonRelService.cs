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
    public class AliasPersonRelService
    {
        private readonly IKeyedRepository<int, AliasPersonRel> _repo;
        private readonly AliasPersonRelValidator _validation;

        public AliasPersonRelService(IKeyedRepository<int, AliasPersonRel> repo)
        {
            _repo = repo;
            _validation = new AliasPersonRelValidator();
        }

        public IList<AliasPersonRel> All()
        {
            return _repo.All().ToList();
        }

        public AliasPersonRel FindBy(Expression<Func<AliasPersonRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AliasPersonRel> FilterBy(Expression<Func<AliasPersonRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AliasPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AliasPersonRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AliasPersonRel person in items)
            {
                if (!_validation.IsValid(person))
                {
                    brokenRules = _validation.BrokenRules(person);
                    return false;
                }
            }
            brokenRules = null;
            return _repo.Add(items);
        }

        public bool Update(AliasPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AliasPersonRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AliasPersonRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AliasPersonRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

