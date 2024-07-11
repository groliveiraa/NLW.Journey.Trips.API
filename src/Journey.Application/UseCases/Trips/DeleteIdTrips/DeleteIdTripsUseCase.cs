using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.DeleteIdTrips
{
    public class DeleteIdTripsUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();

            var trip = dbContext.Trips
                .Include(t => t.Activities)
                .FirstOrDefault(t => t.Id == id);

            if (trip == null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            dbContext.Remove(trip);

            dbContext.SaveChanges();
        }
    }
}
