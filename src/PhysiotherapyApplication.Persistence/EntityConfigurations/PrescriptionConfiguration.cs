using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class PrescriptionConfiguration:BaseEntityConfiguration<Prescription>
{
    public override void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.Property(p => p.Medication).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Dosage).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Frequency).IsRequired().HasMaxLength(50);
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.Instructions).HasMaxLength(1000);
        builder.Property(p => p.SideEffects).HasMaxLength(500);

        builder.HasOne(p => p.Treatment)
            .WithMany(t => t.Prescriptions)
            .HasForeignKey(p => p.TreatmentId);
    }
}
