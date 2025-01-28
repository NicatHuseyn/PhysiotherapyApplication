using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.DateOfBirht)
               .IsRequired();

        builder.Property(u => u.Address)
               .HasMaxLength(255);

        builder.Ignore(u => u.FullName);
    }
}
