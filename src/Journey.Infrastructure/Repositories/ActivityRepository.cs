using Journey.Domain.Entities;
using Journey.Domain.Interfaces.Repositories;

namespace Journey.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly JourneyDbContext _dbContext;

        public ActivityRepository(JourneyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Activity> GetAll()
        {
            return _dbContext.Activities.ToList();
        }

        public Activity GetById(Guid id)
        {
            return _dbContext.Activities
                .FirstOrDefault(a => a.Id == id);
        }

        public Activity GetActivityByTripAndId(Guid tripId, Guid activityId)
        {
            return _dbContext.Activities
                .FirstOrDefault(a => a.Id == activityId && a.TripId == tripId);
        }

        public void Add(Activity activity)
        {
            _dbContext.Activities.Add(activity);
            _dbContext.SaveChanges();
        }

        public void Update(Activity activity)
        {
            _dbContext.Activities.Update(activity);
            _dbContext.SaveChanges();
        }

        public void Delete(Activity activity)
        {
            _dbContext.Activities.Remove(activity);
            _dbContext.SaveChanges();
        }
    }
}
