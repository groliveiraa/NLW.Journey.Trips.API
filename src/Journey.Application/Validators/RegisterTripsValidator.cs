using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.Validators
{
    public class RegisterTripsValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripsValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NOME_VAZIO);

            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.DATA_INICIO_POSTERIOR_DATA_NOW);

            RuleFor(request => request)
                .Must(request => request.EndDate.Date >= request.StartDate.Date)
                .WithMessage(ResourceErrorMessages.DATA_TERMINO_VIAGEM_POSTERIOR_DATA_INICIO);
        }
    }
}
