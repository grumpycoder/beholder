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
    public class UserTypeService
    {
        private readonly IKeyedRepository<int, UserType> _repo;
        private readonly UserTypeValidator _validation;

        public UserTypeService(IKeyedRepository<int, UserType> repo)
        {
            _repo = repo;
            _validation = new UserTypeValidator();
        }

        public IList<UserType> All()
        {
            return _repo.All().ToList();
        }

        public UserType FindBy(Expression<Func<UserType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<UserType> FilterBy(Expression<Func<UserType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(UserType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<UserType> items, out IEnumerable<string> brokenRules)
        {
            foreach (UserType item in items)
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

        public bool Update(UserType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(UserType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<UserType> entities)
        {
            return _repo.Delete(entities);
        }

        public UserType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
