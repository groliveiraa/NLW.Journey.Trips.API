using Journey.Application.Interfaces;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        /// <summary>
        /// Obter todas as atividades programadas
        /// </summary>
        /// <returns>Coleção de atividades programadas</returns>
        [HttpGet]
        [Route("all-activities")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAllActivity()
        {
            var result = _activityService.GetAllActivities();

            return Ok(result);
        }

        /// <summary>
        /// Obter uma atividade especifica
        /// </summary>
        /// <param name="id">Identificador da atividade</param>
        /// <returns>Informações da atividade</returns>
        [HttpGet]
        [Route("get-activity/{id}")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetIdActivity([FromRoute] Guid id)
        {
            var result = _activityService.GetActivityById(id);

            return Ok(result);
        }

        /// <summary>
        /// Cadastrar uma atividade
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="requestActivity">Informações da atividade</param>
        /// <returns>Atividade cadastrada</returns>
        [HttpPost]
        [Route("{tripId}/create-activity")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson requestActivity)
        {
            var response = _activityService.RegisterActivity(tripId, requestActivity);

            return Created(string.Empty, response);
        }

        /// <summary>
        /// Atualizar o status de uma atividade especifica
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="activityId">Identificador da atividade</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{tripId}/update-activity-status/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            _activityService.UpdateActivityStatus(tripId, activityId);

            return NoContent();
        }

        /// <summary>
        /// Deletar uma atividade especifica
        /// </summary>
        /// <param name="tripId">Identificador da viagem</param>
        /// <param name="activityId">Identificador da atividade</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{tripId}/delete-activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            _activityService.DeleteActivity(tripId, activityId);

            return NoContent();
        }
    }
}