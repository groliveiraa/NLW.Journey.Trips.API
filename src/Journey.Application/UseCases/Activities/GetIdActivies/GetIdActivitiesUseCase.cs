using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Activities.GetIdActivies
{
    public class GetIdActivitiesUseCase
    {
        public ResponseActivityJson Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext.Activities
                .FirstOrDefault(t => t.Id == id);

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
    }
}

