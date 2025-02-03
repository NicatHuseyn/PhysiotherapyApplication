using Mapster;
using Microsoft.AspNetCore.Identity;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.RegisterUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Persistence.Services.AuthService;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<ServiceResult<ApplicationUserDto>> GetUserAsync(string userNameOrEmail)
    {
        var exsistUser = await userManager.FindByEmailAsync(userNameOrEmail) ?? await userManager.FindByNameAsync(userNameOrEmail);

        var dtoAsUser = exsistUser.Adapt<ApplicationUserDto>();

        if (exsistUser is null)
            return ServiceResult<ApplicationUserDto>.Fail("User Not Found", System.Net.HttpStatusCode.NotFound);

        return ServiceResult<ApplicationUserDto>.Success(dtoAsUser);
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

        IdentityResult result = await userManager.CreateAsync(user,registerUser.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e=>e.Description).ToList();

            return ServiceResult.Fail(errors);
        }

        return ServiceResult.Success(System.Net.HttpStatusCode.Created);
    }
}
