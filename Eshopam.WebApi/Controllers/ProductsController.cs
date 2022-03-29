using Eshopam.Models;
using Eshopam.Repository;
using Newtonsoft.Json;
using Ploeh.Hyprlinkr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Eshopam.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ProductRepository productRepository;
        public ProductsController()
        {
            productRepository = new ProductRepository();
        }


        [HttpGet]
        public IHttpActionResult Get()
        {
            var products = productRepository.Find(x => true)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => MapProduct(x, Request));
            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var product = productRepository.Get(id);
            if (product == null)
                return NotFound();
            return Ok(MapProduct(product, Request));
        }

        [HttpGet]
        public IHttpActionResult Get(string code)
        {
            var product = productRepository.Get(code);
            if (product == null)
                return NotFound();
            return Ok(MapProduct(product, Request));
        }

        [HttpGet]
        public HttpResponseMessage GetImage(int imageId)
        {
            var product = productRepository.Get(imageId);
            if (product == null || product.Photo == null)
                return null;

            var stream = new MemoryStream(product.Photo);
            stream.Position = 0;

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return response;
        }


        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                var model = JsonConvert.DeserializeObject<ProductModel>(HttpContext.Current.Request.Form["model"]);
                if (model == null)
                    return BadRequest();

                byte[] photo = null;
                if (HttpContext.Current.Request.Files != null && HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0];
                    photo = new byte[file.InputStream.Length];
                    file.InputStream.Read(photo, 0, photo.Length);
                }

                var product = new Product
                (
                    0,
                    model.Code,
                    model.Name,
                    model.Description,
                    model.Price,
                    photo,
                    model.CategoryId,
                    model.UserId
                );
                product = productRepository.Add(product);
                return Ok(MapProduct(product, Request));
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
        public IHttpActionResult Put()
        {
            try
            {
                var model = JsonConvert.DeserializeObject<ProductModel>(HttpContext.Current.Request.Form["model"]);
                if (model == null)
                    return BadRequest();

                byte[] photo = null;
                if (HttpContext.Current.Request.Files != null && HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0];
                    photo = new byte[file.InputStream.Length];
                    file.InputStream.Read(photo, 0, photo.Length);
                }

                var product = new Product
                (
                     model.Id,
                     model.Code,
                     model.Name,
                     model.Description,
                     model.Price,
                     photo,
                     model.CategoryId,
                     model.UserId
                );
                product = productRepository.Set(product);
                return Ok(MapProduct(product, Request));
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
            try
            {                
                var product = productRepository.Delete(id);
                return Ok(MapProduct(product, Request));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public static ProductModel MapProduct(Product product, HttpRequestMessage request)
        {
            if (product == null)
                return null;

            var linker = new RouteLinker(request);
            var urlImage = linker.GetUri<ProductsController>(x => x.GetImage(product.Id)).ToString();
            return new ProductModel
            (
                product.Id,
                product.Code,
                product.Name,
                product.Description,
                product.Price,
                product.Photo != null ? urlImage : null,
                product.CategoryId,
                CategoriesController.MapCategory(product.Category),
                product.UserId,
                UsersController.MapUser(product.User),
                product.CreatedAt
            );
        }
    }
}
