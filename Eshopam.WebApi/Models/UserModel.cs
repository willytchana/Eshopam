using Eshopam.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopam.WebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public UserModel()
        {

        }

        public UserModel(int id, string username, string fullname, string role)
        {
            Id = id;
            Username = username;
            Fullname = fullname;
            Role = role;
        }



        public UserModel(int id, string username, string fullname, string role, string password)
            : this(id, username, fullname, role)
        {
            Password = password;
        }


        public UserModel(User user)
            :this(user?.Id ?? 0, user?.Username, user?.Fullname, user?.Role)
        {

        }
    }
}