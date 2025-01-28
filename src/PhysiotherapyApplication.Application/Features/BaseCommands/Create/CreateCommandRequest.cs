using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Create;

public class CreateCommandRequest<TRequest, TDto> : IRequest<ServiceResult<TDto>>
{
    public TRequest RequestData { get; set; }
}


public class CreateCommandHandler<TEntity, TRequest, TDto>(
    IGenericRepository<TEntity> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<CreateCommandRequest<TRequest, TDto>, ServiceResult<TDto>>
    where TEntity : BaseEntity
    where TRequest : class
    where TDto : class
{
    public async Task<ServiceResult<TDto>> Handle(CreateCommandRequest<TRequest, TDto> request, CancellationToken cancellationToken)
    {
        if (request == null || request.RequestData == null)
        {
            return ServiceResult<TDto>.Fail("Request data cannot be null.");
        }

        try
        {
            var entityModel = mapper.Map<TEntity>(request.RequestData);

            await repository.AddAsync(entityModel);
            await unitOfWork.CommitAsync();

            var entityAsDto = mapper.Map<TDto>(entityModel);

            return ServiceResult<TDto>.Success(entityAsDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<TDto>.Fail($"An error occurred: {ex.Message}");
        }
    }
}

