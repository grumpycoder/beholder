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
    public class RaceService
    {
        private readonly IKeyedRepository<int, Race> _repo;
        private readonly RaceValidator _validation;

        public RaceService(IKeyedRepository<int, Race> repo)
        {
            _repo = repo;
            _validation = new RaceValidator();
        }

        public IList<Race> All()
        {
            return _repo.All().ToList();
        }

        public Race FindBy(Expression<Func<Race, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Race> FilterBy(Expression<Func<Race, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Race entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Race> items, out IEnumerable<string> brokenRules)
        {
            foreach (Race item in items)
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

        public bool Update(Race entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Race entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Race> entities)
        {
            return _repo.Delete(entities);
        }

        public Race FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

