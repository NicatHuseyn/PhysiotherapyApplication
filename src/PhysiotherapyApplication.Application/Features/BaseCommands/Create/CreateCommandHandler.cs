using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Create;

public class CreateCommandHandler<TRequest, TEntity, TId>(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCommandRequest<TRequest, TEntity, TId>, ServiceResult<CreateCommandResponse<TId>>>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult<CreateCommandResponse<TId>>> Handle(CreateCommandRequest<TRequest, TEntity, TId> request, CancellationToken cancellationToken)
    {
		try
		{
			var entityModel = mapper.Map<TEntity>(request);
			await repository.AddAsync(entityModel);
			await unitOfWork.CommitAsync();

			return ServiceResult<CreateCommandResponse<TId>>.Success(new CreateCommandResponse<TId>());
		}
		catch (Exception ex)
		{
			return ServiceResult<CreateCommandResponse<TId>>.Fail(ex.Message);
		}
    }
}
