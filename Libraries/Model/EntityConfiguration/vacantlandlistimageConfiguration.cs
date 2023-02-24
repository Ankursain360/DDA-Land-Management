using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class vacantlandlistimageConfiguration : IEntityTypeConfiguration<vacantlandlistimage>
    {
        public void Configure(EntityTypeBuilder<vacantlandlistimage> builder)
        {
            builder.HasIndex(e => e.vacantlandimageId)
                 .HasName("Fk_vacantlandimageId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.vacantlandimageId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ImagePath)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.vacantlandimage)
                .WithMany(p => p.vacantlandlistimages)
                .HasForeignKey(d => d.vacantlandimageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_vacantlandimageId");
        }
    }
}
