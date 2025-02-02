using MediatR;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;

public class RegisterUserCommand:IRequest<ServiceResult>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterUserCommandHandler(IUserService userService) : IRequestHandler<RegisterUserCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await userService.RegisterUserAsync(request);
    }
}
