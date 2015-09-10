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
    public class ActiveStatusService
    {
        private readonly IKeyedRepository<int, ActiveStatus> _repo;
        private readonly ActiveStatusValidator _validation;

        public ActiveStatusService(IKeyedRepository<int, ActiveStatus> repo)
        {
            _repo = repo;
            _validation = new ActiveStatusValidator();
        }

        public IList<ActiveStatus> All()
        {
            return _repo.All().ToList();
        }

        public ActiveStatus FindBy(Expression<Func<ActiveStatus, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ActiveStatus> FilterBy(Expression<Func<ActiveStatus, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ActiveStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ActiveStatus> items, out IEnumerable<string> brokenRules)
        {
            foreach (ActiveStatus person in items)
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

        public bool Update(ActiveStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ActiveStatus entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ActiveStatus> entities)
        {
            return _repo.Delete(entities);
        }

        public ActiveStatus FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

