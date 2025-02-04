using PhysiotherapyApplication.Application.Features.AuthFeatures.AuthenticationUser;
using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.Services;

public interface IGoogleAuthService
{
    Task<ServiceResult<TokenDto>> GoogleLoginAsync(GoogleLoginUserCommand googleLoginUserCommand);
}
