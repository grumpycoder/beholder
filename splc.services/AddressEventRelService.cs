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
    public class AddressEventRelService
    {
        private readonly IKeyedRepository<int, AddressEventRel> _repo;
        private readonly AddressEventRelValidator _validation;

        public AddressEventRelService(IKeyedRepository<int, AddressEventRel> repo)
        {
            _repo = repo;
            _validation = new AddressEventRelValidator();
        }

        public IList<AddressEventRel> All()
        {
            return _repo.All().ToList();
        }

        public AddressEventRel FindBy(Expression<Func<AddressEventRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressEventRel> FilterBy(Expression<Func<AddressEventRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressEventRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressEventRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressEventRel item in items)
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

        public bool Update(AddressEventRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressEventRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressEventRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressEventRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

