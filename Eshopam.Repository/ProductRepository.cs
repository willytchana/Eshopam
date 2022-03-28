using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Eshopam.Repository
{
    public class ProductRepository
    {
        private readonly EshopamEntities db;
        public ProductRepository()
        {
            db = new EshopamEntities();
        }

        public Product Get(int id)
        {
            return db.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product Get(string code)
        {
            return db.Products.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public Product Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var p = Get(product.Code);
            if (p != null)
                throw new DuplicateWaitObjectException($"Product code {product.Code} already exist !");

            
            product = db.Products.Add(product);
            db.SaveChanges();

            return product;
        }

        public Product Set(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var currentDb = new EshopamEntities();
            var oldProduct = currentDb.Products.Find(product.Id);

            if (oldProduct == null)
                throw new KeyNotFoundException($"Product not found !");

            var u = currentDb.Products.FirstOrDefault(x => x.Code.ToLower() == product.Code.ToLower());
            if (u != null && u.Id != oldProduct.Id)
                throw new DuplicateWaitObjectException($"Product code {product.Code} already exist !");

            if (product.Photo == null)
                product.Photo = oldProduct.Photo;

            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return product;
        }

        public Product Delete(int id)
        {
            var product = Get(id);
            db.Products.Remove(product);
            db.SaveChanges();

            return product;
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate).ToArray();
        }
    }
}
