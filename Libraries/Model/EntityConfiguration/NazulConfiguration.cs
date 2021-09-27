using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class NazulConfiguration : IEntityTypeConfiguration<Nazul>
    {

        public void Configure(EntityTypeBuilder<Nazul> entity)
        {
            //entity.ToTable("nazul", "lms");

            entity.HasIndex(e => e.VillageId)
                .HasName("fkNazulvillageId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Bigha).HasColumnType("int(11)");

            entity.Property(e => e.Biswa).HasColumnType("int(11)");

            entity.Property(e => e.Biswanshi).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.DateOfNotification).HasColumnType("date");

            entity.Property(e => e.DocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.DocumentNameSizra)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.VillageId).HasColumnType("int(11)");

            entity.HasOne(d => d.Village)
                .WithMany(p => p.Nazul)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNazulvillageId_idx");
        }
    }
}

