using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarsApp.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}", Id, Model, Year, Price);
        }
    }
}
