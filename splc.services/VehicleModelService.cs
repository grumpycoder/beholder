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
    public class VehicleModelService
    {
        private readonly IKeyedRepository<int, VehicleModel> _repo;
        private readonly VehicleModelValidator _validation;

        public VehicleModelService(IKeyedRepository<int, VehicleModel> repo)
        {
            _repo = repo;
            _validation = new VehicleModelValidator();
        }

        public IList<VehicleModel> All()
        {
            return _repo.All().ToList();
        }

        public VehicleModel FindBy(Expression<Func<VehicleModel, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<VehicleModel> FilterBy(Expression<Func<VehicleModel, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(VehicleModel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<VehicleModel> items, out IEnumerable<string> brokenRules)
        {
            foreach (VehicleModel item in items)
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

        public bool Update(VehicleModel entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(VehicleModel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<VehicleModel> entities)
        {
            return _repo.Delete(entities);
        }

        public VehicleModel FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

