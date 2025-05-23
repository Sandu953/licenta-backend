﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {                
        [Key]        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfilePicture { get; set; } // URL to the profile picture

        public ICollection<Bid> Bids { get; set; }
        public ICollection<Comment> AuctionComments { get; set; }
        public ICollection<Auction> Auctions { get; set; } // Seller's auctions
        public ICollection<UserInteraction> UserInteractions { get; set; }
    }
}
