using BasicBanking.Application.Banking.Queires.GetAccountBalance;
using BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber;
using BasicBanking.Application.Banking.Commands.CreateAccount;
using BasicBanking.Application.Banking.Commands.TransferMoney;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BasicBanking.Application.Banking.Queires.GetTransferHistory;

namespace BasicBanking.API.Controllers
{
    public class BankingController : ApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateAccountViewModel>> CreateNewAccount(CreateAccountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> TransferMoney(TransferMoneyCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{accountNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountBalanceViewModel>> BalanceByAccountNumber(string accountNumber)
        {
            return Ok(await Mediator.Send(new AccountBalanceQuery { AccountNumber = accountNumber }));
        }

        [HttpGet("{idNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountBalanceViewModel>> BalanceByUserIDNumber(string idNumber)
        {
            return Ok(await Mediator.Send(new BalanceByIDQuery { IDNumber = idNumber }));
        }

        [HttpGet("{accountNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransferHistoryViewModel>> GetTransferHistory(string accountNumber)
        {
            return Ok(await Mediator.Send(new TransferHistoryQuery { AccountNumber = accountNumber }));
        }
    }
}
