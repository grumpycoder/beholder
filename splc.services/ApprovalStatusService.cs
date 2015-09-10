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
    public class ApprovalStatusService
    {
        private readonly IKeyedRepository<int, ApprovalStatus> _repo;
        private readonly ApprovalStatusValidator _validation;

        public ApprovalStatusService(IKeyedRepository<int, ApprovalStatus> repo)
        {
            _repo = repo;
            _validation = new ApprovalStatusValidator();
        }

        public IList<ApprovalStatus> All()
        {
            return _repo.All().ToList();
        }

        public ApprovalStatus FindBy(Expression<Func<ApprovalStatus, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ApprovalStatus> FilterBy(Expression<Func<ApprovalStatus, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ApprovalStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ApprovalStatus> items, out IEnumerable<string> brokenRules)
        {
            foreach (ApprovalStatus item in items)
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

        public bool Update(ApprovalStatus entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ApprovalStatus entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ApprovalStatus> entities)
        {
            return _repo.Delete(entities);
        }

        public ApprovalStatus FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
