using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Domain.Entities;

public class Patient : ApplicationUser
{

    public PatientDetail PatientDetail { get; set; }

    public virtual MedicalHistory MedicalHistory { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Treatment> Treatments { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}