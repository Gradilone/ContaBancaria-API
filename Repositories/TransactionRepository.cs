using ContaBancaria_API.Data;
using ContaBancaria_API.Models;
using ContaBancaria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContaBancaria_API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.SenderAccountId == accountId || t.ReceiverAccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }


    }
}
