using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeasesubpurposeConfiguration : IEntityTypeConfiguration<Leasesubpurpose>
    {


        public void Configure(EntityTypeBuilder<Leasesubpurpose> builder)
        {
            builder.ToTable("leasesubpurpose", "lms");

            builder.HasIndex(e => e.PurposeUseId)
                .HasName("fkleasepurposeid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PurposeUseId).HasColumnType("int(11)");

            builder.Property(e => e.SubPurposeUse)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.PurposeUse)
                .WithMany(p => p.Leasesubpurpose)
                .HasForeignKey(d => d.PurposeUseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkleasepurposeid");
       
    }
    }
}
