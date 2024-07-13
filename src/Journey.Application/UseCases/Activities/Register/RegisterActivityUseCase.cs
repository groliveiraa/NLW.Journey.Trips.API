using FluentValidation;
using FluentValidation.Results;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson requestActivity)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext
                .Trips
                .FirstOrDefault(a => a.Id == tripId);

            Validate(activity, requestActivity);

            var entidade = new Activity
            {
                Name = requestActivity.Name,
                Date = requestActivity.Date,
                TripId = tripId,
            };

            dbContext.Activities.Add(entidade);

            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Id = entidade.Id,
                Name = entidade.Name,
                Date = entidade.Date,
                Status = (Communication.Enums.ActivityStatus)entidade.Status,
            };
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson requestActivity)
        {
            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            var validator = new RegisterActivityValidator();

            var result = validator.Validate(requestActivity);

            if ((requestActivity.Date >= trip.StartDate && requestActivity.Date <= trip.EndDate) == false)
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