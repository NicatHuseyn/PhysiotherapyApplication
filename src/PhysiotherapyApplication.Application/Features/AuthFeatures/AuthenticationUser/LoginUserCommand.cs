using MediatR;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;

public class LoginUserCommand:IRequest<ServiceResult<TokenDto>>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}


class AuthenticationUserCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<LoginUserCommand, ServiceResult<TokenDto>>
{
    public async Task<ServiceResult<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await authenticationService.CreateTokenAsync(request);

        return result;
    }
}
