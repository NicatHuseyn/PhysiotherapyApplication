using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Delete;

public class DeleteCommandHandler<TEntity, TId>(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommandRequest<TEntity>, ServiceResult>
    where TEntity : BaseEntity
{

    public async Task<ServiceResult> Handle(DeleteCommandRequest<TEntity> request, CancellationToken cancellationToken)
    {
		try
		{
			var entityModel = await repository.GetByIdAsync(request.Id);
			if (entityModel is null)
				return ServiceResult.Fail("Model Not Found");
			await repository.DeleteAsync(entityModel);
			await unitOfWork.CommitAsync();
			return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }
		catch (Exception ex)
		{
			return ServiceResult.Fail(ex.Message);
		}
    }
}
