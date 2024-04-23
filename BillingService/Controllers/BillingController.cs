using System.Text.Json;
using BillingService.Dtos;
using BillingService.Services;
using Contracts.Billing;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Controllers
{
    [ApiController]
    [Route("billingservice/[controller]")]
    public class BillingController(IAccountService accountService, ILogger<BillingController> logger) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        public ActionResult<AccountDto> CreateAccount([FromBody] CreateAccountDto dto)
        {
            logger.LogInformation("'{MethodName}' with parameter '{Dto}' was called", nameof(CreateAccount), JsonSerializer.Serialize(dto));

            var newAccount = accountService.CreateAccount(dto.UserName);
            return Ok(newAccount);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        public ActionResult<AccountDto> GetAccountById(int id)
        {
            logger.LogInformation("'{MethodName}' with parameter '{Id}' was called", nameof(GetAccountById), id);

            var account = accountService.GetAccount(id);
            if (account == null)
            {
                return NotFound($"Can't get user by [{id}] id");
            }

            return Ok(account);
        }

        [HttpPut("increase")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public IActionResult IncreaseBalance([FromBody] AccountOperation accountOperation)
        {
            var account = accountService.GetAccount(accountOperation.AccountId);
            if (account == null)
            {
                return NotFound($"Can't get user by [{accountOperation.AccountId}] id");
            }

            try
            {
                accountService.IncreaseBalance(account, accountOperation.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("decrease")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public ActionResult<bool> DecreaseBalance([FromBody] AccountOperation accountOperation)
        {
            var account = accountService.GetAccount(accountOperation.AccountId);
            if (account == null)
            {
                return NotFound($"Can't get user by [{accountOperation.AccountId}] id");
            }

            try
            {
                return Ok(accountService.DecreaseBalance(account, accountOperation.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
