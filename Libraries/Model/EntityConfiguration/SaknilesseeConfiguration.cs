using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    class SaknilesseeConfiguration : IEntityTypeConfiguration<Saknilessee>
    {
        public void Configure(EntityTypeBuilder<Saknilessee> builder)
        {
            //builder.ToTable("saknilessee", "lms");

            builder.HasIndex(e => e.SakniDetailId)
                .HasName("FKsaknidetailsId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FatherName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.LesseeMortgage)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LesseeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LesseeShare)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.SakniDetailId).HasColumnType("int(11)");

            builder.HasOne(d => d.SakniDetail)
                .WithMany(p => p.Saknilessee)
                .HasForeignKey(d => d.SakniDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKsaknidetailsId");

        }
    }
}
