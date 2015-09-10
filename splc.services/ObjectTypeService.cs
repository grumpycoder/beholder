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
    public class ObjectTypeService
    {
        private readonly IKeyedRepository<int, ObjectType> _repo;
        private readonly ObjectTypeValidator _validation;

        public ObjectTypeService(IKeyedRepository<int, ObjectType> repo)
        {
            _repo = repo;
            _validation = new ObjectTypeValidator();
        }

        public IList<ObjectType> All()
        {
            return _repo.All().ToList();
        }

        public ObjectType FindBy(Expression<Func<ObjectType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ObjectType> FilterBy(Expression<Func<ObjectType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ObjectType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ObjectType> items, out IEnumerable<string> brokenRules)
        {
            foreach (ObjectType item in items)
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

        public bool Update(ObjectType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ObjectType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ObjectType> entities)
        {
            return _repo.Delete(entities);
        }

        public ObjectType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

