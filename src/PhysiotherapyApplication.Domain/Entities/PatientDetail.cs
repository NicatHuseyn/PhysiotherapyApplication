using PhysiotherapyApplication.Domain.Entities.Common;
using PhysiotherapyApplication.Domain.Entities.Enums;

namespace PhysiotherapyApplication.Domain.Entities;

public class PatientDetail:BaseEntity
{
    public string InsuranceProvider { get; set; }

    public string InsurancePolicyNumber { get; set; }

    public DateTime? InsuranceExpiryDate { get; set; }

    public Gender Gender { get; set; }

    public BloodGroup? BloodGroup { get; set; }

    public string EmergencyContactName { get; set; }

    public string EmergencyContactPhone { get; set; }
}
