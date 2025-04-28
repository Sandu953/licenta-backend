using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Service
{
    internal class CarService
    {
        private readonly ICarPersistence _carPersistence;

        public CarService(ICarPersistence carPersistence)
        {
            _carPersistence = carPersistence;
        }

        public Car? Save(int specId, string vin, int km, string location, string description)
        {
            Car car = new Car
            {
                SpecId = specId,
                Vin = vin,
                Km = km,
                Location = location,
                Description = description
            };
            return _carPersistence.Save(car);
        }
    }
}
