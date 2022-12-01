using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class IdempotenciaRepository: IIdempotenciaRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public IdempotenciaRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task Add(AddIdempotenciaRequest request)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.OpenAsync();

        await connection.ExecuteAsync(@$"
INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
    VALUES (@Id, @Request, @Resultado)", request);
    }

    public async Task<GetIdempotenciaResponse?> Get(GetIdempotenciaRequest request)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<GetIdempotenciaResponse>(
            $@"SELECT requisicao, resultado FROM idempotencia WHERE chave_idempotencia  = @Id", request);

        return result.FirstOrDefault();
    }
}