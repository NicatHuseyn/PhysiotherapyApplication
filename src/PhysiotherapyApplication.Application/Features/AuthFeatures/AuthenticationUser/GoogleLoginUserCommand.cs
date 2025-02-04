using MediatR;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;

public class GoogleLoginUserCommand:IRequest<ServiceResult<TokenDto>>
{
    public string Credential { get; set; }
}

class GoogleLoginUserCommandHandler(IGoogleAuthService authService) : IRequestHandler<GoogleLoginUserCommand, ServiceResult<TokenDto>>
{
    public async Task<ServiceResult<TokenDto>> Handle(GoogleLoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.GoogleLoginAsync(request);
        return result;
    }
}
