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
    public class WebIncidentTypeService
    {
        private readonly IKeyedRepository<int, WebIncidentType> _repo;
        private readonly WebIncidentTypeValidator _validation;

        public WebIncidentTypeService(IKeyedRepository<int, WebIncidentType> repo)
        {
            _repo = repo;
            _validation = new WebIncidentTypeValidator();
        }

        public IList<WebIncidentType> All()
        {
            return _repo.All().ToList();
        }

        public WebIncidentType FindBy(Expression<Func<WebIncidentType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<WebIncidentType> FilterBy(Expression<Func<WebIncidentType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(WebIncidentType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<WebIncidentType> items, out IEnumerable<string> brokenRules)
        {
            foreach (WebIncidentType item in items)
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

        public bool Update(WebIncidentType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(WebIncidentType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<WebIncidentType> entities)
        {
            return _repo.Delete(entities);
        }

        public WebIncidentType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

