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
    public class PrefixService
    {
        private readonly IKeyedRepository<int, Prefix> _repo;
        private readonly PrefixValidator _validation;

        public PrefixService(IKeyedRepository<int, Prefix> repo)
        {
            _repo = repo;
            _validation = new PrefixValidator();
        }

        public IList<Prefix> All()
        {
            return _repo.All().ToList();
        }

        public Prefix FindBy(Expression<Func<Prefix, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Prefix> FilterBy(Expression<Func<Prefix, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Prefix entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Prefix> items, out IEnumerable<string> brokenRules)
        {
            foreach (Prefix item in items)
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

        public bool Update(Prefix entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Prefix entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Prefix> entities)
        {
            return _repo.Delete(entities);
        }

        public Prefix FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
