using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Seller
        public User User { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public int StartingPrice { get; set; }
        public int Reserve { get; set; }

        public ICollection<Bid> Bids { get; set; }
        public ICollection<Comment> AuctionComments { get; set; }
    }
}
