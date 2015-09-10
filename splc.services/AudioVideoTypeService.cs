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
    public class AudioVideoTypeService
    {
        private readonly IKeyedRepository<int, AudioVideoType> _repo;
        private readonly AudioVideoTypeValidator _validation;

        public AudioVideoTypeService(IKeyedRepository<int, AudioVideoType> repo)
        {
            _repo = repo;
            _validation = new AudioVideoTypeValidator();
        }

        public IList<AudioVideoType> All()
        {
            return _repo.All().ToList();
        }

        public AudioVideoType FindBy(Expression<Func<AudioVideoType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<AudioVideoType> FilterBy(Expression<Func<AudioVideoType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(AudioVideoType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<AudioVideoType> items, out IEnumerable<string> brokenRules)
        {
            foreach (AudioVideoType item in items)
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

        public bool Update(AudioVideoType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(AudioVideoType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<AudioVideoType> entities)
        {
            return _repo.Delete(entities);
        }

        public AudioVideoType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
