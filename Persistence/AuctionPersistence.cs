using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Controller;
using Backend.Model;
using Backend.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

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


        /// <summary>
        /// dont forget to add the favorites in the future
        /// </summary>
        /// <param name="auctionFilter"></param>
        /// <returns></returns>
        public List<AuctionPreviewSendDto> Filter(AuctionFilterDto filter)
        {
            var query = _dbContext.Auctions
                .Include(a => a.User)
                .Include(a => a.Car)
                .ThenInclude(c => c.CarSpec)
                .Include(a => a.Car.CarImages)
                .AsQueryable();

            // Status: active vs inactive
            if (filter.Status == "active")
                query = query.Where(a => a.StartTime <= DateTime.UtcNow && a.StartTime.AddDays(7) >= DateTime.UtcNow);
            else if (filter.Status == "inactive")
                query = query.Where(a => a.StartTime.AddDays(7) < DateTime.UtcNow);

            // Filters
            if (!string.IsNullOrEmpty(filter.Make))
                query = query.Where(a => a.Car.CarSpec.Make.Contains(filter.Make));

            if (!string.IsNullOrEmpty(filter.Model))
                query = query.Where(a => a.Car.CarSpec.Model.Contains(filter.Model));

            if (filter.YearMin.HasValue)
                query = query.Where(a => a.Car.CarSpec.Year >= filter.YearMin);

            if (filter.YearMax.HasValue)
                query = query.Where(a => a.Car.CarSpec.Year <= filter.YearMax);

            if (filter.HpMin.HasValue)
                query = query.Where(a => a.Car.HP >= filter.HpMin); // asumi câmpul există

            if (filter.HpMax.HasValue)
                query = query.Where(a => a.Car.HP <= filter.HpMax);

            if (filter.EngineSizeMin.HasValue)
                query = query.Where(a => a.Car.CarSpec.EngineSize >= filter.EngineSizeMin);

            if (filter.EngineSizeMax.HasValue)
                query = query.Where(a => a.Car.CarSpec.EngineSize <= filter.EngineSizeMax);

            if (filter.PriceMin.HasValue)
                query = query.Where(a => a.StartingPrice >= filter.PriceMin); // sau CurrentBid dacă există

            if (filter.PriceMax.HasValue)
                query = query.Where(a => a.StartingPrice <= filter.PriceMax);

            if (filter.MileageMin.HasValue)
                query = query.Where(a => a.Car.Km >= filter.MileageMin);

            if (filter.MileageMax.HasValue)
                query = query.Where(a => a.Car.Km <= filter.MileageMax);

            if (!string.IsNullOrEmpty(filter.Fuel))
                query = query.Where(a => a.Car.CarSpec.FuelType.Contains(filter.Fuel));

            if (!string.IsNullOrEmpty(filter.BodyType))
                query = query.Where(a => a.Car.BodyType.Contains(filter.BodyType));

            query = query.OrderBy(a => a.Id);

            if (filter.Cursor.HasValue)
            {
                query = query.Where(a => a.Id > filter.Cursor.Value);
            }
            else if (filter.Page.HasValue && filter.Page.Value > 0)
            {
                var skip = (filter.Page.Value - 1) * filter.PageSize;
                query = query.Skip(skip);
            }



            var auctions = query
                .Take(filter.PageSize)
                .ToList() // ← Materialize into memory so EF doesn't need to translate it
                .Select(a => new AuctionPreviewSendDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Make = a.Car.CarSpec.Make,
                    Model = a.Car.CarSpec.Model,
                    Year = a.Car.CarSpec.Year,
                    Location = a.Car.Location,
                    Mileage = a.Car.Km,
                    StartTime = a.StartTime,
                    CurrentBid = a.Bids != null && a.Bids.Any()
                        ? a.Bids.Max(b => b.Amount)
                        : a.StartingPrice,
                    ImageUrl = a.Car.CarImages.FirstOrDefault()?.ImageUrl,
                    IsFavorite = false,
                    OwnerUserName = a.User.Username,
                    OwnerPfp = a.User.ProfilePicture
                })
                .ToList();
            return auctions;
        }
    }
}
