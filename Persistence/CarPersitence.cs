using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence
{
    public class CarPersitence: ICarPersistence
    {
        private ApplicationDbContext _dbContext;

        public CarPersitence(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Car? Save(Car car)
        {
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return car;
        }
        public List<Car> FindAll()
        {
            var cars = _dbContext.Cars.ToList();
            return cars;
        }
        public Car? FindById(long id)
        {
            var car = _dbContext.Cars.Find(id);
            return car;
        }

    }
}
