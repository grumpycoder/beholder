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
    public class PersonScreenNameService
    {
        private readonly IKeyedRepository<int, PersonScreenName> _repo;
        private readonly BeholderPersonScreenNameValidator _validation;

        public PersonScreenNameService(IKeyedRepository<int, PersonScreenName> repo)
        {
            _repo = repo;
            _validation = new BeholderPersonScreenNameValidator();
        }

        public IList<PersonScreenName> All()
        {
            return _repo.All().ToList();
        }

        public PersonScreenName FindBy(Expression<Func<PersonScreenName, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<PersonScreenName> FilterBy(Expression<Func<PersonScreenName, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(PersonScreenName entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<PersonScreenName> items, out IEnumerable<string> brokenRules)
        {
            foreach (PersonScreenName person in items)
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

        public bool Update(PersonScreenName entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(PersonScreenName entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<PersonScreenName> entities)
        {
            return _repo.Delete(entities);
        }

        public PersonScreenName FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
