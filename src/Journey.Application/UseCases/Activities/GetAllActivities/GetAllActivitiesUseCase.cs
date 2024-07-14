using Journey.Communication.Responses;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Activities.GetAllActivities
{
    public class GetAllActivitiesUseCase
    {
        public ResponseActivitiesJson Execute()
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext.Activities.ToList();

            return new ResponseActivitiesJson
            {
                Activities = activity.Select(t => new ResponseActivityJson
                {
                    Id = t.Id,
                    Name = t.Name,
                    Date = t.Date
                }).ToList()
            };
        }
    }
}
