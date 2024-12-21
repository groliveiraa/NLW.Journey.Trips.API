using Journey.Application.UseCases.Trips.DeleteIdTrips;
using Journey.Application.UseCases.Trips.GetAllTrips;
using Journey.Application.UseCases.Trips.GetIdTrips;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.UpdateTrip;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
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
            var useCase = new RegisterTripUseCase();

            var response = useCase.Execute(requestTrip);

            return Created(string.Empty, response);
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
            var useCase = new GetAllTripsUseCase();

            var result = useCase.Execute();

            return Ok(result);
        }

        /// <summary>
        /// Obter uma viagem especifica
        /// </summary>
        /// <param name="id">Identificador da viagem</param>
        /// <returns>Informações da viagem</returns>
        [HttpGet]
        [Route("get-trip/{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetIdTrip([FromRoute] Guid id)
        {
            var useCase = new GetIdTripUseCase();

            var result = useCase.Execute(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("update-trip/{id}")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTrip(Guid id, [FromBody] RequestRegisterTripJson requestTrip)
        {
            var useCase = new UpdateTripUseCase();

            var response = useCase.Execute(id, requestTrip);

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
            var useCase = new DeleteIdTripsUseCase();

            useCase.Execute(id);

            return NoContent();
        }
    }
}
