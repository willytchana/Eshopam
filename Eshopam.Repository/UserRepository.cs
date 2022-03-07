using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Eshopam.Repository
{
    public class UserRepository
    {
        private readonly EshopamEntities db;
        public UserRepository()
        {
            db = new EshopamEntities();
        }

        public User Get(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Get(string username)
        {
            return db.Users.FirstOrDefault(x => x.Username == username);
        }

        public User Get(string username, string password)
        {
            var user = Get(username);
            if (user?.Password == CreateMD5Hash(password))
                return user;
            return null;
        }

        public User Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var u = Get(user.Username);
            if (u != null)
                throw new DuplicateWaitObjectException($"Username {user.Username} already exist !");

            user.Password = CreateMD5Hash(user.Password);
            
            user = db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        public User Set(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var oldUser = new EshopamEntities().Users.Find(user.Id);

            if (oldUser == null)
                throw new KeyNotFoundException($"User not found !");

            var u = Get(user.Username);
            if (u != null && u.Id != oldUser.Id)
                throw new DuplicateWaitObjectException($"Username {user.Username} already exist !");

            user.Password = oldUser.Password != user.Password ? CreateMD5Hash(user.Password) : oldUser.Password;

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return user;
        }

        private string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
