using Journey.Application.UseCases.Activities.DeleteActivities;
using Journey.Application.UseCases.Activities.GetAllActivities;
using Journey.Application.UseCases.Activities.GetIdActivies;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Activities.UpdateActivities;
using Journey.Application.UseCases.Trips.GetAllTrips;
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
        /// <summary>
        /// Cadastrar uma atividade
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="requestActivity">Informações da atividade</param>
        /// <returns>Atividade cadastrada</returns>
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

        /// <summary>
        /// Obter todas as atividades programadas
        /// </summary>
        /// <returns>Coleção de atividades programadas</returns>
        [HttpGet]
        [Route("consulta-activities")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAllActivity()
        {
            var useCase = new GetAllActivitiesUseCase();

            var result = useCase.Execute();

            return Ok(result);
        }

        /// <summary>
        /// Obter uma atividade especifica
        /// </summary>
        /// <param name="id">Identificador da atividade</param>
        /// <returns>Informações da atividade</returns>
        [HttpGet]
        [Route("consulta-activity/{id}")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetIdActivity([FromRoute] Guid id)
        {
            var useCase = new GetIdActivitiesUseCase();

            var result = useCase.Execute(id);

            return Ok(result);
        }

        /// <summary>
        /// Atualizar uma atividade especifica
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="activityId">Identificador da atividade</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletar uma atividade especifica
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="activityId">Identificador da atividade</param>
        /// <returns></returns>
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