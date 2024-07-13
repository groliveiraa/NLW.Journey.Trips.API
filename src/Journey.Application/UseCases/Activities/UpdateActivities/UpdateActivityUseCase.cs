using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.UpdateActivities
{
    public class UpdateActivityUseCase
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

            activity.Status = ActivityStatus.Done;

            dbContext.Activities.Update(activity);

            dbContext.SaveChanges();
        }
    }
}
