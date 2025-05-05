using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Backend.Persistence.Interfaces;

namespace Backend.Persistence
{
    public class UserPersistence : IUserPersistence
    {
        private readonly ApplicationDbContext _dbContext;

        public UserPersistence(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Function that finds a user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User? FindUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Function that verifies that 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Login(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }

        /// <summary>
        /// Saves a new user to the database.
        /// Used for register.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public User? Save(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        /// <summary>
        /// Function that checks if a user with the given email already exists.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool validEmail(string email)
        {
            return _dbContext.Users.Any(u => u.Email == email);
        }

        /// <summary>
        /// Function that checks if a user with the given username already exists.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool validUserName(string userName)
        {
            return _dbContext.Users.Any(u => u.Username == userName);
        }
    }
}
