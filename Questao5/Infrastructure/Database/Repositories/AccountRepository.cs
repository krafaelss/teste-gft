using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{

    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AccountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        //public async Task<IEnumerable<Account>> GetAll()
        //{
        //    using var connection = new SqliteConnection(_databaseConfig.Name);
        //    await connection.OpenAsync();

        //    return await connection.QueryAsync<Account>(
        //        $@"SELECT idcontacorrente, numero, nome, ativo FROM contacorrente");
        //}

        public async Task<Account?> GetById(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Account>(
                $@"SELECT idcontacorrente, numero, nome, ativo FROM contacorrente WHERE idcontacorrente  = '{id}'");

            return result.FirstOrDefault();
        }

        


    }
}