using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class DeletedPropertyConfiguration : IEntityTypeConfiguration<Deletedproperty>
    {

        public void Configure(EntityTypeBuilder<Deletedproperty> builder)
        {
            builder.ToTable("deletedproperty", "lms");

            builder.HasIndex(e => e.PropertyRegistrationId)
                .HasName("fkPropertyRegistrationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DeletedBy).HasColumnType("int(11)");

            builder.Property(e => e.DeletedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsDeleted).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            builder.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);
        }
    }
}
