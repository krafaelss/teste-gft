using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Application.Handlers
{

    public class OperateAccountHandler : IRequestHandler<OperateAccountRequest, OperateAccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public OperateAccountHandler(IAccountRepository accountRepository, IOperationRepository operationRepository, IIdempotenciaRepository idempotenciaRepository)
        {
            _accountRepository = accountRepository;
            _operationRepository = operationRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public Task<OperateAccountResponse> Handle(OperateAccountRequest request, CancellationToken cancellationToken)
        {
            //check idempotencia
            var idempotencia = _idempotenciaRepository.Get(new GetIdempotenciaRequest(request.IdRequest)).Result;

            if (idempotencia != null)
            {
                var iRequest = JsonConvert.DeserializeObject<OperateAccountRequest>(idempotencia.Requisicao);
                var iResult = JsonConvert.DeserializeObject<OperateAccountResponse>(idempotencia.Resultado);

                return Task.FromResult(iResult)!;
            }

            OperateAccountResponse result;
            var account = _accountRepository.GetById(request.AccountId).Result;


            if (account == null)
                return Task.FromResult(OperateAccountResponse.Failed("INVALID_ACCOUNT", "Conta corrente não cadastrada"));

            if (account.Ativo == 0)
                return Task.FromResult(OperateAccountResponse.Failed("INACTIVE_ACCOUNT", "Conta corrente inativa, não é possível fazer movimentação"));
            
            if (request.Value < 0)
                return Task.FromResult(OperateAccountResponse.Failed("INVALID_VALUE", "Apenas valores positivos são permitidos"));

            if(request.OperationType != "D" && request.OperationType != "C")
                return Task.FromResult(OperateAccountResponse.Failed("INVALID_TYPE", "Apenas os tipos de operação “débito” ou “crédito” são permitidos"));

            
            var operationId = Guid.NewGuid().ToString();
            var operation = new Operation()
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = request.AccountId,
                DataMovimento = $"{DateTime.Now:d}",
                Valor = Math.Round(request.Value, 2),
                TipoMovimento = request.OperationType
                
            };

            Task.FromResult(_operationRepository.Add(operation));
            result = OperateAccountResponse.Success(operationId);

            var requestJson = JsonConvert.SerializeObject(request);
            var resultJson = JsonConvert.SerializeObject(result);
            Task.FromResult(_idempotenciaRepository.Add(new AddIdempotenciaRequest(request.IdRequest, requestJson, resultJson)));

            return Task.FromResult(result);
        }
    }
}