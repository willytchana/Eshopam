using System;

namespace Eshopam.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserModel User { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(int id, string code, string name, string description, float price,
          string photo,  int categoryId, int userId, DateTime createdAt):this()
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            Photo = Photo;
            CategoryId = categoryId;
            UserId = userId;
            CreatedAt = createdAt;
        }

        public ProductModel(int id, string code, string name, string description, float price, string photo, 
            int categoryId, CategoryModel category, int userId, UserModel user, DateTime createdAt)
            :this(id, code, name, description, price, photo, categoryId, userId, createdAt)
        {
           
            Category = category;
            User = user;
        }
    }
}