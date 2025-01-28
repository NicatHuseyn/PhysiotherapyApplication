using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;

public class GetAllQueryRequest<TDto>:IRequest<ServiceResult<IEnumerable<TDto>>>
{
}

public class GetAllQueryHandler<TEntity, TDto>(IGenericRepository<TEntity> repository, IMapper mapper) : IRequestHandler<GetAllQueryRequest<TDto>, ServiceResult<IEnumerable<TDto>>>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult<IEnumerable<TDto>>> Handle(GetAllQueryRequest<TDto> request, CancellationToken cancellationToken)
    {
		try
		{
            var entityModels = await repository.GetAllAsync();

            var entityModelAsDtos = mapper.Map<IEnumerable<TDto>>(entityModels);

            return ServiceResult<IEnumerable<TDto>>.Success(entityModelAsDtos);
        }
		catch (Exception ex)
		{
            return ServiceResult<IEnumerable<TDto>>.Fail(ex.Message);
		}
    }
}
