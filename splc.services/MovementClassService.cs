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
    public class MovementClassService
    {
        private readonly IKeyedRepository<int, MovementClass> _repo;
        private readonly MovementClassValidator _validation;

        public MovementClassService(IKeyedRepository<int, MovementClass> repo)
        {
            _repo = repo;
            _validation = new MovementClassValidator();
        }

        public IList<MovementClass> All()
        {
            return _repo.All().ToList();
        }

        public MovementClass FindBy(Expression<Func<MovementClass, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<MovementClass> FilterBy(Expression<Func<MovementClass, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(MovementClass entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<MovementClass> items, out IEnumerable<string> brokenRules)
        {
            foreach (MovementClass item in items)
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

        public bool Update(MovementClass entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(MovementClass entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<MovementClass> entities)
        {
            return _repo.Delete(entities);
        }

        public MovementClass FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

