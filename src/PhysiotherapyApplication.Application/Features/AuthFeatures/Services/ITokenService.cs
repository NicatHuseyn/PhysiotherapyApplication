using PhysiotherapyApplication.Application.Features.AuthFeatures.DTOs;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Application.Features.AuthFeatures.Services;

public interface ITokenService
{
    Task<TokenDto> CreateTokenAsync(ApplicationUser applicationUser);
}
