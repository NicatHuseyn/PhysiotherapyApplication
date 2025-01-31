namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

public record CreateAppointmentRequestDto(string PatientId, DateTime? AppointmentDateTime, TimeSpan Duration, string Notes, decimal ConsultationFee);
