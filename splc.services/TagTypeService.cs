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
    public class TagTypeService
    {
        private readonly IKeyedRepository<int, TagType> _repo;
        private readonly TagTypeValidator _validation;

        public TagTypeService(IKeyedRepository<int, TagType> repo)
        {
            _repo = repo;
            _validation = new TagTypeValidator();
        }

        public IList<TagType> All()
        {
            return _repo.All().ToList();
        }

        public TagType FindBy(Expression<Func<TagType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<TagType> FilterBy(Expression<Func<TagType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(TagType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<TagType> items, out IEnumerable<string> brokenRules)
        {
            foreach (TagType item in items)
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

        public bool Update(TagType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(TagType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<TagType> entities)
        {
            return _repo.Delete(entities);
        }

        public TagType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
