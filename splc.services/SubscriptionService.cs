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
    public class SubscriptionService
    {
        private readonly IKeyedRepository<int, Subscription> _repo;
        private readonly SubscriptionValidator _validation;

        public SubscriptionService(IKeyedRepository<int, Subscription> repo)
        {
            _repo = repo;
            _validation = new SubscriptionValidator();
        }

        public IList<Subscription> All()
        {
            return _repo.All().ToList();
        }

        public Subscription FindBy(Expression<Func<Subscription, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Subscription> FilterBy(Expression<Func<Subscription, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Subscription entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Subscription> items, out IEnumerable<string> brokenRules)
        {
            foreach (Subscription item in items)
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

        public bool Update(Subscription entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Subscription entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Subscription> entities)
        {
            return _repo.Delete(entities);
        }

        public Subscription FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
