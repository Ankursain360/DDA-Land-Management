using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
  public  class LeasesignupConfigurations : IEntityTypeConfiguration<Leasesignup>
    {
        public void Configure(EntityTypeBuilder<Leasesignup> builder)
        {

            //builder.ToTable("leasesignup", "lms");

            builder.HasIndex(e => e.KycId)
                .HasName("fkKycId_idx");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KycId).HasColumnType("int(11)");

            builder.Property(e => e.KycStatus)
                .HasColumnType("char(1)")
                .HasDefaultValueSql("F");

            builder.Property(e => e.MobileNo)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Kyc)
                .WithMany(p => p.Leasesignup)
                .HasForeignKey(d => d.KycId)
                .HasConstraintName("fkKycId");




        }
        }
    }
