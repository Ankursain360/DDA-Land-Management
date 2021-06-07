using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class Undersection17plotdetailConfiguration : IEntityTypeConfiguration<Undersection17plotdetail>
    {
        public void Configure(EntityTypeBuilder<Undersection17plotdetail> builder)
        {
            builder.ToTable("undersection17plotdetail", "lms");

            builder.HasIndex(e => e.KhasraId)
                    .HasName("fkkhasraid_idx");

            builder.HasIndex(e => e.UnderSection17Id)
                    .HasName("fkundersection17_idx");

            builder.HasIndex(e => e.VillageId)
                    .HasName("fkVillageId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

            builder.Property(e => e.UnderSection17Id).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                    .WithMany(p => p.Undersection17plotdetail)
                    .HasForeignKey(d => d.KhasraId)
                    .HasConstraintName("fk2khasraid");

            builder.HasOne(d => d.UnderSection17)
                    .WithMany(p => p.Undersection17plotdetail)
                    .HasForeignKey(d => d.UnderSection17Id)
                    .HasConstraintName("fk3undersectionid");

            builder.HasOne(d => d.Acquiredlandvillage)
                    .WithMany(p => p.Undersection17plotdetail)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("fk1villageid");
        }
    }
}
