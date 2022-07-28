using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamageselfassessmentgpadetailConfiguration : IEntityTypeConfiguration<Newdamageselfassessmentgpadetail>
    {
        public void Configure(EntityTypeBuilder<Newdamageselfassessmentgpadetail> builder)
        {
            //entity.ToTable("newdamageselfassessmentgpadetail", "lms_local20_07_2022");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamagepayeeRegistrationtGpaId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AddressOfThePlotAsPerGpa)
                .HasMaxLength(4000)
                .IsUnicode(false);

            builder.Property(e => e.AreaOfThePlotAsPerGpa)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DateOfExecutionOfGpa).HasColumnType("date");

            builder.Property(e => e.GpafilePath).HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NameOfThePayer)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NameOfTheSeller)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamageselfassessmentgpadetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtGpaId");
        }
    }
}
