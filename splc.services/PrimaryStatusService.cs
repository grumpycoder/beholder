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
    public class PrimaryStatusService
    {
        private readonly IKeyedRepository<int, PrimaryStatus> _repo;
        private readonly PrimaryStatusValidator _validation;

        public PrimaryStatusService(IKeyedRepository<int, PrimaryStatus> repo)
        {
            _repo = repo;
            _validation = new PrimaryStatusValidator();
        }

        public IList<PrimaryStatus> All()
        {
            return _repo.All().ToList();
        }

        public PrimaryStatus FindBy(Expression<Func<PrimaryStatus, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<PrimaryStatus> FilterBy(Expression<Func<PrimaryStatus, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(PrimaryStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<PrimaryStatus> items, out IEnumerable<string> brokenRules)
        {
            foreach (PrimaryStatus item in items)
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

        public bool Update(PrimaryStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(PrimaryStatus entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<PrimaryStatus> entities)
        {
            return _repo.Delete(entities);
        }

        public PrimaryStatus FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

