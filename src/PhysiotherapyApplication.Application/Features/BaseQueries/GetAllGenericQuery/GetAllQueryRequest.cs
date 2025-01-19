using MediatR;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.BaseQueries.GetAllGenericQuery;

public class GetAllQueryRequest<TEntity, TResponseModel>:IRequest<ServiceResult<IEnumerable<GetAllQueryResponse<TResponseModel>>>>
{
}
