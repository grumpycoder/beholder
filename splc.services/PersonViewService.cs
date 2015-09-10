using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using splc.infrastructure.Repository;
using splc.domain;
using splc.domain.Validators;

namespace splc.services
{
    public class PersonViewService
    {
        private readonly IKeyedRepository<int, PersonView> _personViewRepo;
        private readonly PersonViewValidator _validation;

        public PersonViewService(IKeyedRepository<int, PersonView> PersonViewRepo)
        {
            _personViewRepo = PersonViewRepo;
            _validation = new PersonViewValidator();
        }

        public IList<PersonView> All()
        {
            return _personViewRepo.All().ToList();
        }

        public PersonView FindBy(Expression<Func<PersonView, bool>> expression)
        {
            return _personViewRepo.FindBy(expression);
        }

        public IList<PersonView> FilterBy(Expression<Func<PersonView, bool>> expression)
        {
            return _personViewRepo.FilterBy(expression).ToList();
        }

        public bool Add(PersonView entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _personViewRepo.Add(entity);
        }

        public bool Add(IEnumerable<PersonView> items, out IEnumerable<string> brokenRules)
        {
            foreach (PersonView person in items)
            {
                if (!_validation.IsValid(person))
                {
                    brokenRules = _validation.BrokenRules(person);
                    return false;
                }
            }
            brokenRules = null;
            return _personViewRepo.Add(items);
        }

        public bool Update(PersonView entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _personViewRepo.Update(entity);
        }

        public bool Delete(PersonView entity)
        {
            return _personViewRepo.Delete(entity);
        }

        public bool Delete(IEnumerable<PersonView> entities)
        {
            return _personViewRepo.Delete(entities);
        }

        public PersonView FindBy(int id)
        {
            return _personViewRepo.FindBy(id);
        }
    }
}
