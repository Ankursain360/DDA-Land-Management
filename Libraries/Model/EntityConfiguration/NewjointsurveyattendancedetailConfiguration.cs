using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewjointsurveyattendancedetailConfiguration : IEntityTypeConfiguration<Newjointsurveyattendancedetail>
    {


        public void Configure(EntityTypeBuilder<Newjointsurveyattendancedetail> builder)
        {
            //builder.ToTable("newjointsurveyattendancedetail", "lms");

            builder.HasIndex(e => e.JointSurveyId)
                    .HasName("FKnewjoint_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Attendance)
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Designation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("1");

            builder.Property(e => e.JointSurveyId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.HasOne(d => d.JointSurvey)
                    .WithMany(p => p.Newjointsurveyattendancedetail)
                    .HasForeignKey(d => d.JointSurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKnewjoint");
        }
    }
}
