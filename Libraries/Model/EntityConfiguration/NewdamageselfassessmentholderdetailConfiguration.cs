using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamageselfassessmentholderdetailConfiguration : IEntityTypeConfiguration<Newdamageselfassessmentholderdetail>
    {
        public void Configure(EntityTypeBuilder<Newdamageselfassessmentholderdetail> builder)
        {
            //entity.ToTable("newdamageselfassessmentholderdetail", "lms_local20_07_2022");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamagepayeeRegistrationtholderId_idx");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .ValueGeneratedNever();

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DeathCertificateDate).HasColumnType("date");

            builder.Property(e => e.DeathCertificateNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsRelinquished)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NameOfGpaats)
                .HasColumnName("NameOfGPAATS")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NameOfSurvivingMember)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.Property(e => e.Relationship)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamageselfassessmentholderdetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtholderId");
        }
    }
}
