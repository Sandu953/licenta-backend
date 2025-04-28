using Backend.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Service
{
    public class CarSpecService
    {
        private readonly ICarSpecPersistence _carSpecPersistence;

        public CarSpecService(ICarSpecPersistence carSpecPersistence)
        {
            _carSpecPersistence = carSpecPersistence;
        }

        /// <summary>
        /// Saves a car specification to the database.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <param name="year"></param>
        /// <param name="fuel"></param>
        /// <param name="engineSize"></param>
        /// <param name="emissions"></param>
        /// <returns></returns>
        public CarSpec? Save(string make, string model, string trim, int year, string fuel, int engineSize, int emissions)
        {
            CarSpec carSpec = new CarSpec
            {
                Make = make,
                Model = model,
                Trim = trim,
                Year = year,
                FuelType = fuel,
                EngineSize = engineSize,
                Emissions = emissions
            };
            return _carSpecPersistence.Save(carSpec);
        }

        /// <summary>
        /// Function that finds all car specifications in the database.
        /// </summary>
        /// <returns></returns>
        public List<CarSpec> FindAll()
        {
            return _carSpecPersistence.FindAll();
        }

        /// <summary>
        /// Function that finds a car specification by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CarSpec? FindById(long id)
        {
            return _carSpecPersistence.FindById(id);
        }

        /// <summary>
        /// Function that finds a car specification by trim.
        /// </summary>
        /// <param name="trim"></param>
        /// <returns></returns>
        public CarSpec? FindByTrim(string trim)
        {
            return _carSpecPersistence.FindByTrim(trim);
        }

        /// <summary>
        /// Function that finds all car specifications by brand.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public List<CarSpec> GetSpecsByBrand(string brand)
        {
            return _carSpecPersistence.GetSpecsByBrand(brand);
        }

        /// <summary>
        /// Function that finds all car specifications by brand and model.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<CarSpec> GetSpecsByBrandModel(string brand, string model)
        {
            return _carSpecPersistence.GetSpecsByBrandModel(brand, model);
        }
    }
}
