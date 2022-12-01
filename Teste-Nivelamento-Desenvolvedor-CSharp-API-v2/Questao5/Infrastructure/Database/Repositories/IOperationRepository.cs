using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.Repositories;

public interface IOperationRepository
{
    Task Add(Operation operation);
    Task<IEnumerable<GetAccountOperationsResponse>> GetAccountOperations(GetAccountOperationsRequest request);
}