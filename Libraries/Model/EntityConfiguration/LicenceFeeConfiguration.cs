using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LicenceFeeConfiguration : IEntityTypeConfiguration<Licencefees>
    {


        public void Configure(EntityTypeBuilder<Licencefees> entity)
        {
            entity.ToTable("licencefees", "lms");

            entity.HasIndex(e => e.PropertyTypeId)
                .HasName("fklicencefee_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.FromDate).HasColumnType("date");

            entity.Property(e => e.IsActive)
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("1");

            entity.Property(e => e.LicenceFees)
                        .HasColumnName("LicenceFees")
                        .HasColumnType("decimal(18,3)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.PropertyTypeId).HasColumnType("int(11)");

            entity.Property(e => e.ToDate).HasColumnType("date");

            entity.HasOne(d => d.PropertyType)
                        .WithMany(p => p.Licencefees)
                        .HasForeignKey(d => d.PropertyTypeId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fklicencefee");

        }
    }
}