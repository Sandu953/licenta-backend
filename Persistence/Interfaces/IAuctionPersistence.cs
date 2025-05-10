using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence.Interfaces
{
    public interface IAuctionPersistence
    {
        public Auction? Save(Auction auction);
        public List<Auction> FindAll();
        public Auction? FindById(long id);
        public List<Auction> FindByCarId(long carId);
        public List<Auction> FindByUserId(long userId);
        public List<Auction> FindActiveAuctions();
        public Auction? FindLiveAuctionByVIN(string vin);
    }
}
