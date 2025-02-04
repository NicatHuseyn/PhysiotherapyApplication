using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;
using PhysiotherapyApplication.Domain.Options;

namespace PhysiotherapyApplication.Persistence.Services.AuthService;

public class TokenService : ITokenService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomTokenOption _customTokenOption;

    public TokenService(UserManager<ApplicationUser> userManager, IOptions<CustomTokenOption> options)
    {
        _userManager = userManager;
        _customTokenOption = options.Value;
    }

    public async Task<TokenDto> CreateTokenAsync(ApplicationUser applicationUser)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOption.AccessTokenExpiration);

        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOption.RefreshTokenExpiration);


        var securityKey = SignService.GetSymmetricSecurityKey(_customTokenOption.SecurityKey);

        SigningCredentials credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        var claims = await GetClaimsAsync(applicationUser,_customTokenOption.Auidence);

        // Create JWT Token
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
            issuer:_customTokenOption.Issuer,
            expires:accessTokenExpiration,
            notBefore:DateTime.UtcNow,
            claims:claims,
            signingCredentials:credentials
            );

        var handler = new JwtSecurityTokenHandler();

        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration,
        };

        return tokenDto;
    }


    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser applicationUser, string audience)
    {
        var userRoles = await _userManager.GetRolesAsync(applicationUser);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, applicationUser.Id),
            new(JwtRegisteredClaimNames.Email, applicationUser.Email ?? string.Empty),
            new(ClaimTypes.Name, applicationUser.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Aud, audience),
            new("address", applicationUser.Address.ToString()),
            new("age", applicationUser.Age.ToString()),
        };


        //claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
