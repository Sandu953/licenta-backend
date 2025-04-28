using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Persistence.Interfaces;

namespace Backend.Persistence
{
    public class UserInteractionPersistence : IUserInteractionPersistence
    {
        private readonly ApplicationDbContext _dbContext;

        public UserInteractionPersistence(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserInteraction? Save(UserInteraction userInteraction)
        {
            _dbContext.UserInteractions.Add(userInteraction);
            _dbContext.SaveChanges();
            return userInteraction;
        }

        public List<UserInteraction> FindAll()
        {
            return _dbContext.UserInteractions.ToList();
        }

        public UserInteraction? FindById(long id)
        {
            return _dbContext.UserInteractions.Find(id);
        }

        public List<UserInteraction> FindByUserId(long userId)
        {
            return _dbContext.UserInteractions.Where(ui => ui.UserId == userId).ToList();
        }

        public List<UserInteraction> FindByCarId(long carId)
        {
            return _dbContext.UserInteractions.Where(ui => ui.CarId == carId).ToList();
        }
    }
}
