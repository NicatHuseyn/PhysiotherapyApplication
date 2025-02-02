using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;

public class GetByIdQueryRequest<TDto>:IRequest<ServiceResult<TDto>>
{
    public Guid Id { get; set; }
}

public class GetByIdQueryHandler<TEntity, TDto>(IGenericRepository<TEntity> repository, IMapper mapper) : IRequestHandler<GetByIdQueryRequest<TDto>, ServiceResult<TDto>>
    where TEntity : BaseEntity
{
    public async Task<ServiceResult<TDto>> Handle(GetByIdQueryRequest<TDto> request, CancellationToken cancellationToken)
    {
		try
		{
            var entityModel = await repository.GetByIdAsync(request.Id);

            var entityModelAsDto = mapper.Map<TDto>(entityModel);

            return ServiceResult<TDto>.Success(entityModelAsDto);
        }
		catch (Exception ex)
		{
            return ServiceResult<TDto>.Fail(ex.Message);
		}
    }
}
