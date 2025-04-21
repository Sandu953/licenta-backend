using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Persistence;
using Backend.Model;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Backend.Service
{
    public class UserService
    {
        private IUserPersistence _userPersistence;

        public UserService(IUserPersistence userPersistence)
        {
            _userPersistence = userPersistence;
        }

        /// <summary>
        /// Function that logs in a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string email, string password)
        {
            return _userPersistence.Login(email, ComputeSha256Hash(password));
        }

        /// <summary>
        /// Function that saves a user to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User? Save(string email, string password, string userName)
        {
            var user = new User();
            user.Email = email;
            user.Password = ComputeSha256Hash(password);
            user.Username = userName; 
            return _userPersistence.Save(user);
        }

        /// <summary>
        /// Function that finds a user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User? FindUserByEmail(string email)
        {
            return _userPersistence.FindUserByEmail(email);
        }

        /// <summary>
        /// Function that computes the SHA256 hash of a string.
        /// Used for storing hashed passords in the database.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to hex string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hex
                }
                return builder.ToString();
            }
        }

    }
}
