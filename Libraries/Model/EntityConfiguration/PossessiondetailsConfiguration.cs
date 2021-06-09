using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
  public  class PossessiondetailsConfiguration : IEntityTypeConfiguration<Possessiondetails>
    {
        public void Configure(EntityTypeBuilder<Possessiondetails> builder)
        {



            builder.ToTable("possessiondetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkpkhasraid_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkpvillageid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PlotNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PossDate).HasColumnType("date");

            builder.Property(e => e.PossType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.DocumentName)
               .HasMaxLength(1000)
               .IsUnicode(false);

            builder.Property(e => e.ReasonNonPoss)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(4000)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Possessiondetails)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkpkhasraid");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Possessiondetails)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fkpvillageid");

          
        }
        }
    }
