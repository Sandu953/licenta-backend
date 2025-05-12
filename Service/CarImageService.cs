using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;


namespace Backend.Service
{
    public class CarImageService
    {
        private readonly ICarImagePersistence _carImagePersistence;

        public CarImageService(ICarImagePersistence carImagePersistence)
        {
            _carImagePersistence = carImagePersistence;
        }

        public CarImage? Save(int carId, string imageUrl)
        {
            CarImage carImage = new CarImage
            {
                CarId = carId,
                ImageUrl = imageUrl,
                UploadedAt = DateTime.UtcNow
            };
            return _carImagePersistence.Save(carImage);
        }

        public List<CarImage> FindAll()
        {
            return _carImagePersistence.FindAll();
        }

        public CarImage? FindById(int id)
        {
            return _carImagePersistence.FindById(id);
        }

        public List<CarImage> FindByCarId(int carId)
        {
            return _carImagePersistence.FindByCarId(carId);
        }
    }
}
