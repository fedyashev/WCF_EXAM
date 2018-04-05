using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CarsApp.Entities;

namespace CarsApp.Store
{
    public class AppStore
    {
        public string Username { get; set; }
        public BindingList<Car> Cars { get; set; }

        public AppStore()
        {
            Username = null;
            Cars = new BindingList<Car>();
        }
    }
}
