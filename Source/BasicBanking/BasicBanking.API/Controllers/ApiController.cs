using BasicBanking.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BasicBanking.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
