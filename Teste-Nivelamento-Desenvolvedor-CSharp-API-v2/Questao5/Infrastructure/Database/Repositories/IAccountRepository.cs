using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repositories
{

    public interface IAccountRepository
    {
        Task<Account?> GetById(string id);
    }
}