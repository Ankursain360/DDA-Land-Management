using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamageselfassessmentfloordetailConfiguration : IEntityTypeConfiguration<Newdamageselfassessmentfloordetail>
    {
        public void Configure(EntityTypeBuilder<Newdamageselfassessmentfloordetail> builder)
        {
            //builder.ToTable("newdamageselfassessmentfloordetail", "lms_local20_07_2022");

            builder.HasIndex(e => e.FloorId)
                .HasName("fkFloor_idx");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamagepayeeRegistrationtFloorId_idx");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .ValueGeneratedNever();

            builder.Property(e => e.CarpetArea).HasColumnType("decimal(10,2)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CurrentUse)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ElectricityKno)
                .HasColumnName("ElectricityKNo")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.FloorId)
                .HasColumnName("FloorID")
                .HasColumnType("int(11)");

            builder.Property(e => e.McdpropertyTaxId)
                .HasColumnName("MCDPropertyTaxId")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.Property(e => e.WaterKno)
                .HasColumnName("WaterKNO")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.Floor)
                .WithMany(p => p.Newdamageselfassessmentfloordetail)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("fkFloor");

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamageselfassessmentfloordetail)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtFloorId");
        }
    }
}
