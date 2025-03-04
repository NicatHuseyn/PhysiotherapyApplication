﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.EntityConfigurations;

namespace PhysiotherapyApplication.Persistence.EntityTypeConfigurations;

public class ExerciseConfiguration : BaseEntityConfiguration<Exercise>
{
    public override void Configure(EntityTypeBuilder<Exercise> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(e => e.Instructions)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(e => e.Duration)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.RepetitionsCount);

        builder.Property(e => e.SetsCount);

        builder.Property(e => e.VideoUrl)
               .HasMaxLength(500);

        builder.Property(e => e.ImageUrl)
               .HasMaxLength(500);

        builder.HasOne(e => e.Treatment)
               .WithMany(t => t.Exercises)
               .HasForeignKey(e => e.TreatmentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
