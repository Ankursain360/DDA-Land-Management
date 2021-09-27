using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
  
    class RestorepropertyConfiguration : IEntityTypeConfiguration<Restoreproperty>
    {

        public void Configure(EntityTypeBuilder<Restoreproperty> builder)
        {

            //builder.ToTable("restoreproperty", "lms");

            builder.HasIndex(e => e.PropertyRegistrationId)
                .HasName("fkPropertyRegistration_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            builder.Property(e => e.RestoreBy).HasColumnType("int(11)");

            builder.Property(e => e.RestoreDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.RestoreReason)
                .IsRequired()
                .HasMaxLength(400)
                .IsUnicode(false);
        }
    }
}
