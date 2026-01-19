using ContaBancaria_API.Models;

namespace ContaBancaria_API.Repositories.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
