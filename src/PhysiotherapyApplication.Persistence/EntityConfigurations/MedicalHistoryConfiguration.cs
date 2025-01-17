using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class MedicalHistoryConfiguration:BaseEntityConfiguration<MedicalHistory>
{
    public override void Configure(EntityTypeBuilder<MedicalHistory> builder)
    {
        builder.Property(m => m.ExistingConditions).HasMaxLength(1000);
        builder.Property(m => m.PreviousTreatments).HasMaxLength(1000);
        builder.Property(m => m.Allergies).HasMaxLength(500);
        builder.Property(m => m.CurrentMedications).HasMaxLength(1000);
        builder.Property(m => m.PastSurgeries).HasMaxLength(1000);
        builder.Property(m => m.FamilyHistory).HasMaxLength(1000);
        builder.Property(m => m.LifestyleFactors).HasMaxLength(500);
        builder.Property(m => m.OccupationalHazards).HasMaxLength(500);

        builder.HasOne(m => m.Patient)
            .WithOne(p => p.MedicalHistory)
            .HasForeignKey<MedicalHistory>(m => m.PatientId);
    }
}
