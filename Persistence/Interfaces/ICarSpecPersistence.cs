using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence.Interfaces
{
    public interface ICarSpecPersistence
    {
        public CarSpec? Save(CarSpec CarSpec);
        public List<CarSpec> FindAll();
        public CarSpec? FindById(long id);
        public CarSpec? FindByTrim(string trim);
        public List<CarSpec> GetSpecsByBrand(string brand);
        public List<CarSpec> GetSpecsByBrandModel(string brand, string model);
        public List<string> GetAllBrands();
        public List<string> GetAllModelsByBrand(string brand);
        public List<int> GetAllYearsByBrandModel(string brand, string model);
        public List<int> GetAllEngineSizesByBrandModel(string brand, string model);
        public List<string> GetAllFuelTypeByBrandModelEngineSize(string brand, string model, int engineSize);
        public List<CarSpec> GetAllTrimsByBrandModelEngineSizeFuelType(string brand, string model, int engineSize, string fuel);
        //public List<string> GetAllTrimsByBrandModelYear(string brand, string model, int year);
    }
}
