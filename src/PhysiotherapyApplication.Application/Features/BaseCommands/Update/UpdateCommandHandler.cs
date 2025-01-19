using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseCommands.Update;

public class UpdateCommandHandler<TEntity, TModel>(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCommandRequest<TEntity, TModel>, ServiceResult>
    where TEntity : BaseEntity
{

    public async Task<ServiceResult> Handle(UpdateCommandRequest<TEntity, TModel> request, CancellationToken cancellationToken)
    {
		try
		{
			var entityModel = await repository.GetByIdAsync(request.Id);
			if (entityModel is null)
				return ServiceResult.Fail("Model Not Found");
			entityModel = mapper.Map(request,entityModel);

			await unitOfWork.CommitAsync();
			return ServiceResult.Success();

		}
		catch (Exception ex)
		{
			return ServiceResult.Fail(ex.Message);
		}
    }
}
