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
            builder.ToTable("undersection22plotdetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkKhasraid_idx");

            builder.HasIndex(e => e.AcquiredlandvillageId)
                .HasName("fkAcqlandVillageid_idx");

            builder.HasIndex(e => e.UnderSection17Id)
                .HasName("fkus17id_idx");

            builder.HasIndex(e => e.UnderSection22Id)
                .HasName("fkus22id_idx");

            builder.HasIndex(e => e.UnderSection4Id)
                .HasName("fkus4id_idx");

            builder.HasIndex(e => e.UnderSection6Id)
                .HasName("fkus6id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.AcquiredlandvillageId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.UnderSection17Id).HasColumnType("int(11)");

            builder.Property(e => e.UnderSection22Id).HasColumnType("int(11)");

            builder.Property(e => e.UnderSection4Id).HasColumnType("int(11)");

            builder.Property(e => e.UnderSection6Id).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkKhasraid");

            builder.HasOne(d => d.Acquiredlandvillage)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.AcquiredlandvillageId)
                .HasConstraintName("fkAcqlandVillageid");

            builder.HasOne(d => d.UnderSection17)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.UnderSection17Id)
                .HasConstraintName("fkus17id");

            builder.HasOne(d => d.UnderSection22)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.UnderSection22Id)
                .HasConstraintName("fkus22id");

            builder.HasOne(d => d.UnderSection4)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.UnderSection4Id)
                .HasConstraintName("fkus4id");

            builder.HasOne(d => d.UnderSection6)
                .WithMany(p => p.Undersection22plotdetails)
                .HasForeignKey(d => d.UnderSection6Id)
                .HasConstraintName("fkus6id");




        }
    }
}
