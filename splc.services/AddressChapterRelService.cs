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
    public class AddressChapterRelService
    {
        private readonly IKeyedRepository<int, AddressChapterRel> _repo;
        private readonly AddressChapterRelValidator _validation;

        public AddressChapterRelService(IKeyedRepository<int, AddressChapterRel> repo)
        {
            _repo = repo;
            _validation = new AddressChapterRelValidator();
        }

        public IList<AddressChapterRel> All()
        {
            return _repo.All().ToList();
        }

        public AddressChapterRel FindBy(Expression<Func<AddressChapterRel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AddressChapterRel> FilterBy(Expression<Func<AddressChapterRel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AddressChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AddressChapterRel> items, out IEnumerable<string> brokenRules)
        {
            foreach (AddressChapterRel item in items)
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

        public bool Update(AddressChapterRel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AddressChapterRel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AddressChapterRel> entities)
        {
            return _repo.Delete(entities);
        }

        public AddressChapterRel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
