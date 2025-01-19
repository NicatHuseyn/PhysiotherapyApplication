using MediatR;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetByIdGenericQuery;

public class GetByIdQueryRequest<TEntity,TResponseModel>:IRequest<ServiceResult<GetByIdQueryResponse<TResponseModel>>>
    where TEntity : BaseEntity
{
    public Guid Id { get; set; }
}
