using System.Net;
using Microsoft.AspNetCore.Identity;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AuthFeatures.Services;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Persistence.Services.AuthService;

public class AuthenticationService(ITokenService tokenService, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IGenericRepository<RefreshToken> refreshTokenRepository) : IAuthenticationService
{
    public async Task<ServiceResult<TokenDto>> CreateRefreshTokenAsync(string refreshToken)
    {
        var exsistRefreshToken = await refreshTokenRepository.GetAsync(r=>r.UserRefreshToken == refreshToken);

        if (exsistRefreshToken != null)
            return ServiceResult<TokenDto>.Fail("Refresh Token Not Found", System.Net.HttpStatusCode.NotFound);

        var user = await userManager.FindByIdAsync(exsistRefreshToken.ApplicationUserId);
        if (user is null)
            return ServiceResult<TokenDto>.Fail("User Not Found", HttpStatusCode.NotFound);

        var tokenDto = await tokenService.CreateTokenAsync(user);

        exsistRefreshToken.UserRefreshToken = tokenDto.RefreshToken;
        exsistRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

        await unitOfWork.CommitAsync();

        return ServiceResult<TokenDto>.Success(tokenDto);
    }

    public async Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginUserCommand authenticationUser)
    {
        if (authenticationUser is null)
            throw new ArgumentNullException(nameof(authenticationUser));

        var user = await userManager.FindByEmailAsync(authenticationUser.UserNameOrEmail) ?? await userManager.FindByNameAsync(authenticationUser.UserNameOrEmail);

        var checkPassword = await userManager.CheckPasswordAsync(user, authenticationUser.Password);
        if (user is null || !checkPassword)
            return ServiceResult<TokenDto>.Fail("Username or Password is wrong", HttpStatusCode.BadRequest);

        var token = await tokenService.CreateTokenAsync(user);
        var userRefreshToken = await refreshTokenRepository.GetAsync(r=>r.ApplicationUserId == user.Id);

        if (refreshTokenRepository == null)
        {
            await refreshTokenRepository.AddAsync(new RefreshToken
            {
                ApplicationUserId = user.Id,
                UserRefreshToken = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration,
            });
        }
        //else
        //{
        //    userRefreshToken.UserRefreshToken = token.RefreshToken;
        //    userRefreshToken.Expiration = token.RefreshTokenExpiration;
        //}

        await unitOfWork.CommitAsync();

        return ServiceResult<TokenDto>.Success(token);
    }

    public async Task<ServiceResult> RevokeRefreshToken(string refreshToken)
    {
        var exsistRefreshToken = await refreshTokenRepository.GetAsync(x => x.UserRefreshToken == refreshToken);

        if (exsistRefreshToken is null)
            return ServiceResult.Fail("Refresh Token Not Found", HttpStatusCode.BadRequest);

        await refreshTokenRepository.DeleteAsync(exsistRefreshToken);
        await unitOfWork.CommitAsync();

        return ServiceResult.Success();
    }
}
