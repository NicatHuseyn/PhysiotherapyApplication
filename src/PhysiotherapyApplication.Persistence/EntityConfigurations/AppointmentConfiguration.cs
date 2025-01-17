using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class AppointmentConfiguration : BaseEntityConfiguration<Appointment>
{
    public override void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(a => a.AppointmentDateTime).IsRequired();
        builder.Property(a => a.Duration).IsRequired();
        builder.Property(a => a.Notes).IsRequired().HasMaxLength(500);
        builder.Property(a => a.CancellationReason).IsRequired().HasMaxLength(500);
        builder.Property(a => a.ConsultationFee).HasColumnType("decimal(18,2)");
        builder.Property(a => a.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(a => a.Patient)
          .WithMany(p => p.Appointments)
          .HasForeignKey(a => a.PatientId)
          .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Treatment)
            .WithOne(t => t.Appointment)
            .HasForeignKey<Treatment>(t => t.AppointmentId);
    }
}
