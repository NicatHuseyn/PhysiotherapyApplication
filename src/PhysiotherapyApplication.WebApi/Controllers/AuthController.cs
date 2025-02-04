using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;

namespace PhysiotherapyApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator, IAuthenticationService authenticationService) : BaseController
    {


        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand command)
        {
            var result =  await mediator.Send(command);

            return CreateActionResult(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand authenticationUser)
        {
            var result = await mediator.Send(authenticationUser);
            return CreateActionResult(result);
        }

        [HttpPost("create-refreshtoken")]
        public async Task<IActionResult> CreateRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await authenticationService.CreateRefreshTokenAsync(refreshTokenDto.Token);

            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> GoogleLogin(GoogleLoginUserCommand googleLoginUserCommand)
        {
            var result = await mediator.Send(googleLoginUserCommand);
            return CreateActionResult(result);
        }
    }
}
