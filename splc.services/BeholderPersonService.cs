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
    public class BeholderPersonService
    {
        private readonly IKeyedRepository<int, BeholderPerson> _repo;
        private readonly BeholderPersonValidator _validation;


        public BeholderPersonService(IKeyedRepository<int, BeholderPerson> repo)
        {
            _repo = repo;
           
            _validation = new BeholderPersonValidator();
        }

        public IList<BeholderPerson> All()
        {
            return _repo.All().ToList();
        }

        public BeholderPerson FindBy(Expression<Func<BeholderPerson, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<BeholderPerson> FilterBy(Expression<Func<BeholderPerson, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(BeholderPerson entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<BeholderPerson> items, out IEnumerable<string> brokenRules)
        {
            foreach (BeholderPerson person in items)
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

        public bool Update(BeholderPerson entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(BeholderPerson entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<BeholderPerson> entities)
        {
            return _repo.Delete(entities);
        }

        public BeholderPerson FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
