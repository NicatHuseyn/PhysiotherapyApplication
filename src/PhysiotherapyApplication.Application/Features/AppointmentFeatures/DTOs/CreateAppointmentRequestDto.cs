namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

public record CreateAppointmentRequestDto(Guid PatientId, DateTime? AppointmentDateTime, TimeSpan Duration, string Notes, decimal? ConsultationFee, Guid TreatmentId);
