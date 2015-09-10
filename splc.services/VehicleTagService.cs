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
    public class VehicleTagService
    {
        private readonly IKeyedRepository<int, VehicleTag> _repo;
        private readonly VehicleTagValidator _validation;

        public VehicleTagService(IKeyedRepository<int, VehicleTag> repo)
        {
            _repo = repo;
            _validation = new VehicleTagValidator();
        }

        public IList<VehicleTag> All()
        {
            return _repo.All().ToList();
        }

        public VehicleTag FindBy(Expression<Func<VehicleTag, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<VehicleTag> FilterBy(Expression<Func<VehicleTag, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(VehicleTag entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<VehicleTag> items, out IEnumerable<string> brokenRules)
        {
            foreach (VehicleTag item in items)
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

        public bool Update(VehicleTag entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(VehicleTag entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<VehicleTag> entities)
        {
            return _repo.Delete(entities);
        }

        public VehicleTag FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
