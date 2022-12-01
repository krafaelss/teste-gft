using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.Repositories;

public interface IIdempotenciaRepository
{
    Task Add(AddIdempotenciaRequest idempotencia);
    Task<GetIdempotenciaResponse?> Get(GetIdempotenciaRequest id);
}