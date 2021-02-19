using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class SaknikhasraConfiguration : IEntityTypeConfiguration<Saknikhasra>
    {
        public void Configure(EntityTypeBuilder<Saknikhasra> builder)
        {
            builder.ToTable("saknikhasra", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fksaknikhId_idx");

            builder.HasIndex(e => e.SakniDetailId)
                .HasName("FKsaknikhasra_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AreaSqYard)
                .HasColumnName("AreaSqYard")
                .HasColumnType("decimal(18,3)");

            builder.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.LeaseAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PlotNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.RenewalDate).HasColumnType("date");

            builder.Property(e => e.SakniDetailId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Saknikhasra)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fksaknikhId");

            builder.HasOne(d => d.SakniDetail)
                .WithMany(p => p.Saknikhasra)
                .HasForeignKey(d => d.SakniDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkkhsakni");


        }
    }
    
}
