using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class JointSurveySitePositionMappedConfiguration : IEntityTypeConfiguration<Jointsurveysitepositionmapped>
    {
        public void Configure(EntityTypeBuilder<Jointsurveysitepositionmapped> builder)
        {
            builder.ToTable("jointsurveysitepositionmapped", "lms");

            //builder.HasIndex(e => e.JointSurveyId)
            //    .HasName("fk_JointSurvey_idx");

            builder.HasIndex(e => e.SitePositionId)
                .HasName("fk_SitePositionId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.IsAvailable).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.JointSurveyId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.SitePositionId).HasColumnType("int(11)");

            //builder.HasOne(d => d.JointSurvey)
            //    .WithMany(p => p.Jointsurveysitepositionmapped)
            //    .HasForeignKey(d => d.JointSurveyId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_JointSurveyId");

            builder.HasOne(d => d.SitePosition)
                .WithMany(p => p.Jointsurveysitepositionmapped)
                .HasForeignKey(d => d.SitePositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SitePositionId");
        }
    }
}

