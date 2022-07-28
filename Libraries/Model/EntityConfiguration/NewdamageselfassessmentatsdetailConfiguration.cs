using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamageselfassessmentatsdetailConfiguration : IEntityTypeConfiguration<Newdamageselfassessmentatsdetail>
    {
        public void Configure(EntityTypeBuilder<Newdamageselfassessmentatsdetail> builder)
        {
            //builder.ToTable("newdamageselfassessmentatsdetail", "lms_local20_07_2022");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamageSelfAssessmentId_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.AddressOfThePlotAsPerAts)
                .HasMaxLength(4000)
                .IsUnicode(false);

            builder.Property(e => e.AreaOfThePlotAsPerAts)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DateOfExecutionOfAts).HasColumnType("date");

            builder.Property(e => e.AtsfilePath)
                .HasColumnName("ATSFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NameOfThePayerAts)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NameOfTheSellerAts)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamageselfassessmentatsdetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtId");
        }
    }
}
