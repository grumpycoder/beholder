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
    public class ReligionService
    {
        private readonly IKeyedRepository<int, Religion> _repo;
        private readonly ReligionValidator _validation;

        public ReligionService(IKeyedRepository<int, Religion> repo)
        {
            _repo = repo;
            _validation = new ReligionValidator();
        }

        public IList<Religion> All()
        {
            return _repo.All().ToList();
        }

        public Religion FindBy(Expression<Func<Religion, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Religion> FilterBy(Expression<Func<Religion, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Religion entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Religion> items, out IEnumerable<string> brokenRules)
        {
            foreach (Religion item in items)
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

        public bool Update(Religion entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Religion entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Religion> entities)
        {
            return _repo.Delete(entities);
        }

        public Religion FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

