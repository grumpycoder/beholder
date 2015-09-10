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
    public class VehicleTypeService
    {
        private readonly IKeyedRepository<int, VehicleType> _repo;
        private readonly VehicleTypeValidator _validation;

        public VehicleTypeService(IKeyedRepository<int, VehicleType> repo)
        {
            _repo = repo;
            _validation = new VehicleTypeValidator();
        }

        public IList<VehicleType> All()
        {
            return _repo.All().ToList();
        }

        public VehicleType FindBy(Expression<Func<VehicleType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<VehicleType> FilterBy(Expression<Func<VehicleType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(VehicleType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<VehicleType> items, out IEnumerable<string> brokenRules)
        {
            foreach (VehicleType item in items)
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

        public bool Update(VehicleType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(VehicleType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<VehicleType> entities)
        {
            return _repo.Delete(entities);
        }

        public VehicleType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
