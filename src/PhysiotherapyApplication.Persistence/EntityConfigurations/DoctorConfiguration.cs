using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasOne(d => d.DoctorDetail)
               .WithOne()
               .HasForeignKey<DoctorDetail>("DoctorId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Appointments)
               .WithOne()
               .HasForeignKey("DoctorId")
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Treatments)
               .WithOne()
               .HasForeignKey("DoctorId")
               .OnDelete(DeleteBehavior.Restrict);
    }
}
