using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewDamageSelfAssessmentAtsDetailsConfiguration : IEntityTypeConfiguration<NewDamageSelfAssessmentAtsDetails>
    {
        public void Configure(EntityTypeBuilder<NewDamageSelfAssessmentAtsDetails> builder)
        {
           // builder.ToTable("newdamage_selfassessment_atsdetail", "lms");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("FkNewAtsSelfAssissment_idx");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AddressOfThePlotAsPerAts)
                .HasMaxLength(4000)
                .IsUnicode(false);

            builder.Property(e => e.AreaOfThePlotAsPerAts)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DateOfExecutionOfAts).HasColumnType("date");

            builder.Property(e => e.NameOfThePayerAts)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NameOfTheSellerAts)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.GetSelfAssessment)
                .WithMany(p => p.NewdamageSelfassessmentAtsdetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkNewAtsSelfAssissment");
        }
    }
}
