using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.Services;

public interface IUserService
{
    Task<ServiceResult> RegisterUserAsync(RegisterUserCommand registerUser);

    Task<ServiceResult<ApplicationUserDto>> GetUserAsync(string userNameOrEmail);
}
