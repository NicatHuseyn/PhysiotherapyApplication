using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasOne(p => p.PatientDetail)
               .WithOne()
               .HasForeignKey<PatientDetail>("PatientId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.MedicalHistory)
               .WithOne(mh => mh.Patient)
               .HasForeignKey<MedicalHistory>(mh => mh.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Appointments)
               .WithOne(a => a.Patient)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Treatments)
               .WithOne(t => t.Patient)
               .HasForeignKey(t => t.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Documents)
               .WithOne(d => d.Patient)
               .HasForeignKey(d => d.PatientId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
