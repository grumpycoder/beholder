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
    public class EventDocumentationTypeService
    {
        private readonly IKeyedRepository<int, EventDocumentationType> _repo;
        private readonly EventDocumentationTypeValidator _validation;

        public EventDocumentationTypeService(IKeyedRepository<int, EventDocumentationType> repo)
        {
            _repo = repo;
            _validation = new EventDocumentationTypeValidator();
        }

        public IList<EventDocumentationType> All()
        {
            return _repo.All().ToList();
        }

        public EventDocumentationType FindBy(Expression<Func<EventDocumentationType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<EventDocumentationType> FilterBy(Expression<Func<EventDocumentationType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(EventDocumentationType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<EventDocumentationType> items, out IEnumerable<string> brokenRules)
        {
            foreach (EventDocumentationType item in items)
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

        public bool Update(EventDocumentationType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(EventDocumentationType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<EventDocumentationType> entities)
        {
            return _repo.Delete(entities);
        }

        public EventDocumentationType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
