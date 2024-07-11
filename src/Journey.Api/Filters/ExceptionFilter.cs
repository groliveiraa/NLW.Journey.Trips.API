using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var tripsException = (TripsException)context.Exception;

            if (context.Exception is TripsException)
            {
                context.HttpContext.Response.StatusCode = (int)tripsException.GetStatusCode();

                var responseJson = new ResponseErrorJson(tripsException.GetErrorMessages());

                context.Result = new ObjectResult(responseJson);
            }           
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrorJson(new List<string> { ResourceErrorMessages.ERRO_DESCONHECIDO });

                context.Result = new ObjectResult(responseJson);
            }             
        }
    }
}
