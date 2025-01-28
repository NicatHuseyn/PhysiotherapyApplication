using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class Treatment : BaseEntity
{
    public string PatientId { get; set; }

    public virtual Patient Patient { get; set; }

    public Guid AppointmentId { get; set; }

    public virtual Appointment Appointment { get; set; }

    public string Diagnosis { get; set; }

    public string TreatmentPlan { get; set; }

    public DateTime Date { get; set; }

    public string Progress { get; set; }

    public string Notes { get; set; }

    public DateTime? NextAppointmentDate { get; set; }

    public bool RequiresFollowUp { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}
