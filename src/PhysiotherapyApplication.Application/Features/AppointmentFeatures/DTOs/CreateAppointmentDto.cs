using PhysiotherapyApplication.Domain.Entities.Enums;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

public record CreateAppointmentDto(Guid PatientId, string PatientName, DateTime AppointmentDateTime,TimeSpan Duration ,string Notes, AppointmentStatus AppointmentStatus, string CancellationReason, decimal? ConsultationFee, bool isPaid, Guid TreatmentId);
