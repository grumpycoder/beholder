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
    public class AddressPersonRelService
    {
        private readonly IKeyedRepository<int, AddressPersonRel> _repo;
        private readonly AddressPersonRelValidator _validation;

        public AddressPersonRelService(IKeyedRepository<int, AddressPersonRel> repo)
        {
            _repo = repo;
            _validation = new AddressPersonRelValidator();
        }

        public IList<AddressPersonRel> All()
        {
            return _repo.All().ToList();
        }

        public AddressPersonRel FindBy(Expression<Func<AddressPersonRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressPersonRel> FilterBy(Expression<Func<AddressPersonRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressPersonRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressPersonRel item in items)
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

        public bool Update(AddressPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressPersonRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressPersonRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressPersonRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
