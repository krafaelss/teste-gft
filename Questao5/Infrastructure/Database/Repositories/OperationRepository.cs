using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories;

public class OperationRepository: IOperationRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public OperationRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task Add(Operation operation)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.OpenAsync();
        
        await connection.ExecuteAsync(@$"
INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
    VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)", operation);
    }

    public async Task<IEnumerable<GetAccountOperationsResponse>> GetAccountOperations(GetAccountOperationsRequest request)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.OpenAsync();

        var result = await connection.QueryAsync<GetAccountOperationsResponse>(
            $@"SELECT tipomovimento, valor FROM movimento WHERE idcontacorrente  = @AccountId", request);

        return result;
    }
}