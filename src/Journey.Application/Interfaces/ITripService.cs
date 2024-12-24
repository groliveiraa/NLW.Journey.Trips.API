using Journey.Communication.Requests;
using Journey.Communication.Responses;

namespace Journey.Application.Interfaces
{
    public interface ITripService
    {
        ResponseTripsJson GetAllTrips();
        ResponseTripJson GetTripById(Guid id);
        ResponseShortTripJson RegisterTrip(RequestRegisterTripJson requestTrip);
        ResponseShortTripJson UpdateTrip(Guid id, RequestRegisterTripJson requestTrip);
        void DeleteTrip(Guid id);
    }
}
