using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsApp.Entities;
using System.Windows;

namespace CarsApp.Repository
{
    public static class UserRepository
    {
        private static IEnumerable<User> _users;

        static UserRepository()
        {
            _users = new List<User>()
            {
                new User() {Id = 1, Username = "red", Password = "red"},
                new User() {Id = 2, Username = "green", Password = "green"},
                new User() {Id = 3, Username = "blue", Password = "blue"}
            };
        }

        public static IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public static bool IsExists(string username)
        {
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("Incorrect username.");
            var user = _users.ToList().Find(p => p.Username.Equals(username));
            return user != null;
        }

        public static bool SignUp(string username, string password)
        {
            try
            {
                //if (IsExists(username)) throw new Exception("Username already exists.");
                if (IsExists(username)) return false;
                //var maxId = _users.Max(p => p.Id);
                var maxId = _users.Last().Id;
                ((List<User>)_users).Add(new User() { Id = maxId + 1, Username = username, Password = password });
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
                return false;
            }
        }
    }
}
