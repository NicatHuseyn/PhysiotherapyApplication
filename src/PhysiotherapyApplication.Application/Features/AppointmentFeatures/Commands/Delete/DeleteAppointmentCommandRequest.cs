using MediatR;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Delete;

public class DeleteAppointmentCommandRequest:IRequest<ServiceResult>
{
    public Guid Id { get; set; }

    public DeleteAppointmentCommandRequest(Guid id)
    {
        Id = id;
    }
}
