using PhysiotherapyApplication.Domain.Entities.Enums;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

public record UpdateAppointmentDto(Guid Id,DateTime AppointmentDateTime, TimeSpan Duration, AppointmentStatus AppointmentStatus, string Notes, string CancellationReason, decimal? ConsulationFee, bool isPaid);

