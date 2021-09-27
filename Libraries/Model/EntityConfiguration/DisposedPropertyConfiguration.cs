using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class DisposedPropertyConfiguration : IEntityTypeConfiguration<Disposedproperty>
    {

        public void Configure(EntityTypeBuilder<Disposedproperty> builder)
        {
            //builder.ToTable("disposedproperty", "lms");

            builder.HasIndex(e => e.PropertyRegistrationId)
                .HasName("fkDIsposedPropertyRegistrationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DisposalComments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.DisposalDate).HasColumnType("date");

            builder.Property(e => e.DisposalTypeFilePath).HasColumnType("longtext");

            builder.Property(e => e.DisposalTypeId).HasColumnType("int(11)");

            builder.Property(e => e.DisposedBy).HasColumnType("int(11)");

            builder.Property(e => e.DisposedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsDisposed).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            //builder.HasOne(d => d.PropertyRegistration)
            //    .WithMany(p => p.Disposedproperty)
            //    .HasForeignKey(d => d.PropertyRegistrationId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fkDIsposedPropertyRegistrationId");
        }
    }
}
