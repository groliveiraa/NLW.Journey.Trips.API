using Journey.Domain.Entities;

namespace Journey.Domain.Interfaces.Repositories
{
    public interface IActivityRepository
    {
        List<Activity> GetAll();
        Activity GetById(Guid id);
        void Add(Activity activity);
        Activity GetActivityByTripAndId(Guid tripId, Guid activityId);
        void Update(Activity activity);
        void Delete(Activity activity);
    }
}
