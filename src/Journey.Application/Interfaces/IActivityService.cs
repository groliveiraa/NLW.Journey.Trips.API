using Journey.Communication.Requests;
using Journey.Communication.Responses;

namespace Journey.Application.Interfaces
{
    public interface IActivityService
    {
        ResponseActivitiesJson GetAllActivities();
        ResponseActivityJson GetActivityById(Guid id);
        ResponseActivityJson RegisterActivity(Guid tripId, RequestRegisterActivityJson requestActivity);
        void UpdateActivityStatus(Guid tripId, Guid activityId);
        void DeleteActivity(Guid tripId, Guid activityId);
    }
}
