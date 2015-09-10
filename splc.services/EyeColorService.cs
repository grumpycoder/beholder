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
    public class EyeColorService
    {
        private readonly IKeyedRepository<int, EyeColor> _repo;
        private readonly EyeColorValidator _validation;

        public EyeColorService(IKeyedRepository<int, EyeColor> repo)
        {
            _repo = repo;
            _validation = new EyeColorValidator();
        }

        public IList<EyeColor> All()
        {
            return _repo.All().ToList();
        }

        public EyeColor FindBy(Expression<Func<EyeColor, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<EyeColor> FilterBy(Expression<Func<EyeColor, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(EyeColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<EyeColor> items, out IEnumerable<string> brokenRules)
        {
            foreach (EyeColor item in items)
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

        public bool Update(EyeColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(EyeColor entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<EyeColor> entities)
        {
            return _repo.Delete(entities);
        }

        public EyeColor FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

