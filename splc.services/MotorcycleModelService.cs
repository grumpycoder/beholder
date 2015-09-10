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
    public class MotorcycleModelService
    {
        private readonly IKeyedRepository<int, MotorcycleModel> _repo;
        private readonly MotorcycleModelValidator _validation;

        public MotorcycleModelService(IKeyedRepository<int, MotorcycleModel> repo)
        {
            _repo = repo;
            _validation = new MotorcycleModelValidator();
        }

        public IList<MotorcycleModel> All()
        {
            return _repo.All().ToList();
        }

        public MotorcycleModel FindBy(Expression<Func<MotorcycleModel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<MotorcycleModel> FilterBy(Expression<Func<MotorcycleModel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(MotorcycleModel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<MotorcycleModel> items, out IEnumerable<string> brokenRules)
        {
            foreach (MotorcycleModel item in items)
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

        public bool Update(MotorcycleModel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(MotorcycleModel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<MotorcycleModel> entities)
        {
            return _repo.Delete(entities);
        }

        public MotorcycleModel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

