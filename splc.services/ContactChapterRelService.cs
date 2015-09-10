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
    public class ContactChapterRelService
    {
        private readonly IKeyedRepository<int, ContactChapterRel> _repo;
        private readonly ContactChapterRelValidator _validation;

        public ContactChapterRelService(IKeyedRepository<int, ContactChapterRel> repo)
        {
            _repo = repo;
            _validation = new ContactChapterRelValidator();
        }

        public IList<ContactChapterRel> All()
        {
            return _repo.All().ToList();
        }

        public ContactChapterRel FindBy(Expression<Func<ContactChapterRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ContactChapterRel> FilterBy(Expression<Func<ContactChapterRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ContactChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ContactChapterRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (ContactChapterRel item in items)
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

        public bool Update(ContactChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ContactChapterRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ContactChapterRel> entities)
        {
            return _repo.Delete(entities);
        }

        public ContactChapterRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
