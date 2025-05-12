using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence.Interfaces
{
    public interface ICarImagePersistence
    {
        public CarImage? Save(CarImage carImage);
        public List<CarImage> FindAll();
        public CarImage? FindById(int id);
        public List<CarImage> FindByCarId(int carId);


    }
}
