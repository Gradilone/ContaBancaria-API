using ContaBancaria_API.Data;
using ContaBancaria_API.Models;
using ContaBancaria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContaBancaria_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountByUserId(int userId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<Account?> GetAccountByNumber(string accountNumber)
        {
            return await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<bool> TransferMoney(Account sender, Account receiver, decimal amount)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                sender.Balance -= amount;
                receiver.Balance += amount;

                var transactionHistory = new Transaction
                {
                    SenderAccountId = sender.Id,
                    ReceiverAccountId = receiver.Id,
                    Amount = amount,
                    Date = DateTime.Now,
                    Description = $"Transferência para conta {receiver.AccountNumber}"
                };

                _context.Transactions.Add(transactionHistory);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<List<Account>> ListAccounts()
        {
            return await _context.Accounts
                .Include(a => a.User)
                .ToListAsync();
        }


    }
}
