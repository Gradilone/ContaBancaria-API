using System.Security.Claims;
using ContaBancaria_API.DTO;
using ContaBancaria_API.Repositories;
using ContaBancaria_API.Repositories.Interfaces;
using ContaBancaria_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaBancaria_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountController(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpGet("balance")]
        [Authorize]
        public async Task<IActionResult> GetBalance()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized("User ID não encontrado.");

            var userId = int.Parse(userIdClaim);

            var account = await _accountRepository.GetAccountByUserId(userId);

            if (account == null) return NotFound("Conta não encontrada");

            return Ok(new
            {
                User = User.Identity?.Name,
                account.AccountNumber,
                Balance = account.Balance
            });

        }
        [HttpGet("extract")]
        [Authorize]
        public async Task<IActionResult> GetExtract()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized("User ID não encontrado.");

            var userId = int.Parse(userIdClaim);

            var account = await _accountRepository.GetAccountByUserId(userId);
            if (account == null) return NotFound("Conta não encontrada");

            var extract = await _transactionRepository.GetTransactionsByAccountId(account.Id);

            return Ok(extract);
        }

        //Método para facilitar testes
        [HttpGet("listAccounts(test)")]
        public async Task<IActionResult> ListAccounts()
        {

            var accounts = await _accountRepository.ListAccounts();

            var result = accounts.Select(a => new
            {
                a.User.Name,
                a.User.Email,
                a.AccountNumber,
                a.Balance
            });

            return Ok(result);

        }


    }
}
