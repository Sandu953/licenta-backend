using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Persistence
{
    public class CarPersistence: ICarPersistence
    {
        private readonly ApplicationDbContext _dbContext;

        public CarPersistence(ApplicationDbContext dbContext)
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
            return _dbContext.Cars.ToList();
        }

        public Car? FindById(long id)
        {
            return _dbContext.Cars.Find(id);
        }

    }
}
