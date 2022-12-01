using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{

    [ApiController]
    [Route("v1/Account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Exibir saldo atual da conta corrente", OperationId = "GetAccountBalance")]
        [SwaggerResponse(200, "Saldo exibido com sucesso", typeof(GetAccountBalanceResponse))]
        [SwaggerResponse(400, "Falha ao exibir saldo", typeof(GetAccountBalanceResponse))]
        [Route("Balance")]
        public async Task<ActionResult> GetBalance([FromServices] IMediator mediator,
            [FromBody, SwaggerRequestBody("Informação da conta corrente para busca do saldo", Required = true)] GetAccountBalanceRequest command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

    }
}