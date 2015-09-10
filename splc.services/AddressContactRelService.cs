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
    public class AddressContactRelService
    {
        private readonly IKeyedRepository<int, AddressContactRel> _repo;
        private readonly AddressContactRelValidator _validation;

        public AddressContactRelService(IKeyedRepository<int, AddressContactRel> repo)
        {
            _repo = repo;
            _validation = new AddressContactRelValidator();
        }

        public IList<AddressContactRel> All()
        {
            return _repo.All().ToList();
        }

        public AddressContactRel FindBy(Expression<Func<AddressContactRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressContactRel> FilterBy(Expression<Func<AddressContactRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressContactRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressContactRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressContactRel item in items)
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

        public bool Update(AddressContactRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressContactRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressContactRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressContactRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

