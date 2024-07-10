using Journey.Infrastructure.Entities;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journey.Communication.Requests;
using Journey.Communication.Responses;

namespace Journey.Application.UseCases.Trips.GetAllTrips
{
    public class GetAllTripsUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbContext = new JourneyDbContext();

            var trips = dbContext.Trips.ToList();

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
    }
}
