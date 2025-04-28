using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Persistence.Interfaces;
using Backend.Model;

namespace Backend.Service
{
    public class AuctionService
    {
        private readonly IAuctionPersistence _auctionPersistence;

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
        }

        public Auction? Save(int carId, int userID, DateTime startDate, int startingPrice, int reserve)
        {
            Auction auction = new Auction
            {
                CarId = carId,
                UserId = userID,
                StartTime = startDate,
                StartingPrice = startingPrice,
                Reserve = reserve
            };
            return _auctionPersistence.Save(auction);
        }

        public List<Auction> FindAll()
        {
            return _auctionPersistence.FindAll();
        }

        public Auction? FindById(long id)
        {
            return _auctionPersistence.FindById(id);
        }

        public List<Auction> FindByCarId(long carId)
        {
            return _auctionPersistence.FindByCarId(carId);
        }

        public List<Auction> FindByUserId(long userId)
        {
            return _auctionPersistence.FindByUserId(userId);
        }

        public List<Auction> FindActiveAuctions()
        {
            return _auctionPersistence.FindActiveAuctions();
        }
    }
}
