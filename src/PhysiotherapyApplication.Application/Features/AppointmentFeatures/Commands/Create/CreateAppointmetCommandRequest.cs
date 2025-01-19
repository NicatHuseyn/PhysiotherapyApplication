using MediatR;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Commands.Create;

public class CreateAppointmetCommandRequest : IRequest<ServiceResult<CreateAppointmentCommandResponse>>
{
    public Guid PatientId { get; set; }
    public DateTime? AppointmentDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string Notes { get; set; }
    public decimal? ConsultationFee { get; set; }
    public Guid TreatmentId { get; set; }

}


//Guid PatientId, DateTime? AppointmentDateTime, TimeSpan Duration, string Notes, decimal? ConsultationFee, Guid TreatmentId)