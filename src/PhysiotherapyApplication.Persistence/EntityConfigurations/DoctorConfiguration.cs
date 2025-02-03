using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Surname)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.LicenseNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.Specialization)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Qualifications)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(d => d.Biography)
            .HasMaxLength(1000);

        builder.Property(d => d.ProfilePicture)
            .HasMaxLength(255);

        builder.Property(d => d.WorkingHours)
            .HasMaxLength(100);

        builder.Property(d => d.ConsultationFees)
            .HasPrecision(10, 2);

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
