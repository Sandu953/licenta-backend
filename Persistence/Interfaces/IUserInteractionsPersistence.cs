using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.Persistence.Interfaces
{
    public interface IUserInteractionPersistence
    {
        public UserInteraction? Save(UserInteraction UserInteraction);
        public List<UserInteraction> FindAll();
        public UserInteraction? FindById(long id);
        public List<UserInteraction> FindByUserId(long userId);
        public List<UserInteraction> FindByCarId(long carId);
    }
}
