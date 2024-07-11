using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson requestTrip)
        {
            Validate(requestTrip);

            var dbContext = new JourneyDbContext();

            var entidade = new Trip
            {
                Name = requestTrip.Name,
                StartDate = requestTrip.StartDate,
                EndDate = requestTrip.EndDate
            };

            dbContext.Add(entidade);
                
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entidade.EndDate,
                StartDate = entidade.StartDate,
                Name = entidade.Name,
                Id = entidade.Id
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
