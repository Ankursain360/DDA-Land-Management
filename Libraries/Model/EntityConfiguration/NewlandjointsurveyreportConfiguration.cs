using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandjointsurveyreportConfiguration : IEntityTypeConfiguration<Newjointsurveyreportdetail>
    {


        public void Configure(EntityTypeBuilder<Newjointsurveyreportdetail> builder)
        {

            builder.ToTable("newjointsurveyreportdetail", "lms");

            builder.HasIndex(e => e.JointSurveyId)
                    .HasName("FKnew_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("1");

            builder.Property(e => e.JointSurveyId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UploadFilePath).HasColumnType("longtext");

            builder.HasOne(d => d.JointSurvey)
                    .WithMany(p => p.Newjointsurveyreportdetail)
                    .HasForeignKey(d => d.JointSurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKnew");
        }
    }
}
