using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class MedicalHistoryConfiguration : BaseEntityConfiguration<MedicalHistory>
{
    public override void Configure(EntityTypeBuilder<MedicalHistory> builder)
    {
        base.Configure(builder);

        builder.Property(mh => mh.ExistingConditions)
               .HasMaxLength(1000);

        builder.Property(mh => mh.PreviousTreatments)
               .HasMaxLength(1000);

        builder.Property(mh => mh.Allergies)
               .HasMaxLength(500);

        builder.Property(mh => mh.CurrentMedications)
               .HasMaxLength(1000);

        builder.Property(mh => mh.PastSurgeries)
               .HasMaxLength(1000);

        builder.Property(mh => mh.FamilyHistory)
               .HasMaxLength(1000);

        builder.Property(mh => mh.LifestyleFactors)
               .HasMaxLength(500);

        builder.Property(mh => mh.OccupationalHazards)
               .HasMaxLength(500);

        builder.HasOne(mh => mh.Patient)
               .WithOne(p => p.MedicalHistory)
               .HasForeignKey<MedicalHistory>(mh => mh.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
