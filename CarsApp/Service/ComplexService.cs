using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsApp.Repository;
using CarsApp.Entities;

namespace CarsApp.Service
{
    public class ComplexService
    {
        public bool Authenticate(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) ||
                String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Incorrect username or password.");

            var user = UserRepository.GetUsers().ToList().Find(p => p.Username.Equals(username));

            if (user != null)
            {
                var result = user.Password.Equals(password);
                return result;
            }
            else
            {
                return false;
            }
        }

        public bool SignUp(string username, string password)
        {
            return UserRepository.SignUp(username, password);
        }

        public IEnumerable<Car> GetCars()
        {
            return CarRepository.GetCars();
        }

        public bool AddCar(string model, decimal price, int year)
        {
            return CarRepository.AddCar(model, price, year);
        }
    }
}
