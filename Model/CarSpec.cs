using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class CarSpec
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int Year { get; set; }
        public string FuelType { get; set; }
        public int EngineSize { get; set; }
        public int Emissions { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
