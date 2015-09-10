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
    public class HairColorService
    {
        private readonly IKeyedRepository<int, HairColor> _repo;
        private readonly HairColorValidator _validation;

        public HairColorService(IKeyedRepository<int, HairColor> repo)
        {
            _repo = repo;
            _validation = new HairColorValidator();
        }

        public IList<HairColor> All()
        {
            return _repo.All().ToList();
        }

        public HairColor FindBy(Expression<Func<HairColor, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<HairColor> FilterBy(Expression<Func<HairColor, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(HairColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<HairColor> items, out IEnumerable<string> brokenRules)
        {
            foreach (HairColor item in items)
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

        public bool Update(HairColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(HairColor entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<HairColor> entities)
        {
            return _repo.Delete(entities);
        }

        public HairColor FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
