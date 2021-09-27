using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
   
    class Undersection22plotdetailsConfiguration : IEntityTypeConfiguration<Undersection22plotdetails>
    {
        public void Configure(EntityTypeBuilder<Undersection22plotdetails> builder)
        {

            //builder.ToTable("undersection22plotdetails", "lms");

            builder.HasIndex(e => e.AcquiredlandvillageId)
                .HasName("fkAcqlandVillageid_idx");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkKhasraid_idx");

            builder.HasIndex(e => e.UnderSection22Id)
                .HasName("fkus22id_idx");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.Acquiredlandvillage)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.AcquiredlandvillageId)
                .HasConstraintName("fkAcqlandVillageid");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkKhasraid");

            builder.HasOne(d => d.UnderSection22)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.UnderSection22Id)
                .HasConstraintName("fkus22id");





        }
    }
}
