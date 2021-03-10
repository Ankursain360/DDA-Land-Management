using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
   public class EncrocherPeopleConfiguration : IEntityTypeConfiguration<EncrocherPeople>
    {

        public void Configure(EntityTypeBuilder<EncrocherPeople> builder)
        {
            builder.ToTable("encrocherpeople", "lms");

            builder.HasIndex(e => e.EnchId)
                   .HasName("fkEnchId_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasColumnName("ADDRESS")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EnchId).HasColumnType("int(11)");

            builder.Property(e => e.FileNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.RecState).HasColumnType("tinyint(4)");

            builder.HasOne(d => d.Enchroachment)
                .WithMany(p => p.EncrocherPeople)
                .HasForeignKey(d => d.EnchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkEnchId");

        }
    }
   
}
