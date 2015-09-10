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
    public class AliasChapterRelService
    {
        private readonly IKeyedRepository<int, AliasChapterRel> _repo;
        private readonly AliasChapterRelValidator _validation;

        public AliasChapterRelService(IKeyedRepository<int, AliasChapterRel> repo)
        {
            _repo = repo;
            _validation = new AliasChapterRelValidator();
        }

        public IList<AliasChapterRel> All()
        {
            return _repo.All().ToList();
        }

        public AliasChapterRel FindBy(Expression<Func<AliasChapterRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AliasChapterRel> FilterBy(Expression<Func<AliasChapterRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AliasChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AliasChapterRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AliasChapterRel item in items)
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

        public bool Update(AliasChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AliasChapterRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AliasChapterRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AliasChapterRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

