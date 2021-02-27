using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{

    public class NewlandawardplotDetailsConfiguration : IEntityTypeConfiguration<Newlandawardplotdetails>
    {
        public void Configure(EntityTypeBuilder<Newlandawardplotdetails> builder)
        {
            builder.ToTable("newlandawardplotdetails", "lms");

            builder.HasIndex(e => e.AwardMasterId)
                    .HasName("AwardmasterId_idx");

            builder.HasIndex(e => e.KhasraId)
                .HasName("KhasraId_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("VillageId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AwardMasterId).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.NewlandAwardMaster)
                .WithMany(p => p.Newlandawardplotdetails)
                .HasForeignKey(d => d.AwardMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_awardmasteridfk56");

            builder.HasOne(d => d.NewlandKhasra)
                .WithMany(p => p.Newlandawardplotdetails)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_khasraid12");

            builder.HasOne(d => d.NewlandVillage)
                .WithMany(p => p.Newlandawardplotdetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_villageid11");


        }

       
    }
}
