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
    public class CompanyService
    {
        private readonly IKeyedRepository<int, Company> _repo;
        private readonly CompanyValidator _validation;

        public CompanyService(IKeyedRepository<int, Company> repo)
        {
            _repo = repo;
            _validation = new CompanyValidator();
        }

        public IList<Company> All()
        {
            return _repo.All().ToList();
        }

        public Company FindBy(Expression<Func<Company, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<Company> FilterBy(Expression<Func<Company, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(Company entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<Company> items, out IEnumerable<string> brokenRules)
        {
            foreach (Company item in items)
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

        public bool Update(Company entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(Company entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<Company> entities)
        {
            return _repo.Delete(entities);
        }

        public Company FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}

