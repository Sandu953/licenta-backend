using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Persistence.Interfaces;
using Backend.Model;

namespace Backend.Persistence
{
    public class CarImagePersitence : ICarImagePersistence
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CarImagePersitence(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public CarImage? Save(CarImage carImage)
        {
            applicationDbContext.CarImages.Add(carImage);
            applicationDbContext.SaveChanges();
            return carImage;

        }
        public List<CarImage> FindAll()
        {
            return applicationDbContext.CarImages.ToList();
        }
        public Model.CarImage? FindById(int id)
        {
            return applicationDbContext.CarImages.Find(id);
        }
        public List<CarImage> FindByCarId(int carId)
        {
            return applicationDbContext.CarImages.Where(c => c.CarId == carId).ToList();
        }
    }
}
