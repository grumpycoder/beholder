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
    public class MotorcycleMakeService
    {
        private readonly IKeyedRepository<int, MotorcycleMake> _repo;
        private readonly MotorcycleMakeValidator _validation;

        public MotorcycleMakeService(IKeyedRepository<int, MotorcycleMake> repo)
        {
            _repo = repo;
            _validation = new MotorcycleMakeValidator();
        }

        public IList<MotorcycleMake> All()
        {
            return _repo.All().ToList();
        }

        public MotorcycleMake FindBy(Expression<Func<MotorcycleMake, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<MotorcycleMake> FilterBy(Expression<Func<MotorcycleMake, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(MotorcycleMake entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<MotorcycleMake> items, out IEnumerable<string> brokenRules)
        {
            foreach (MotorcycleMake item in items)
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

        public bool Update(MotorcycleMake entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(MotorcycleMake entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<MotorcycleMake> entities)
        {
            return _repo.Delete(entities);
        }

        public MotorcycleMake FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

