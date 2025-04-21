using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence
{
    public interface ICarPersistence
    {
        public Car? Save(Car car);
        public List<Car> FindAll();
        public Car? FindById(long id);
    }
}
