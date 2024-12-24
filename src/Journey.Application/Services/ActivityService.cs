using Journey.Application.Interfaces;
using Journey.Communication.Responses;
using Journey.Domain.Interfaces.Repositories;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Domain.Enums;
using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Domain.Entities;
using Journey.Domain;
using Journey.Application.Validators;

namespace Journey.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IActivityRepository _activityRepository;

        public ActivityService(ITripRepository tripRepository, IActivityRepository activityRepository)
        {
            _tripRepository = tripRepository;
            _activityRepository = activityRepository;
        }

        public ResponseActivitiesJson GetAllActivities()
        {
            var activities = _activityRepository.GetAll();

            return new ResponseActivitiesJson
            {
                Activities = activities.Select(a => new ResponseActivityJson
                {
                    Id = a.Id,
                    Name = a.Name,
                    Date = a.Date
                }).ToList()
            };
        }

        public ResponseActivityJson GetActivityById(Guid id)
        {
            var activity = _activityRepository.GetById(id);

            if (activity == null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            return new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date
            };
        }

        public ResponseActivityJson RegisterActivity(Guid tripId, RequestRegisterActivityJson requestActivity)
        {
            var trip = _tripRepository.GetById(tripId);

            Validate(trip, requestActivity);

            var activity = new Activity
            {
                Name = requestActivity.Name,
                Date = requestActivity.Date,
                TripId = tripId,
            };

            _activityRepository.Add(activity);

            return new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status,
            };
        }

        public void UpdateActivityStatus(Guid tripId, Guid activityId)
        {
            var activity = _activityRepository.GetActivityByTripAndId(tripId, activityId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            activity.Status = ActivityStatus.Done;

            _activityRepository.Update(activity);
        }

        public void DeleteActivity(Guid tripId, Guid activityId)
        {
            var activity = _activityRepository.GetActivityByTripAndId(tripId, activityId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            _activityRepository.Delete(activity);
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson requestActivity)
        {
            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            var validator = new RegisterActivityValidator();

            var result = validator.Validate(requestActivity);

            if (requestActivity.Date < trip.StartDate || requestActivity.Date > trip.EndDate)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATA_FORA_PERIODO_VIAGEM));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErroOnValidationException(errorMessages);
            }
        }
    }
}
