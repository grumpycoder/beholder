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
    public class CommonPersonService
     {
         private readonly IKeyedRepository<int, CommonPerson> _repo;
         private readonly CommonPersonValidator _validation;

         public CommonPersonService(IKeyedRepository<int, CommonPerson> repo)
        {
            _repo = repo;
            _validation = new CommonPersonValidator();
        }

         public IList<CommonPerson> All()
        {
            return _repo.All().ToList();
        }

         public CommonPerson FindBy(Expression<Func<CommonPerson, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

         public IList<CommonPerson> FilterBy(Expression<Func<CommonPerson, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

         public bool Add(CommonPerson entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

         public bool Add(IEnumerable<CommonPerson> items, out IEnumerable<string> brokenRules)
        {
            foreach (CommonPerson person in items)
            {
                if (!_validation.IsValid(person))
                {
                    brokenRules = _validation.BrokenRules(person);
                    return false;
                }
            }
            brokenRules = null;
            return _repo.Add(items);
        }

         public bool Update(CommonPerson entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
            
        }

         public bool Delete(CommonPerson entity)
        {
            return _repo.Delete(entity);
        }

         public bool Delete(IEnumerable<CommonPerson> entities)
        {
            return _repo.Delete(entities);
        }

         public CommonPerson FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
