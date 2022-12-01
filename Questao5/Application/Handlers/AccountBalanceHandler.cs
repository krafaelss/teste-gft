using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Application.Handlers;

public class AccountBalanceHandler : IRequestHandler<GetAccountBalanceRequest, GetAccountBalanceResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;

    public AccountBalanceHandler(IAccountRepository accountRepository, IOperationRepository operationRepository)
    {
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
    }

    public async Task<GetAccountBalanceResponse> Handle(GetAccountBalanceRequest request, CancellationToken cancellationToken)
    {
        
        var account = _accountRepository.GetById(request.AccountId).Result;

        if (account == null)
            return await Task.FromResult(GetAccountBalanceResponse.Failed("INVALID_ACCOUNT", "Conta corrente não cadastrada"));

        if (account.Ativo == 0)
            return await Task.FromResult(GetAccountBalanceResponse.Failed("INACTIVE_ACCOUNT", "Conta corrente inativa, não é possivel consultar"));
        

        var operations = await _operationRepository.GetAccountOperations(new GetAccountOperationsRequest(){AccountId = request.AccountId});
        var balance = 0d;
        if (operations.Any())
        {
            var debits = operations.Where(o => o.TipoMovimento == "D").ToList();
            var credits = operations.Where(o => o.TipoMovimento == "C").ToList();

            var debitsSum = 0d;
            var creditsSum = 0d;
            
            if(debits.Any())
                debitsSum = debits.Sum(d => d.valor);

            if(credits.Any())
                creditsSum = credits.Sum(c => c.valor);

            balance = creditsSum - debitsSum;
        }

        var result = GetAccountBalanceResponse.Success(account.Numero, account.Nome, $"{DateTime.Now:dd/MM/yyyy hh:mm}", Math.Round(balance, 2));
        return await Task.FromResult(result);
    }
}