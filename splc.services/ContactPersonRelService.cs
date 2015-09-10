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
    public class ContactPersonRelService
    {
        private readonly IKeyedRepository<int, ContactPersonRel> _repo;
        private readonly CommonContactPersonRelValidator _validation;

        public ContactPersonRelService(IKeyedRepository<int, ContactPersonRel> repo)
        {
            _repo = repo;
            _validation = new CommonContactPersonRelValidator();
        }

        public IList<ContactPersonRel> All()
        {
            return _repo.All().ToList();
        }

        public ContactPersonRel FindBy(Expression<Func<ContactPersonRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ContactPersonRel> FilterBy(Expression<Func<ContactPersonRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ContactPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ContactPersonRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (ContactPersonRel person in items)
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

        public bool Update(ContactPersonRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ContactPersonRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ContactPersonRel> entities)
        {
            return _repo.Delete(entities);
        }

        public ContactPersonRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

