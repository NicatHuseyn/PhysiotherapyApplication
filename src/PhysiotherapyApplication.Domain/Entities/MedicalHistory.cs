using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class MedicalHistory : BaseEntity
{
    public string PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public string ExistingConditions { get; set; }
    public string PreviousTreatments { get; set; }

    public string Allergies { get; set; }

    public string CurrentMedications { get; set; }

    public string PastSurgeries { get; set; }

    public string FamilyHistory { get; set; }

    public string LifestyleFactors { get; set; }

    public string OccupationalHazards { get; set; }
}
