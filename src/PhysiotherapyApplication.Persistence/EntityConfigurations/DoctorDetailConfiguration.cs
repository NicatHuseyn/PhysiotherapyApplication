using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class DoctorDetailConfiguration : BaseEntityConfiguration<DoctorDetail>
{
    public override void Configure(EntityTypeBuilder<DoctorDetail> builder)
    {
        base.Configure(builder);

        builder.Property<string>("DoctorId").IsRequired();

        builder.Property(dd => dd.LicenseNumber)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(dd => dd.Specialization)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(dd => dd.Qualifications)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(dd => dd.Biography)
               .HasMaxLength(1000);

        builder.Property(dd => dd.ProfilePicture)
               .HasMaxLength(255);

        builder.Property(dd => dd.IsAvailable)
               .IsRequired();

        builder.Property(dd => dd.WorkingHours)
               .HasMaxLength(500);

        builder.Property(dd => dd.ConsultationFees)
               .HasPrecision(10, 2);
    }
}
