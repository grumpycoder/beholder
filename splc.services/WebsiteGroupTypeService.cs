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
    public class WebsiteGroupTypeService
    {
        private readonly IKeyedRepository<int, WebsiteGroupType> _repo;
        private readonly WebsiteGroupTypeValidator _validation;

        public WebsiteGroupTypeService(IKeyedRepository<int, WebsiteGroupType> repo)
        {
            _repo = repo;
            _validation = new WebsiteGroupTypeValidator();
        }

        public IList<WebsiteGroupType> All()
        {
            return _repo.All().ToList();
        }

        public WebsiteGroupType FindBy(Expression<Func<WebsiteGroupType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<WebsiteGroupType> FilterBy(Expression<Func<WebsiteGroupType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(WebsiteGroupType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<WebsiteGroupType> items, out IEnumerable<string> brokenRules)
        {
            foreach (WebsiteGroupType item in items)
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

        public bool Update(WebsiteGroupType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(WebsiteGroupType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<WebsiteGroupType> entities)
        {
            return _repo.Delete(entities);
        }

        public WebsiteGroupType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
