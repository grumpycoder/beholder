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
    public class HairPatternService
    {
        private readonly IKeyedRepository<int, HairPattern> _repo;
        private readonly HairPatternValidator _validation;

        public HairPatternService(IKeyedRepository<int, HairPattern> repo)
        {
            _repo = repo;
            _validation = new HairPatternValidator();
        }

        public IList<HairPattern> All()
        {
            return _repo.All().ToList();
        }

        public HairPattern FindBy(Expression<Func<HairPattern, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<HairPattern> FilterBy(Expression<Func<HairPattern, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(HairPattern entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<HairPattern> items, out IEnumerable<string> brokenRules)
        {
            foreach (HairPattern item in items)
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

        public bool Update(HairPattern entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(HairPattern entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<HairPattern> entities)
        {
            return _repo.Delete(entities);
        }

        public HairPattern FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
