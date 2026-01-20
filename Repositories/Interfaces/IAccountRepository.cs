using ContaBancaria_API.Models;

namespace ContaBancaria_API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account?> GetAccountByUserId(int userId);
        public Task<Account?> GetAccountByNumber(string accountNumber);
        public Task<bool> TransferMoney(Account sender, Account receiver, decimal amount);
        public Task<List<Account>> ListAccounts();
    }
}
