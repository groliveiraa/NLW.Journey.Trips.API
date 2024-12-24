using Journey.Application.Interfaces;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        /// <summary>
        /// Obter todas as viagens
        /// </summary>
        /// <returns>Coleção de viagens</returns>
        [HttpGet]
        [Route("all-trips")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAllTrip()
        {
            var result = _tripService.GetAllTrips();

            return Ok(result);
        }

        /// <summary>
        /// Obtém uma viagem pelo ID.
        /// </summary>
        /// <param name="id">ID da viagem.</param>
        /// <returns>Detalhes da viagem.</returns>
        [HttpGet]
        [Route("get-trip/{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetIdTrip([FromRoute] Guid id)
        {
            var result = _tripService.GetTripById(id);

            return Ok(result);
        }

        /// <summary>
        /// Cadastrar uma viagem
        /// </summary>
        /// <param name="requestTrip">Informações da viagem</param>
        /// <returns>Viagem cadastrada</returns>
        [HttpPost]
        [Route("create-trip")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult RegisterTrip([FromBody] RequestRegisterTripJson requestTrip)
        {
            var response = _tripService.RegisterTrip(requestTrip);

            return Created(string.Empty, response);
        }

        /// <summary>
        /// Atualiza os dados de uma viagem.
        /// </summary>
        /// <param name="id">ID da viagem.</param>
        /// <param name="requestTrip">Dados para atualização.</param>
        /// <returns>Dados atualizados da viagem.</returns>
        [HttpPut]
        [Route("update-trip/{id}")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTrip(Guid id, [FromBody] RequestRegisterTripJson requestTrip)
        {
            var response = _tripService.UpdateTrip(id, requestTrip);

            return Ok(response);
        }

        /// <summary>
        /// Deletar uma viagem especifica
        /// </summary>
        /// <param name="id">Identificador da viagem</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-trip/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult DeleteIdTrip([FromRoute] Guid id)
        {
            _tripService.DeleteTrip(id);

            return NoContent();
        }
    }
}
