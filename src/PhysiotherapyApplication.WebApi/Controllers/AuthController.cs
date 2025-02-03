using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand command)
        {
            var result =  await mediator.Send(command);

            return CreateActionResult(result);
        }
    }
}
