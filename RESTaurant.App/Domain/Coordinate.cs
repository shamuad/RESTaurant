using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTaurant.App.Domain
{
    public class Coordinate
    {
        public double XCoord { get; set; }
        public double YCoord { get; set; }

        public Coordinate(double xCoord, double yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
        }
    }
}
