using Eshopam.Models;
using Eshopam.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eshopam.WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly CategoryRepository categoryRepository;
        public CategoriesController()
        {
            categoryRepository = new CategoryRepository();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
                return NotFound();
            return Ok(MapCategory(category));
        }

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var category = categoryRepository.Get(name);
            if (category == null)
                return NotFound();
            return Ok(MapCategory(category));
        }


        [HttpGet]
        public IHttpActionResult Find(string value)
        {
            var searchValue = value?.ToLower() ?? string.Empty;

            var categories = categoryRepository.Find
            (
                x =>
                x.Name.ToLower().Contains(searchValue)
            );

            return Ok(categories.Select(x => MapCategory(x)).ToArray());
        }

        [HttpPost]
        public IHttpActionResult Post(CategoryModel model)
        {
            try
            {

                if (model == null)
                    return BadRequest();

                var category = new Category
                (
                    0,
                    model.Name,
                    model.UserId
                );

                category = categoryRepository.Add(category);
                return Ok(MapCategory(category));
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public IHttpActionResult Put(CategoryModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var category = new Category
                (
                    model.Id,
                    model.Name,
                    model.UserId
                );
                category = categoryRepository.Set(category);
                return Ok(MapCategory(category));
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var category = categoryRepository.Delete(id);
            return Ok(MapCategory(category));
        }       
        
        public static CategoryModel MapCategory(Category category)
        {
            if (category == null)
                return null;

            return new CategoryModel
            (
                category.Id,
                category.Name,
                category.UserId,
                new UserModel
                (
                    category.User.Id,
                    category.User.Username,
                    category.User.Fullname,
                    category.User.Role
                )
            );
        }
    }
}
