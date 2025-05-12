using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Service
{
    public class UserInteractionService
    {
        private readonly IUserInteractionPersistence _userInteractionPersistence;

        public UserInteractionService(IUserInteractionPersistence userInteractionPersistence)
        {
            _userInteractionPersistence = userInteractionPersistence;
        }

        public UserInteraction? Save(int userId, int carId, int interactionScore)
        {
            UserInteraction userInteraction = new UserInteraction
            {
                UserId = userId,
                CarId = carId,
                Score = interactionScore,
                InteractedAt = DateTime.UtcNow
            };
            return _userInteractionPersistence.Save(userInteraction);
        }

        public List<UserInteraction> FindAll()
        {
            return _userInteractionPersistence.FindAll();
        }

        public UserInteraction? FindById(long id)
        {
            return _userInteractionPersistence.FindById(id);
        }

        public List<UserInteraction> FindByUserId(long userId)
        {
            return _userInteractionPersistence.FindByUserId(userId);
        }

        public List<UserInteraction> FindByCarId(long carId)
        {
            return _userInteractionPersistence.FindByCarId(carId);
        }
    }
}
