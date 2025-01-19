using MediatR;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities.Enums;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Update;

public class UpdateAppointmentCommandRequest:IRequest<ServiceResult>
{
    public Guid Id { get; set; }
    public DateTime? AppointmentDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }
    public string Notes { get; set; }
    public string CancellationReason { get; set; }
    public decimal? ConsultationFee { get; set; }
    public bool IsPaid { get; set; }
}