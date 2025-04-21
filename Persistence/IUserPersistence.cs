using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Persistence
{
    public interface IUserPersistence
    {
        public User? Save(User user);
        public User? FindUserByEmail(string email);
        public bool Login(string email, string password);
    }
}
