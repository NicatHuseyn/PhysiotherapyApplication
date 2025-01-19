using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;

public class GetByIdQueryHandler<TEntity, TResponseModel>(IGenericRepository<TEntity> repository, IMapper mapper) : IRequestHandler<GetByIdQueryRequest<TEntity, TResponseModel>, ServiceResult<GetByIdQueryResponse<TResponseModel>>>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult<GetByIdQueryResponse<TResponseModel>>> Handle(GetByIdQueryRequest<TEntity, TResponseModel> request, CancellationToken cancellationToken)
    {
        var entityModel = await repository.GetByIdAsync(request.Id);
        if (entityModel == null)
            return ServiceResult<GetByIdQueryResponse<TResponseModel>>.Fail("Model Not Found");

        var entityModelAsResponseModel = mapper.Map<GetByIdQueryResponse<TResponseModel>>(entityModel);

        return ServiceResult<GetByIdQueryResponse<TResponseModel>>.Success(entityModelAsResponseModel);
    }
}
