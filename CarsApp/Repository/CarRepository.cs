using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsApp.Entities;

namespace CarsApp.Repository
{
    public static class CarRepository
    {
        private static IEnumerable<Car> _cars;

        static CarRepository()
        {
            _cars = new List<Car>()
            {
                new Car() { Id = 1, Model = "BMW", Price = 2000, Year = 2008 },
                new Car() { Id = 1, Model = "Reno", Price = 1500, Year = 1998 },
                new Car() { Id = 1, Model = "Kamaz", Price = 6000, Year = 2018 },
            };
        }

        public static IEnumerable<Car> GetCars()
        {
            return _cars;
        }

        public static bool AddCar(string model, decimal price, int year)
        {
            if (string.IsNullOrWhiteSpace(model) ||
                price < 0 ||
                year < 0) throw new ArgumentException("Incorrect car model, price or year parameters");

            var maxId = _cars.Last().Id;
            _cars.ToList().Add(new Car() { Id = maxId + 1, Model = model, Price = price, Year = year });
            return true;
        }
    }
}
