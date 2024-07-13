using Journey.Application.UseCases.Activities.DeleteActivities;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Activities.UpdateActivities;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        [HttpPost]
        [Route("{tripId}/adicionar-activity")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity([FromRoute] Guid tripId ,[FromBody] RequestRegisterActivityJson requestActivity)
        {
            var useCase = new RegisterActivityUseCase();

            var response = useCase.Execute(tripId, requestActivity);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{tripId}/atualizar-activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            var useCase = new UpdateActivityUseCase();

            useCase.Execute(tripId, activityId);

            return NoContent();
        }

        [HttpDelete]
        [Route("{tripId}/deletar-activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            var useCase = new DeleteActivityUseCase();

            useCase.Execute(tripId, activityId);

            return NoContent();
        }
    }
}