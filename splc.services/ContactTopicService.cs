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
    public class ContactTopicService
    {
        private readonly IKeyedRepository<int, ContactTopic> _repo;
        private readonly ContactTopicValidator _validation;

        public ContactTopicService(IKeyedRepository<int, ContactTopic> repo)
        {
            _repo = repo;
            _validation = new ContactTopicValidator();
        }

        public IList<ContactTopic> All()
        {
            return _repo.All().ToList();
        }

        public ContactTopic FindBy(Expression<Func<ContactTopic, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ContactTopic> FilterBy(Expression<Func<ContactTopic, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ContactTopic entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ContactTopic> items, out IEnumerable<string> brokenRules)
        {
            foreach (ContactTopic item in items)
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

        public bool Update(ContactTopic entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ContactTopic entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ContactTopic> entities)
        {
            return _repo.Delete(entities);
        }

        public ContactTopic FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

