using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Queries.GetAllQuery;

public class GetAllAppointmentQueryHandler(IAppointmentRepository repository, IMapper mapper) : IRequestHandler<GetAllAppointmentQueryRequest,
	ServiceResult<IEnumerable<GetAllAppointmentQueryResponse>>>
{

    public async Task<ServiceResult<IEnumerable<GetAllAppointmentQueryResponse>>> Handle(GetAllAppointmentQueryRequest request, CancellationToken cancellationToken)
    {
		try
		{
			var appointments = await repository.GetAllAsync();

            var appointmentAsDto = mapper.Map<IEnumerable<GetAllAppointmentQueryResponse>>(appointments);

			return ServiceResult<IEnumerable<GetAllAppointmentQueryResponse>>.Success(appointmentAsDto);
        }
		catch (Exception ex)
		{
			return ServiceResult<IEnumerable<GetAllAppointmentQueryResponse>>.Fail(ex.Message);
		}
    }
}
