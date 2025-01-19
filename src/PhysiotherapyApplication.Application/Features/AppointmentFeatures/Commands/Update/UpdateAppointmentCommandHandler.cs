using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Update;

public class UpdateAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAppointmentCommandRequest, ServiceResult>
{

    public async Task<ServiceResult> Handle(UpdateAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
		try
		{
			var appointment = await repository.GetByIdAsync(request.Id);
			if (appointment == null)
				return ServiceResult.Fail("Appointment Not Found");

			appointment = mapper.Map(request,appointment);
			await unitOfWork.CommitAsync();

			return ServiceResult.Success();
        }
		catch (Exception ex)
		{
            return ServiceResult.Fail(ex.Message);
        }
    }
}
