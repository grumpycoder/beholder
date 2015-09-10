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
    public class VehicleService
    {
        private readonly IKeyedRepository<int, Vehicle> _repo;
        private readonly VehicleValidator _validation;

        public VehicleService(IKeyedRepository<int, Vehicle> repo)
        {
            _repo = repo;
            _validation = new VehicleValidator();
        }

        public IList<Vehicle> All()
        {
            return _repo.All().ToList();
        }

        public Vehicle FindBy(Expression<Func<Vehicle, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Vehicle> FilterBy(Expression<Func<Vehicle, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Vehicle entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Vehicle> items, out IEnumerable<string> brokenRules)
        {
            foreach (Vehicle item in items)
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

        public bool Update(Vehicle entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Vehicle entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Vehicle> entities)
        {
            return _repo.Delete(entities);
        }

        public Vehicle FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
