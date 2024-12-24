using Journey.Application.Interfaces;
using Journey.Communication.Responses;
using Journey.Domain;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Communication.Requests;
using Journey.Domain.Entities;
using Journey.Application.Validators;

namespace Journey.Application.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public ResponseTripsJson GetAllTrips()
        {
            var trips = _tripRepository.GetAll();

            return new ResponseTripsJson
            {
                Trips = trips.Select(t => new ResponseShortTripJson
                {
                    Id = t.Id,
                    EndDate = t.EndDate,
                    Name = t.Name,
                    StartDate = t.StartDate
                }).ToList()
            };
        }

        public ResponseTripJson GetTripById(Guid id)
        {
            var trip = _tripRepository.GetById(id);

            if (trip == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            return new ResponseTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(a => new ResponseActivityJson
                {
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.Date,
                    Status = (Communication.Enums.ActivityStatus)a.Status
                }).ToList()
            };
        }

        public ResponseShortTripJson RegisterTrip(RequestRegisterTripJson requestTrip)
        {
            Validate(requestTrip);

            var trip = new Trip
            {
                Name = requestTrip.Name,
                StartDate = requestTrip.StartDate,
                EndDate = requestTrip.EndDate
            };

            _tripRepository.Add(trip);

            return new ResponseShortTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate
            };
        }

        public ResponseShortTripJson UpdateTrip(Guid id, RequestRegisterTripJson requestTrip)
        {
            Validate(requestTrip);

            var trip = _tripRepository.GetById(id);
            
            trip.Name = requestTrip.Name;
            trip.StartDate = requestTrip.StartDate;
            trip.EndDate = requestTrip.EndDate;

            _tripRepository.Update(trip);

            return new ResponseShortTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate
            };
        }

        public void DeleteTrip(Guid id)
        {
            // Busca a viagem pelo ID
            var trip = _tripRepository.GetById(id);
            if (trip == null)
            {
                throw new NotFoundException("Viagem não encontrada.");
            }

            // Remove a viagem
            _tripRepository.Delete(trip);
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
