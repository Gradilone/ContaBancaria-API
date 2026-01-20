using ContaBancaria_API.DTO;
using ContaBancaria_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaBancaria_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        
        private readonly IAccountRepository _accountRepository;

        public TransactionController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("transfer")]
        [Authorize]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO transfer)
        {
            var senderId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var senderAccount = await _accountRepository.GetAccountByUserId(senderId);

            var receiverAccount = await _accountRepository.GetAccountByNumber(transfer.ToAccountNumber);

            if (receiverAccount == null) return NotFound("Conta de destino não encontrada.");
            if (senderAccount!.Balance < transfer.Amount) return BadRequest("Saldo insuficiente.");
            if (transfer.Amount <= 0) return BadRequest("O valor deve ser maior que zero.");
            if (senderAccount.AccountNumber == receiverAccount.AccountNumber) return BadRequest("Você não pode transferir para si mesmo.");

            var success = await _accountRepository.TransferMoney(senderAccount, receiverAccount, transfer.Amount);

            if (!success) return StatusCode(500, "Erro ao processar a transferência.");

            return Ok("Transferência realizada com sucesso.");
        }


    }
}
