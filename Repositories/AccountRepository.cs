using ContaBancaria_API.Data;
using ContaBancaria_API.Repositories.Interfaces;

namespace ContaBancaria_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
