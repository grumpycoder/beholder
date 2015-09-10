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
    public class UserService
    {
        private readonly IKeyedRepository<int, User> _repo;
        private readonly UserValidator _validation;

        public UserService(IKeyedRepository<int, User> repo)
        {
            _repo = repo;
            _validation = new UserValidator();
        }

        public IList<User> All()
        {
            return _repo.All().ToList();
        }

        public User FindBy(Expression<Func<User, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<User> FilterBy(Expression<Func<User, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(User entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<User> items, out IEnumerable<string> brokenRules)
        {
            foreach (User item in items)
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

        public bool Update(User entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(User entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<User> entities)
        {
            return _repo.Delete(entities);
        }

        public User FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

