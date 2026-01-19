using ContaBancaria_API.Data;
using ContaBancaria_API.Repositories.Interfaces;

namespace ContaBancaria_API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
