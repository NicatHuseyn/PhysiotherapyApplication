using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysiotherapyApplication.Domain.Entities.Enums;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs
{
    public record AppointmentDto(Guid Id, Guid PatientId, Patient Patient, DateTime AppointmentDateTime, TimeSpan Duration, AppointmentStatus AppointmentStatus, string Notes, string CancellationReason, decimal ConsultationFee, bool IsPaid, Treatment Treatment);
}
