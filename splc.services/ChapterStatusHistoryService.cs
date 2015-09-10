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
    public class ChapterStatusHistoryService
    {
        private readonly IKeyedRepository<int, ChapterStatusHistory> _repo;
        private readonly ChapterStatusHistoryValidator _validation;

        public ChapterStatusHistoryService(IKeyedRepository<int, ChapterStatusHistory> repo)
        {
            _repo = repo;
            _validation = new ChapterStatusHistoryValidator();
        }

        public IList<ChapterStatusHistory> All()
        {
            return _repo.All().ToList();
        }

        public ChapterStatusHistory FindBy(Expression<Func<ChapterStatusHistory, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ChapterStatusHistory> FilterBy(Expression<Func<ChapterStatusHistory, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ChapterStatusHistory entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ChapterStatusHistory> items, out IEnumerable<string> brokenRules)
        {
            foreach (ChapterStatusHistory item in items)
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

        public bool Update(ChapterStatusHistory entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ChapterStatusHistory entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ChapterStatusHistory> entities)
        {
            return _repo.Delete(entities);
        }

        public ChapterStatusHistory FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

