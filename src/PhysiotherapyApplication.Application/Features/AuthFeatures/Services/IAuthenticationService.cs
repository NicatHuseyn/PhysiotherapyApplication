﻿using PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.Services;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginUserCommand authenticationUser);

    Task<ServiceResult<TokenDto>> CreateRefreshTokenAsync(string refreshToken);

    Task<ServiceResult> RevokeRefreshToken(string refreshToken);
}
