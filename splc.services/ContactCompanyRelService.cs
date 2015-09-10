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
    public class ContactCompanyRelService
    {
        private readonly IKeyedRepository<int, ContactCompanyRel> _repo;
        private readonly ContactCompanyRelValidator _validation;

        public ContactCompanyRelService(IKeyedRepository<int, ContactCompanyRel> repo)
        {
            _repo = repo;
            _validation = new ContactCompanyRelValidator();
        }

        public IList<ContactCompanyRel> All()
        {
            return _repo.All().ToList();
        }

        public ContactCompanyRel FindBy(Expression<Func<ContactCompanyRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ContactCompanyRel> FilterBy(Expression<Func<ContactCompanyRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ContactCompanyRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ContactCompanyRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (ContactCompanyRel item in items)
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

        public bool Update(ContactCompanyRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ContactCompanyRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ContactCompanyRel> entities)
        {
            return _repo.Delete(entities);
        }

        public ContactCompanyRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

