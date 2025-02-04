using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;
using PhysiotherapyApplication.Domain.Options;

namespace PhysiotherapyApplication.Persistence.Services.AuthService;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly GoogleConfigurationOption _googleConfigurationOption;
    private readonly ITokenService _tokenService;

    public GoogleAuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<GoogleConfigurationOption> googleConfigurationOption,
        IConfiguration configuration
,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _googleConfigurationOption = googleConfigurationOption.Value;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public async Task<ServiceResult<TokenDto>> GoogleLoginAsync(GoogleLoginUserCommand googleLoginUserCommand)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string>() { _googleConfigurationOption.ClientId}
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginUserCommand.Credential, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject,"GOOGLE");

        var user = await _userManager.FindByLoginAsync(info.LoginProvider,info.ProviderKey);

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(payload.Email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("Could not create user");
                }
            }
        }

        var identityResult = await _userManager.AddLoginAsync(user,info);

        if (!identityResult.Succeeded)
            throw new Exception("Invariant external authentication.");
        var token = await _tokenService.CreateTokenAsync(user);

        return ServiceResult<TokenDto>.Success(token);
    }
}
