using ContaBancaria_API.Models;

namespace ContaBancaria_API.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
    }
}
