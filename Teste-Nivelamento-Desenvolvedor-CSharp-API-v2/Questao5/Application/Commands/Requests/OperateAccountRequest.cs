using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{

    public class OperateAccountRequest : IRequest<OperateAccountResponse>
    {
        public string IdRequest { get; set; }
        public string AccountId { get; set; }
        public double Value { get; set; }
        public string OperationType { get; set; }
    }
}