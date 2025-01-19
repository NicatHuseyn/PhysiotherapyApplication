using MediatR;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Queries.GetAllQuery;

public class GetAllAppointmentQueryRequest:IRequest<ServiceResult<IEnumerable<GetAllAppointmentQueryResponse>>>
{
}
