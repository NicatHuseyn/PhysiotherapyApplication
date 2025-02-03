using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class RefreshTokenConfiguration:BaseEntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(rt => rt.ApplicationUserId)
           .IsRequired();

        builder.Property(rt => rt.UserRefreshToken)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(rt => rt.Expiration)
            .IsRequired();

        // Index for faster lookups
        builder.HasIndex(rt => rt.UserRefreshToken);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(rt => rt.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

         builder.HasIndex(rt => rt.ApplicationUserId).IsUnique();
    }
}
