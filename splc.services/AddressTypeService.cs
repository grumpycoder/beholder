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
    public class AddressTypeService
    {
        private readonly IKeyedRepository<int, AddressType> _repo;
        private readonly AddressTypeValidator _validation;

        public AddressTypeService(IKeyedRepository<int, AddressType> repo)
        {
            _repo = repo;
            _validation = new AddressTypeValidator();
        }

        public IList<AddressType> All()
        {
            return _repo.All().ToList();
        }

        public AddressType FindBy(Expression<Func<AddressType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressType> FilterBy(Expression<Func<AddressType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressType> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressType item in items)
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

        public bool Update(AddressType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressType> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

