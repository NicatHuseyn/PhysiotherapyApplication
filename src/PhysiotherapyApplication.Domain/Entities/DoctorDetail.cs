using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class DoctorDetail:BaseEntity
{
    public string LicenseNumber { get; set; }
    public string Specialization { get; set; }
    public string Qualifications { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public bool IsAvailable { get; set; }
    public string WorkingHours { get; set; }
    public decimal ConsultationFees { get; set; }
}
