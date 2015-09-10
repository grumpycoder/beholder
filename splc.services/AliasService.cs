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
    public class AliasService
    {
        private readonly IKeyedRepository<int, Alias> _repo;
        private readonly AliasValidator _validation;

        public AliasService(IKeyedRepository<int, Alias> repo)
        {
            _repo = repo;
            _validation = new AliasValidator();
        }

        public IList<Alias> All()
        {
            return _repo.All().ToList();
        }

        public Alias FindBy(Expression<Func<Alias, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Alias> FilterBy(Expression<Func<Alias, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Alias entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Alias> items, out IEnumerable<string> brokenRules)
        {
            foreach (Alias item in items)
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

        public bool Update(Alias entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Alias entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Alias> entities)
        {
            return _repo.Delete(entities);
        }

        public Alias FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

