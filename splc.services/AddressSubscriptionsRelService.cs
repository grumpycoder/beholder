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
    public class AddressSubscriptionRelService
    {
        private readonly IKeyedRepository<int, AddressSubscriptionsRel> _repo;
        private readonly AddressSubscriptionsRelValidator _validation;

        public AddressSubscriptionRelService(IKeyedRepository<int, AddressSubscriptionsRel> repo)
        {
            _repo = repo;
            _validation = new AddressSubscriptionsRelValidator();
        }

        public IList<AddressSubscriptionsRel> All()
        {
            return _repo.All().ToList();
        }

        public AddressSubscriptionsRel FindBy(Expression<Func<AddressSubscriptionsRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressSubscriptionsRel> FilterBy(Expression<Func<AddressSubscriptionsRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressSubscriptionsRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressSubscriptionsRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressSubscriptionsRel item in items)
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

        public bool Update(AddressSubscriptionsRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressSubscriptionsRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressSubscriptionsRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressSubscriptionsRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

