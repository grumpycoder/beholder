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
    public class ImageTypeService
    {
        private readonly IKeyedRepository<int, ImageType> _repo;
        private readonly ImageTypeValidator _validation;

        public ImageTypeService(IKeyedRepository<int, ImageType> repo)
        {
            _repo = repo;
            _validation = new ImageTypeValidator();
        }

        public IList<ImageType> All()
        {
            return _repo.All().ToList();
        }

        public ImageType FindBy(Expression<Func<ImageType, bool>> expression)
        {
            return _repo.FindBy(expression);
        }

        public IList<ImageType> FilterBy(Expression<Func<ImageType, bool>> expression)
        {
            return _repo.FilterBy(expression).ToList();
        }

        public bool Add(ImageType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Add(entity);
        }

        public bool Add(IEnumerable<ImageType> items, out IEnumerable<string> brokenRules)
        {
            foreach (ImageType item in items)
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

        public bool Update(ImageType entity, out IEnumerable<string> brokenRules)
        {
            if (!_validation.IsValid(entity))
            {
                brokenRules = _validation.BrokenRules(entity);
                return false;
            }
            brokenRules = null;
            return _repo.Update(entity);
        }

        public bool Delete(ImageType entity)
        {
            return _repo.Delete(entity);
        }

        public bool Delete(IEnumerable<ImageType> entities)
        {
            return _repo.Delete(entities);
        }

        public ImageType FindBy(int id)
        {
            return _repo.FindBy(id);
        }
    }
}
