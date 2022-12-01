using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/OperateAccount")]
    public class OperateAccountController : ControllerBase
    {
        
        [HttpPost]
        [SwaggerOperation(Summary = "Registra uma nova operação de débito ou crédito na conta corrente", OperationId = "OperateAccount")]
        [SwaggerResponse(200, "Movimentação registrada", typeof(OperateAccountResponse))]
        [SwaggerResponse( 400, "Falha ao registrar movimentação", typeof(OperateAccountResponse) )]
        [Route("")]
        public async Task<ActionResult> Operate(
            [FromServices] IMediator mediator, 
            [FromBody, SwaggerRequestBody("Informações da movimentação", Required = true)] OperateAccountRequest command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
