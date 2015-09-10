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
    public class MediaTypeService
    {
        private readonly IKeyedRepository<int, MediaType> _repo;
        private readonly MediaTypeValidator _validation;

        public MediaTypeService(IKeyedRepository<int, MediaType> repo)
        {
            _repo = repo;
            _validation = new MediaTypeValidator();
        }

        public IList<MediaType> All()
        {
            return _repo.All().ToList();
        }

        public MediaType FindBy(Expression<Func<MediaType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<MediaType> FilterBy(Expression<Func<MediaType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(MediaType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<MediaType> items, out IEnumerable<string> brokenRules)
        {
            foreach (MediaType item in items)
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

        public bool Update(MediaType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(MediaType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<MediaType> entities)
        {
            return _repo.Delete(entities);
        }

        public MediaType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
