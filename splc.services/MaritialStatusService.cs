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
    public class MaritialStatusService
    {
        private readonly IKeyedRepository<int, MaritialStatus> _repo;
        private readonly MaritialStatusValidator _validation;

        public MaritialStatusService(IKeyedRepository<int, MaritialStatus> repo)
        {
            _repo = repo;
            _validation = new MaritialStatusValidator();
        }

        public IList<MaritialStatus> All()
        {
            return _repo.All().ToList();
        }

        public MaritialStatus FindBy(Expression<Func<MaritialStatus, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<MaritialStatus> FilterBy(Expression<Func<MaritialStatus, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(MaritialStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<MaritialStatus> items, out IEnumerable<string> brokenRules)
        {
            foreach (MaritialStatus item in items)
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

        public bool Update(MaritialStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(MaritialStatus entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<MaritialStatus> entities)
        {
            return _repo.Delete(entities);
        }

        public MaritialStatus FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
