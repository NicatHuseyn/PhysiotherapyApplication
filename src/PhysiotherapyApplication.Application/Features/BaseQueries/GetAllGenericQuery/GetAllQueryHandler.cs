using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;

public class GetAllQueryHandler<TEntity, TResponseModel>(IGenericRepository<TEntity> repository, IMapper mapper) : IRequestHandler<GetAllQueryRequest<TEntity, TResponseModel>, ServiceResult<IEnumerable<GetAllQueryResponse<TResponseModel>>>>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult<IEnumerable<GetAllQueryResponse<TResponseModel>>>> Handle(GetAllQueryRequest<TEntity, TResponseModel> request, CancellationToken cancellationToken)
    {
		try
		{
            var entityModels = await repository.GetAllAsync();

            var entityModelsAsResponseModel = mapper.Map<IEnumerable<GetAllQueryResponse<TResponseModel>>>(entityModels);

            return ServiceResult<IEnumerable<GetAllQueryResponse<TResponseModel>>>.Success(entityModelsAsResponseModel);
        }
		catch (Exception ex)
		{
            return ServiceResult<IEnumerable<GetAllQueryResponse<TResponseModel>>>.Fail(ex.Message);
		}
    }
}
