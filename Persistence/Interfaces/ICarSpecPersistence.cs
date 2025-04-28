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
    }
}
