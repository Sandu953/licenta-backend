using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Car
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public int KM { get; set; }
        public int CubicCapacity { get; set; }
        public int Power { get; set; }
        public string Fuel { get; set; }
        public string Gearbox { get; set; }
        public string DriveTrain { get; set; }
        public string Color { get; set; }
        public DateOnly FirstRegistration { get; set; }
        public string Description { get; set; }
        public string ImagesFolderPath { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
