using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Persistence
{
    public class AuctionPersistence : IAuctionPersistence
    {
        private readonly ApplicationDbContext _dbContext;

        public AuctionPersistence(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Auction? Save(Auction auction)
        {
            _dbContext.Auctions.Add(auction);
            _dbContext.SaveChanges();
            return auction;
        }

        public List<Auction> FindAll()
        {
            return _dbContext.Auctions.ToList();
        }

        public Auction? FindById(long id)
        {
            return _dbContext.Auctions.Find(id);
        }

        public List<Auction> FindByCarId(long carId)
        {
            return _dbContext.Auctions.Where(a => a.CarId == carId).ToList();
        }

        public List<Auction> FindByUserId(long userId)
        {
            return _dbContext.Auctions.Where(a => a.UserId == userId).ToList();
        }

        public List<Auction> FindActiveAuctions()
        {
            return _dbContext.Auctions.Where(a => a.StartTime >= DateTime.Now && a.StartTime <= DateTime.Now.AddDays(7)).ToList();
        }

        public Auction? FindLiveAuctionByVIN(string vin)
        {
            var now = DateTime.UtcNow;
            return _dbContext.Auctions
                .Where(a => a.StartTime >= now && a.StartTime <= now.AddDays(7)) // Filter live auctions
                .Join(
                    _dbContext.Cars, // Join with Cars table
                    auction => auction.CarId, // Match Auction.CarId with Car.Id
                    car => car.Id,
                    (auction, car) => new { Auction = auction, Car = car }
                )
                .Where(ac => ac.Car.Vin == vin) // Filter by VIN
                .Select(ac => ac.Auction) // Select the Auction
                .FirstOrDefault(); // Return the first matching auction or null
        }
    }
}
