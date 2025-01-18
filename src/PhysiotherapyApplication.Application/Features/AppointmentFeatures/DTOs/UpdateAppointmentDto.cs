using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Domain.Entities.Enums;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

public record UpdateAppointmentDto( Guid Id,Guid PatientId, string PatientName, DateTime AppointmentDateTime, string Notes, AppointmentStatus AppointmentStatus, string CancellationReason, bool isPaid, Treatment Treatment);

