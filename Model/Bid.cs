using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Auction")]
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
