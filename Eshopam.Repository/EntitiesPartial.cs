namespace Eshopam.Repository
{
    public partial class User
    {
        public User(int id, string username, string password, string fullname, string role)
            :this()
        {
            Id = id;
            Username = username;
            Password = password;
            Fullname = fullname;
            Role = role;
        }
    }

    public partial class Category
    {

        public Category(int id, string name, int userId)
            : this()
        {
            Id = id;
            Name = name;
            UserId = userId;
        }

        public Category(int id, string name, int userId, User user)
            : this(id, name, userId)
        {     
            User = user;
        }
    }

    public partial class Product
    {
        public Product()
        {

        }

        public Product(int id, string code, string name, string description,
            float price, byte[] photo, int categoryId, int userId)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            Photo = photo;
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}
