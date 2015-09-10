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
    public class StateService
    {
        private readonly IKeyedRepository<int, State> _repo;
        private readonly StateValidator _validation;

        public StateService(IKeyedRepository<int, State> repo)
        {
            _repo = repo;
            _validation = new StateValidator();
        }

        public IList<State> All()
        {
            return _repo.All().ToList();
        }

        public State FindBy(Expression<Func<State, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<State> FilterBy(Expression<Func<State, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(State entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<State> items, out IEnumerable<string> brokenRules)
        {
            foreach (State item in items)
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

        public bool Update(State entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(State entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<State> entities)
        {
            return _repo.Delete(entities);
        }

        public State FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

