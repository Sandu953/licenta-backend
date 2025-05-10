using Backend.Model;
using Backend.Persistence;
using Backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;


namespace Backend.Data
{
    public class CarSpecCsvModel
    {
        public string Genmodel_ID { get; set; }
        public string Maker { get; set; }
        public string Genmodel { get; set; }
        public string Trim { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public int Gas_emission { get; set; }
        public string Fuel_type { get; set; }
        public int Engine_size { get; set; }
    }

    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly CarSpecService _carSpecService;
        private readonly AuctionService _auctionService;

        public DataSeeder(ApplicationDbContext context, UserService userService, CarSpecService carSpecService, AuctionService auctionService)
        {
            _context = context;
            _userService = userService;
            _carSpecService = carSpecService;
            _auctionService = auctionService;
        }

        private string GenerateRandomVin(Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 17)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string RandomLocation(Random random)
        {
            string[] locations = { "Bucharest", "Cluj", "Timisoara", "Iasi", "Constanta", "Brasov", "Oradea", "Sibiu" };
            return locations[random.Next(locations.Length)];
        }


        public void TestCarSpecs()
        {
            //test getBrands, getModelsByBrand, getYaarssByBrandModel, getTrimsByBrandModelYear
            var brands = _carSpecService.GetAllBrands();
            var models = _carSpecService.GetAllModelsByBrand("Bmw");
            var years = _carSpecService.GetAllYearsByBrandModel("Bmw", "3 series");
            //var engines = _carSpecService.GetAllEngineSizesByBrandModelYear("Bmw", "3 series", 2010);
            //var fuel = _carSpecService.GetAllFuelTypeByBrandModelYearEngineSize("Bmw", "3 series", 2010, 1995);
            //var trims = _carSpecService.GetAllTrimsByBrandModelYearEngineSizeFuelType("Bmw", "3 series", 2010, 1995, "Diesel");


            //Console.WriteLine("Brands: " + string.Join(", ", brands));
            //Console.WriteLine("Models: " + string.Join(", ", models));
            //Console.WriteLine("Years: " + string.Join(", ", years));
            ////Console.WriteLine("Engines: " + string.Join(", ", engines));
            //Console.WriteLine("Fuel: " + string.Join(", ", fuel));
            //foreach(CarSpec carSpec in trims)
            //{
            //    Console.WriteLine("CarSpec: " + carSpec.Trim);
            //}


        }


        public void Seed()
        {
            // Seed users
            if (!_context.Users.Any())
            {
                _userService.Save("user1@test.com", "pass", "User1");
                _userService.Save("user2@test.com", "pass", "User2");
            }

            _context.SaveChanges();
        }

        public void SeedCarSpecs()
        {
            if (!_context.CarSpecs.Any())
            { 
                using (var reader = new StreamReader("C:\\Users\\alexp\\Licenta\\Backend\\Trim_table.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CarSpecCsvModel>().ToList();

                    var batchSize = 5000; // 🚀 Tune this if needed
                    var batch = new List<CarSpec>(batchSize);

                    foreach (var record in records)
                    {

                        var carSpec = new CarSpec
                        {
                            Make = record.Maker,
                            Model = record.Genmodel,
                            Trim = record.Trim,
                            Year = record.Year,
                            FuelType = record.Fuel_type,
                            EngineSize = record.Engine_size,
                            Emissions = record.Gas_emission
                        };
                        batch.Add(carSpec);

                        if (batch.Count >= batchSize)
                        {
                            _context.CarSpecs.AddRange(batch);
                            _context.SaveChanges();
                            batch.Clear(); // 🧹 Clear memory
                        }
                    }
                    // Save any remaining records
                    if (batch.Any())
                    {
                        _context.CarSpecs.AddRange(batch);
                        _context.SaveChanges();
                    }

                }
            }           
        }

        public void SeedCars()
        {
            if (!_context.Cars.Any())
            {
                var random = new Random();
                //var carSpecs = _context.CarSpecs.Take(100).ToList(); // Take 100 specs for now, you can adjust
                var carSpecs = _context.CarSpecs
                    .OrderBy(c => Guid.NewGuid()) // random order
                    .Take(100)                    // pick 100 random specs
                    .ToList();

                var carsToInsert = new List<Car>();

                foreach (var spec in carSpecs)
                {
                    var numberOfCars = random.Next(1, 4); // 1 to 3 cars per spec

                    for (int i = 0; i < numberOfCars; i++)
                    {
                        var car = new Car
                        {
                            SpecId = spec.Id,
                            Vin = GenerateRandomVin(random),
                            Km = random.Next(0, 250_000),  // Random mileage
                            Location = RandomLocation(random),
                            Description = $"A nice {spec.Make} {spec.Model} {spec.Trim} from {spec.Year}."
                        };
                        carsToInsert.Add(car);
                    }
                }

                _context.Cars.AddRange(carsToInsert);
                _context.SaveChanges();
            }
        }

        public void SeedAuctions()
        {
            if (!_context.Auctions.Any())
            {
                var random = new Random();

                var cars = _context.Cars.ToList();
                var users = _context.Users.ToList(); // you need users already seeded or insert one dummy

                var auctionsToInsert = new List<Auction>();

                foreach (var car in cars)
                {
                    var seller = users[random.Next(users.Count)];

                    var startTime = DateTime.UtcNow.AddDays(-random.Next(0, 5)); // between today and 30 days ago
                    var endTime = startTime.AddDays(7); // 7 days auction

                    var startingPrice = random.Next(2000, 30000); // random starting price
                    var reserve = startingPrice + random.Next(500, 5000); // reserve price higher

                    var auction = new Auction
                    {
                        CarId = car.Id,
                        UserId = seller.Id,
                        StartTime = startTime,
                        StartingPrice = startingPrice,
                        Reserve = reserve,
                    };

                    auctionsToInsert.Add(auction);
                }

                _context.Auctions.AddRange(auctionsToInsert);
                _context.SaveChanges();
            }
        }

        public void SeedUserInteractions()
        {
            if (!_context.UserInteractions.Any())
            {
                var random = new Random();

                var users = _context.Users.ToList();
                var cars = _context.Cars.ToList();

                var interactionsToInsert = new List<UserInteraction>();

                foreach (var user in users)
                {
                    var numberOfInteractions = random.Next(20, 31); // 20-30 interactions per user

                    var carsInteracted = cars
                        .OrderBy(c => Guid.NewGuid())
                        .Take(numberOfInteractions)
                        .ToList();

                    foreach (var car in carsInteracted)
                    {
                        int score;

                        // 35-40% chance of being a 0 (dislike)
                        if (random.NextDouble() < 0.375) // 37.5% chance
                        {
                            score = 0;
                        }
                        else
                        {
                            score = 1;
                        }

                        var interaction = new UserInteraction
                        {
                            UserId = user.Id,
                            CarId = car.Id,
                            Score = score,
                            InteractedAt = DateTime.UtcNow.AddDays(-random.Next(0, 3)) // Last 90 days
                        };

                        interactionsToInsert.Add(interaction);
                    }
                }

                _context.UserInteractions.AddRange(interactionsToInsert);
                _context.SaveChanges();
            }
        }


    }
}
