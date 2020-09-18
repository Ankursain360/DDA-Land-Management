using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
   public class JointsurveyConfiguration : IEntityTypeConfiguration<Jointsurvey>
    {
        public void Configure(EntityTypeBuilder<Jointsurvey> builder)
        {
            builder.ToTable("jointsurvey", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JointSurveyDate).HasColumnType("date");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NatureOfStructure)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.SitePosition)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");
    
        }
        }
}
