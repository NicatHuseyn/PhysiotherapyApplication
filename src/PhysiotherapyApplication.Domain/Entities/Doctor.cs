using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Domain.Entities;
public class Doctor : ApplicationUser
{
    public DoctorDetail DoctorDetail { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ICollection<Treatment> Treatments { get; set; }
}
