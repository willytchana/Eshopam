using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
