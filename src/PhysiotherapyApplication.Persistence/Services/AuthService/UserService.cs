using Microsoft.AspNetCore.Identity;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Persistence.Services.AuthService;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public Task<ServiceResult<ApplicationUserDto>> GetUserAsync(string userNameOrEmail)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> RegisterUserAsync(RegisterUserCommand registerUser)
    {
        var user = new ApplicationUser
        {
            UserName = registerUser.UserName,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email,
        };

        await userManager.CreateAsync(user,registerUser.Password);

        return ServiceResult.Success(System.Net.HttpStatusCode.Created);
    }
}
