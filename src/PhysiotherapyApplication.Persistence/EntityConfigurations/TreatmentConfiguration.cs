using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class TreatmentConfiguration:BaseEntityConfiguration<Treatment>
{
    public override void Configure(EntityTypeBuilder<Treatment> builder)
    {
        builder.Property(t => t.Diagnosis).IsRequired().HasMaxLength(1000);
        builder.Property(t => t.TreatmentPlan).IsRequired().HasMaxLength(1000);
        builder.Property(t => t.Date).IsRequired();
        builder.Property(t => t.Progress).HasMaxLength(1000);
        builder.Property(t => t.Notes).HasMaxLength(1000);

        builder.HasOne(t => t.Patient)
            .WithMany(p => p.Treatments)
            .HasForeignKey(t => t.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Appointment)
            .WithOne(a => a.Treatment)
            .HasForeignKey<Treatment>(t => t.AppointmentId);

        builder.HasMany(t => t.Exercises)
            .WithOne(e => e.Treatment)
            .HasForeignKey(e => e.TreatmentId);

        builder.HasMany(t => t.Prescriptions)
            .WithOne(p => p.Treatment)
            .HasForeignKey(p => p.TreatmentId);

        builder.HasMany(t => t.Documents)
            .WithOne(d => d.Treatment)
            .HasForeignKey(d => d.TreatmentId);
    }
}
