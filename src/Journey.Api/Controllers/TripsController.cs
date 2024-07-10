using Journey.Application.UseCases.Trips.GetAllTrips;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public IActionResult RegisterTrip([FromBody]RequestRegisterTripJson requestTrip)
        {
            try
            {
                var useCase = new RegisterTripUseCase();

                var response = useCase.Execute(requestTrip);

                return Created(string.Empty, response);
            }
            catch (TripsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro Desconhecido");
            }
        }

        [HttpGet]
        public IActionResult GetAllTrip()
        {
            var useCase = new GetAllTripsUseCase();

            var result = useCase.Execute();

            return Ok(result);
        }
    }
}
