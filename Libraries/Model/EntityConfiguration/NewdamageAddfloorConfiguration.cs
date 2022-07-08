using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamageAddfloorConfiguration : IEntityTypeConfiguration<NewdamageAddfloor>
    {
        public void Configure(EntityTypeBuilder<NewdamageAddfloor> builder)
        {
            //builder.ToTable("newdamage_addfloor", "lms");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("FkNewSelf_AssessmentAddFloor_idx");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CarpetArea)
                .HasColumnName("carpet_area")
                .HasColumnType("decimal(4,2)");

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CurrentUse)
                .HasColumnName("current_use")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ElectricityNumber)
                .HasColumnName("Electricity_number")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FloorName)
                .HasColumnName("floor_name")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.MuncipleTaxId)
                .HasColumnName("munciple_tax_id")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.WaterBill)
                .HasColumnName("water_bill")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.GetDamageSelfAssessment)
                .WithMany(p => p.NewdamageAddfloor)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkNewSelf_AssessmentAddFloor");
        }
    }
}
