using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class Prescription : BaseEntity
{
    public Guid TreatmentId { get; set; }
    public virtual Treatment Treatment { get; set; }

    public string Medication { get; set; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Instructions { get; set; }

    public string SideEffects { get; set; }

    public bool IsActive { get; set; }
}
