using PhysiotherapyApplication.Domain.Entities.Common;
using PhysiotherapyApplication.Domain.Entities.Enums;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Domain.Entities;

public class Patient : BaseEntity
{
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public string InsuranceProvider { get; set; }

    public string InsurancePolicyNumber { get; set; }

    public DateTime? InsuranceExpiryDate { get; set; }

    public Gender Gender { get; set; }

    public BloodGroup? BloodGroup { get; set; }

    public string EmergencyContactName { get; set; }

    public string EmergencyContactPhone { get; set; }

    public virtual MedicalHistory MedicalHistory { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Treatment> Treatments { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
}