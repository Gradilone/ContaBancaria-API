using ContaBancaria_API.DTO;
using ContaBancaria_API.Models;
using ContaBancaria_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContaBancaria_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var existingUser = await _userRepository.GetUserByEmail(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Usuario com esse email já existe");
            }

            registerDto.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            string accountNumber = new Random().Next(100000, 999999).ToString();

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Account = new Account
                {
                    AccountNumber = accountNumber,
                    Balance = 100m
                }
            };

            await _userRepository.Create(user);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginData)
        {
            var user = await _userRepository.GetUserByEmail(loginData.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
            {
                return Unauthorized("Email ou senha inválidos");
            }
            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                User = new { user.Name, user.Email}
            }); 
        }

        

    }
}
