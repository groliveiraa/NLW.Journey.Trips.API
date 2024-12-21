using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure;
using Journey.Application.UseCases.Trips.Register;
using Journey.Exception.ExceptionsBase;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.UpdateTrip
{
    public class UpdateTripUseCase
    {
        public ResponseShortTripJson Execute(Guid id, RequestRegisterTripJson requestTrip)
        {
            Validate(requestTrip);

            var dbContext = new JourneyDbContext();

            var trip = dbContext.Trips
                .Include(t => t.Activities)
                .FirstOrDefault(t => t.Id == id);

            trip.Name = requestTrip.Name;
            trip.StartDate = requestTrip.StartDate;
            trip.EndDate = requestTrip.EndDate;

            dbContext.Update(trip);

            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = trip.EndDate,
                StartDate = trip.StartDate,
                Name = trip.Name,
                Id = trip.Id
            };
        }

        private void Validate(RequestRegisterTripJson requestTrip)
        {
            var validator = new RegisterTripsValidator();

            var result = validator.Validate(requestTrip);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErroOnValidationException(errorMessages);
            }
        }
    }
}
