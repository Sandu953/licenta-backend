using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using System.Diagnostics.Metrics;

namespace Backend.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarSpec> CarSpecs { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=car_auctions;Username=postgres;Password=parola");
        }
    }
}
