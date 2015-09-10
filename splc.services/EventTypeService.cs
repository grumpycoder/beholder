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
    public class EventTypeService
    {
        private readonly IKeyedRepository<int, EventType> _repo;
        private readonly EventTypeValidator _validation;

        public EventTypeService(IKeyedRepository<int, EventType> repo)
        {
            _repo = repo;
            _validation = new EventTypeValidator();
        }

        public IList<EventType> All()
        {
            return _repo.All().ToList();
        }

        public EventType FindBy(Expression<Func<EventType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<EventType> FilterBy(Expression<Func<EventType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(EventType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<EventType> items, out IEnumerable<string> brokenRules)
        {
            foreach (EventType item in items)
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

        public bool Update(EventType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(EventType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<EventType> entities)
        {
            return _repo.Delete(entities);
        }

        public EventType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
