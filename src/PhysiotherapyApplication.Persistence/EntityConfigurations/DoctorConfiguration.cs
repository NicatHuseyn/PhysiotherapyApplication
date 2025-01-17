using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class DoctorConfiguration : BaseEntityConfiguration<Doctor>
{
    public override void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.Property(d => d.Specialization).IsRequired().HasMaxLength(500);
        builder.Property(d => d.LicenseNumber).HasMaxLength(20);
        builder.Property(d => d.Qualifications).HasMaxLength(500);
        builder.Property(d => d.Biography).HasMaxLength(1000);
        builder.Property(d => d.ProfilePicture).HasMaxLength(255);
        builder.Property(d => d.WorkingHours).HasMaxLength(500);
        builder.Property(d => d.ConsultationFees).HasColumnType("decimal(18,2)");

        builder.HasOne(d => d.ApplicationUser)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.ApplicationUserId)
            .IsRequired();
    }
}
