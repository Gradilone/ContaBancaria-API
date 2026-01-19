using ContaBancaria_API.Models;

namespace ContaBancaria_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task Create(User user);

        public Task<User?> GetUserId(int id);

        public Task<User?> GetUserByEmail(string email);
    }
}
