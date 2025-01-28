using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class PatientDetailConfiguration : BaseEntityConfiguration<PatientDetail>
{
    public override void Configure(EntityTypeBuilder<PatientDetail> builder)
    {
        base.Configure(builder);

        builder.Property<string>("PatientId").IsRequired();

        builder.Property(pd => pd.InsuranceProvider)
               .HasMaxLength(100);

        builder.Property(pd => pd.InsurancePolicyNumber)
               .HasMaxLength(50);

        builder.Property(pd => pd.InsuranceExpiryDate);

        builder.Property(pd => pd.Gender)
               .IsRequired()
               .HasConversion<int>();

        builder.Property(pd => pd.BloodGroup)
               .HasConversion<int>();

        builder.Property(pd => pd.EmergencyContactName)
               .HasMaxLength(100);

        builder.Property(pd => pd.EmergencyContactPhone)
               .HasMaxLength(20);
    }
}
