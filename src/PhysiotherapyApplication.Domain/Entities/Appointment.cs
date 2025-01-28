using PhysiotherapyApplication.Domain.Entities.Common;
using PhysiotherapyApplication.Domain.Entities.Enums;

namespace PhysiotherapyApplication.Domain.Entities;

public class Appointment : BaseEntity
{
    public string PatientId { get; set; }
    public Patient Patient { get; set; }

    public DateTime AppointmentDateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Notes { get; set; }
    public string? CancellationReason { get; set; }
    public decimal? ConsultationFee { get; set; }
    public bool IsPaid { get; set; }

    public virtual Treatment Treatment { get; set; }
}
