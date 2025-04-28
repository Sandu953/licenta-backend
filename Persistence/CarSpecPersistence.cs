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
    }
}
