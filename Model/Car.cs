using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CarSpec")]
        public int SpecId { get; set; }
        public CarSpec CarSpec { get; set; }

        public string Vin { get; set; }
        public int HP { get; set; }
        public int year { get; set; }
        public int Km { get; set; }
        public string Location { get; set; }
        public string BodyType { get; set; }
        public string Description { get; set; }

        public ICollection<Auction> Auctions { get; set; }
        public ICollection<CarImage> CarImages { get; set; }
        public ICollection<UserInteraction> UserInteractions { get; set; }
    }
}
