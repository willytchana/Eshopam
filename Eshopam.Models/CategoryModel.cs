namespace Eshopam.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public CategoryModel()
        {

        }

        public CategoryModel(int id, string name, int userId):this()
        {
            Id = id;
            Name = name;
            UserId = userId;
        }

        public CategoryModel(int id, string name, int userId, UserModel user)
            : this(id, name, userId)
        {
            User = user;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}