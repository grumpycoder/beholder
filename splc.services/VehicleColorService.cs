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
    public class VehicleColorService
    {
        private readonly IKeyedRepository<int, VehicleColor> _repo;
        private readonly VehicleColorValidator _validation;

        public VehicleColorService(IKeyedRepository<int, VehicleColor> repo)
        {
            _repo = repo;
            _validation = new VehicleColorValidator();
        }

        public IList<VehicleColor> All()
        {
            return _repo.All().ToList();
        }

        public VehicleColor FindBy(Expression<Func<VehicleColor, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<VehicleColor> FilterBy(Expression<Func<VehicleColor, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(VehicleColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<VehicleColor> items, out IEnumerable<string> brokenRules)
        {
            foreach (VehicleColor item in items)
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

        public bool Update(VehicleColor entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(VehicleColor entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<VehicleColor> entities)
        {
            return _repo.Delete(entities);
        }

        public VehicleColor FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
