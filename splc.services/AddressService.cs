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
    public class AddressService
    {
        private readonly IKeyedRepository<int, Address> _repo;
        private readonly AddressValidator _validation;

        public AddressService(IKeyedRepository<int, Address> repo)
        {
            _repo = repo;
            _validation = new AddressValidator();
        }

        public IList<Address> All()
        {
            return _repo.All().ToList();
        }

        public Address FindBy(Expression<Func<Address, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Address> FilterBy(Expression<Func<Address, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Address entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Address> items, out IEnumerable<string> brokenRules)
        {
            foreach (Address person in items)
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

        public bool Update(Address entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Address entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Address> entities)
        {
            return _repo.Delete(entities);
        }

        public Address FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
