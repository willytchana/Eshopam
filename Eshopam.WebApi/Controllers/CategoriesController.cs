using Eshopam.Repository;
using Eshopam.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return Ok(new CategoryModel(category));
        }

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var category = categoryRepository.Get(name);
            if (category == null)
                return NotFound();
            return Ok(new CategoryModel(category));
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

            return Ok(categories.Select( x => new CategoryModel(x)).ToArray());
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
                return Ok(new CategoryModel(category));
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
                return Ok(new CategoryModel(category));
            }

            catch (KeyNotFoundException ex)
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
            return Ok(new CategoryModel(category));
        }
    }
}
