﻿using System;
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
    public class OrganizationStatusHistoryService
    {
        private readonly IKeyedRepository<int, OrganizationStatusHistory> _repo;
        private readonly OrganizationStatusHistoryValidator _validation;

        public OrganizationStatusHistoryService(IKeyedRepository<int, OrganizationStatusHistory> repo)
        {
            _repo = repo;
            _validation = new OrganizationStatusHistoryValidator();
        }

        public IList<OrganizationStatusHistory> All()
        {
            return _repo.All().ToList();
        }

        public OrganizationStatusHistory FindBy(Expression<Func<OrganizationStatusHistory, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<OrganizationStatusHistory> FilterBy(Expression<Func<OrganizationStatusHistory, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(OrganizationStatusHistory entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<OrganizationStatusHistory> items, out IEnumerable<string> brokenRules)
        {
            foreach (OrganizationStatusHistory item in items)
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

        public bool Update(OrganizationStatusHistory entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(OrganizationStatusHistory entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<OrganizationStatusHistory> entities)
        {
            return _repo.Delete(entities);
        }

        public OrganizationStatusHistory FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
