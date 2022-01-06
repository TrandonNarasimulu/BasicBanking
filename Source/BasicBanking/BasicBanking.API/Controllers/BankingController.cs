using BasicBanking.Application.Banking.Commands.CreateAccount;
using BasicBanking.Application.Banking.Commands.TransferMoney;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBanking.API.Controllers
{
    public class BankingController : ApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> CreateNewAccount(CreateAccountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> TransferMoney(TransferMoneyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
