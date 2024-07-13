using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Activities.DeleteActivities
{
    public class DeleteActivityUseCase
    {
        public void Execute(Guid tripId, Guid activityId)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext
            .Activities
                .FirstOrDefault(a => a.Id == activityId && a.TripId == tripId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            dbContext.Activities.Remove(activity);

            dbContext.SaveChanges();
        }
    }
}
