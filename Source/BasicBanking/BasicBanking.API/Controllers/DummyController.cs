using BasicBanking.Application.Dummy.Commands.GetText;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBanking.API.Controllers
{
    public class DummyController : ApiController
    {
        [HttpGet("GetDummyData/{text}")]
        public async Task<ActionResult<string>> GetDummyData(string text)
        {
            var result = await Mediator.Send(new DummyCommand { InputText = text });
            return result.Response;
        }
    }
}
