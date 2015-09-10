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
    public class ChapterService
    {
        private readonly IKeyedRepository<int, Chapter> _repo;
        private readonly ChapterValidator _validation;

        public ChapterService(IKeyedRepository<int, Chapter> repo)
        {
            _repo = repo;
            _validation = new ChapterValidator();
        }

        public IList<Chapter> All()
        {
            return _repo.All().ToList();
        }

        public Chapter FindBy(Expression<Func<Chapter, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Chapter> FilterBy(Expression<Func<Chapter, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Chapter entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Chapter> items, out IEnumerable<string> brokenRules)
        {
            foreach (Chapter item in items)
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

        public bool Update(Chapter entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Chapter entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Chapter> entities)
        {
            return _repo.Delete(entities);
        }

        public Chapter FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
