using System;
using System.Collections.Generic;

namespace RESTaurant.App.Domain
{
    public class Address
    {
        public string Building { get; set; }
        public IEnumerable<double> Coord { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
    }
}
