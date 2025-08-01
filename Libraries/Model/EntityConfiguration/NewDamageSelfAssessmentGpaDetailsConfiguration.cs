﻿using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewDamageSelfAssessmentGpaDetailsConfiguration : IEntityTypeConfiguration<NewDamageSelfAssessmentGpaDetails>
    {
        public void Configure(EntityTypeBuilder<NewDamageSelfAssessmentGpaDetails> builder)
        {
           // builder.ToTable("newdamage_selfassessment_gpadetail", "lms");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("FkSelfAssissment_idx");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.AddressOfThePlotAsPerGpa)
                .HasMaxLength(4000)
                .IsUnicode(false);

            builder.Property(e => e.AreaOfThePlotAsPerGpa)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DateOfExecutionOfGpa).HasColumnType("date");

            builder.Property(e => e.NameOfThePayer)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NameOfTheSeller)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.GetNewDamageSelfAssessment)
                .WithMany(p => p.NewdamageSelfassessmentGpadetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkSelfAssissment");
        }
    }
}
