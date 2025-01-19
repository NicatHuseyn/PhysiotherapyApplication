using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Delete;

public class DeleteAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteAppointmentCommandRequest, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteAppointmentCommandRequest request, CancellationToken cancellationToken)
    {
		try
		{
            var appointment = await repository.GetByIdAsync(request.Id);
            if (appointment is null)
                return ServiceResult.Fail("Apppointment Not Found");

            await repository.DeleteAsync(appointment);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }
		catch (Exception ex)
		{
            return ServiceResult.Fail(ex.Message);
		}
    }
}
