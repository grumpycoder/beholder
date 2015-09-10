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
    public class GenderService
    {
        private readonly IKeyedRepository<int, Gender> _repo;
        private readonly CommonGenderValidator _validation;

        public GenderService(IKeyedRepository<int, Gender> repo)
        {
            _repo = repo;
            _validation = new CommonGenderValidator();
        }

        public IList<Gender> All()
        {
            return _repo.All().ToList();
        }

        public Gender FindBy(Expression<Func<Gender, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Gender> FilterBy(Expression<Func<Gender, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Gender entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Gender> items, out IEnumerable<string> brokenRules)
        {
            foreach (Gender person in items)
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

        public bool Update(Gender entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Gender entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Gender> entities)
        {
            return _repo.Delete(entities);
        }

        public Gender FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

