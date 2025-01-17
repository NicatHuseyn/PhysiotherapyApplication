using PhysiotherapyApplication.Domain.Entities.Common;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Domain.Entities;

public class Doctor : BaseEntity
{
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }


    public string LicenseNumber { get; set; }
    public string Specialization { get; set; }
    public string Qualifications { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public bool IsAvailable { get; set; }
    public string WorkingHours { get; set; }
    public decimal ConsultationFees { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Treatment> Treatments { get; set; }
}
