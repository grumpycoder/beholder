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
    public class PublishedTypeService
    {
        private readonly IKeyedRepository<int, PublishedType> _repo;
        private readonly PublishedTypeValidator _validation;

        public PublishedTypeService(IKeyedRepository<int, PublishedType> repo)
        {
            _repo = repo;
            _validation = new PublishedTypeValidator();
        }

        public IList<PublishedType> All()
        {
            return _repo.All().ToList();
        }

        public PublishedType FindBy(Expression<Func<PublishedType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<PublishedType> FilterBy(Expression<Func<PublishedType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(PublishedType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<PublishedType> items, out IEnumerable<string> brokenRules)
        {
            foreach (PublishedType item in items)
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

        public bool Update(PublishedType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(PublishedType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<PublishedType> entities)
        {
            return _repo.Delete(entities);
        }

        public PublishedType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

