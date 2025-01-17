using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Persistence.EntityConfigurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.CreateDate)
            .HasColumnType("datetime2");

        builder.Property(i => i.UpdateDate)
         .HasColumnType("datetime2");

        builder.Property(i => i.DeleteDate)
        .HasColumnType("datetime2");

        builder.HasQueryFilter(i=>!i.DeleteDate.HasValue);
    }
}
