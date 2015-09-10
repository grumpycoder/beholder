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
    public class ChapterTypeService
    {
        private readonly IKeyedRepository<int, ChapterType> _repo;
        private readonly ChapterTypeValidator _validation;

        public ChapterTypeService(IKeyedRepository<int, ChapterType> repo)
        {
            _repo = repo;
            _validation = new ChapterTypeValidator();
        }

        public IList<ChapterType> All()
        {
            return _repo.All().ToList();
        }

        public ChapterType FindBy(Expression<Func<ChapterType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ChapterType> FilterBy(Expression<Func<ChapterType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ChapterType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ChapterType> items, out IEnumerable<string> brokenRules)
        {
            foreach (ChapterType item in items)
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

        public bool Update(ChapterType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ChapterType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ChapterType> entities)
        {
            return _repo.Delete(entities);
        }

        public ChapterType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
