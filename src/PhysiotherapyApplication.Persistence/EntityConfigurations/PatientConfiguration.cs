using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class PatientConfiguration:BaseEntityConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(p => p.EmergencyContactPhone).HasMaxLength(450);
        builder.Property(p => p.EmergencyContactName).HasMaxLength(450);
        builder.Property(p => p.InsuranceProvider).IsRequired().HasMaxLength(200);
        builder.Property(p => p.InsurancePolicyNumber).IsRequired().HasMaxLength(20);
        builder.Property(p => p.InsuranceExpiryDate).HasColumnType("datetime2");
        builder.Property(p => p.BloodGroup)
            .HasConversion<int>()
            .IsRequired(); 

        builder.Property(p => p.Gender)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(p => p.ApplicationUser)
        .WithOne()
        .HasForeignKey<Patient>(p => p.ApplicationUserId)
        .IsRequired();

        builder.HasOne(p => p.MedicalHistory)
            .WithOne(p => p.Patient)
            .HasForeignKey<MedicalHistory>(p => p.PatientId);

        builder.HasMany(p => p.Appointments)
            .WithOne(p => p.Patient)
            .HasForeignKey(p => p.PatientId);

        builder.HasMany(p => p.Treatments)
           .WithOne(t => t.Patient)
           .HasForeignKey(t => t.PatientId);

        builder.HasMany(p => p.Documents)
            .WithOne(d => d.Patient)
            .HasForeignKey(d => d.PatientId);
    }
}
