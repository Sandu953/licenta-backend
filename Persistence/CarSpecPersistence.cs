using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Persistence
{
    public class CarSpecPersistence : ICarSpecPersistence
    {
        private readonly ApplicationDbContext _dbContext;

        public CarSpecPersistence(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Saves a CarSpec to the database.
        /// </summary>
        /// <param name="CarSpec"></param>
        /// <returns></returns>
        public CarSpec? Save(CarSpec CarSpec)
        {
            _dbContext.CarSpecs.Add(CarSpec);
            _dbContext.SaveChanges();
            return CarSpec;
        }

        /// <summary>
        /// Finds all CarSpecs in the database.
        /// </summary>
        /// <returns></returns>
        public List<CarSpec> FindAll()
        {
            return _dbContext.CarSpecs.ToList();
        }

        /// <summary>
        /// Finds a CarSpec by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CarSpec? FindById(long id)
        {
            return _dbContext.CarSpecs.Find(id);
        }

        /// <summary>
        /// Finds a CarSpec by trim.
        /// </summary>
        /// <param name="trim"></param>
        /// <returns></returns>
        public CarSpec? FindByTrim(string trim)
        {
            return _dbContext.CarSpecs.FirstOrDefault(c => c.Trim == trim);
        }

        /// <summary>
        /// Finds all CarSpecs by brand.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public List<CarSpec> GetSpecsByBrand(string brand)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand)
                .ToList();
        }

        /// <summary>
        /// Finds all CarSpecs by brand and model.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<CarSpec> GetSpecsByBrandModel(string brand, string model)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand && c.Model == model)
                .ToList();
        }

        /// <summary>
        /// Finds all car brands in the database.
        /// </summary>
        /// <returns></returns>
        List<string> ICarSpecPersistence.GetAllBrands()
        {
            return _dbContext.CarSpecs
                .Select(c => c.Make)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Finds all car models by brand.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> GetAllModelsByBrand(string brand)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand)
                .Select(c => c.Model)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Finds all car trims by brand and model.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<int> GetAllYearsByBrandModel(string brand, string model)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand && c.Model == model)
                .Select(c => c.Year)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Finds all car engine sizes by brand, model, and year.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<int> GetAllEngineSizesByBrandModel(string brand, string model)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand && c.Model == model)
                .Select(c => c.EngineSize)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Finds all car HP by brand, model, year, and engine size.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="engineSize"></param>
        /// <returns></returns>
        public List<string> GetAllFuelTypeByBrandModelEngineSize(string brand, string model, int engineSize)
        {
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand && c.Model == model && c.EngineSize == engineSize)
                .Select(c => c.FuelType)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Finds all car trims by brand, model, year, engine size, and HP.
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="engineSize"></param>
        /// <param name="hp"></param>
        /// <returns></returns>
        public List<CarSpec> GetAllTrimsByBrandModelEngineSizeFuelType(string brand, string model, int engineSize, string fuel)
        {

            //return _dbContext.CarSpecs
            //    .Where(c => c.Make == brand && c.Model == model && c.EngineSize == engineSize && c.FuelType == fuel)
            //    .DistinctBy(c => c.Trim)
            //    .ToList();
            return _dbContext.CarSpecs
                .Where(c => c.Make == brand && c.Model == model && c.EngineSize == engineSize && c.FuelType == fuel)
                .GroupBy(c => c.Trim)
                .Select(g => g.First())
                .ToList();
        }
    }
}
