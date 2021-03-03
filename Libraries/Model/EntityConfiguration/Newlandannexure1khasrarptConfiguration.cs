using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class Newlandannexure1khasrarptConfiguration : IEntityTypeConfiguration<Newlandannexure1khasrarpt>
    {
    public void Configure(EntityTypeBuilder<Newlandannexure1khasrarpt> builder)
    {
            builder.ToTable("newlandannexure1khasrarpt", "lms");

            builder.HasIndex(e => e.NewLandAnnexure1Id)
                .HasName("FkNewLandAnnexure1Id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraNo)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NewLandAnnexure1Id).HasColumnType("int(11)");

            builder.Property(e => e.OwnerName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OwnershipStatus)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.NewLandAnnexure1)
                .WithMany(p => p.Newlandannexure1khasrarpt)
                .HasForeignKey(d => d.NewLandAnnexure1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkNewLandAnnexure1Id");


        }
    }
}
