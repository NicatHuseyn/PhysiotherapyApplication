using AutoMapper;
using MediatR;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Create;

public class CreateAppointmentCommandHandler(IAppointmentRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateAppointmetCommandRequest, ServiceResult<CreateAppointmentCommandResponse>>
{
    public async Task<ServiceResult<CreateAppointmentCommandResponse>> Handle(CreateAppointmetCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var appointment = mapper.Map<Appointment>(request);
            await repository.AddAsync(appointment);
            await unitOfWork.CommitAsync();
            return ServiceResult<CreateAppointmentCommandResponse>.SuccessAsCreated(new CreateAppointmentCommandResponse { Id = appointment.Id }, $"api/appointments/{appointment.Id}");
        }
        catch (Exception ex)
        {
            return ServiceResult<CreateAppointmentCommandResponse>.Fail(ex.Message);
        }
    }
}
