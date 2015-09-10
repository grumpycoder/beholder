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
    public class VehicleMakeService
    {
        private readonly IKeyedRepository<int, VehicleMake> _repo;
        private readonly VehicleMakeValidator _validation;

        public VehicleMakeService(IKeyedRepository<int, VehicleMake> repo)
        {
            _repo = repo;
            _validation = new VehicleMakeValidator();
        }

        public IList<VehicleMake> All()
        {
            return _repo.All().ToList();
        }

        public VehicleMake FindBy(Expression<Func<VehicleMake, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<VehicleMake> FilterBy(Expression<Func<VehicleMake, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(VehicleMake entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<VehicleMake> items, out IEnumerable<string> brokenRules)
        {
            foreach (VehicleMake item in items)
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

        public bool Update(VehicleMake entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(VehicleMake entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<VehicleMake> entities)
        {
            return _repo.Delete(entities);
        }

        public VehicleMake FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

