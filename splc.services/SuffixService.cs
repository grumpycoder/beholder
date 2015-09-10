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
    public class SuffixService
    {
        private readonly IKeyedRepository<int, Suffix> _repo;
        private readonly SuffixValidator _validation;

        public SuffixService(IKeyedRepository<int, Suffix> repo)
        {
            _repo = repo;
            _validation = new SuffixValidator();
        }

        public IList<Suffix> All()
        {
            return _repo.All().ToList();
        }

        public Suffix FindBy(Expression<Func<Suffix, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Suffix> FilterBy(Expression<Func<Suffix, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Suffix entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Suffix> items, out IEnumerable<string> brokenRules)
        {
            foreach (Suffix item in items)
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

        public bool Update(Suffix entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Suffix entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Suffix> entities)
        {
            return _repo.Delete(entities);
        }

        public Suffix FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

