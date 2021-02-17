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

            builder.HasIndex(e => e.NAME)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.EnchId).HasColumnType("int(11)");


            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

          //  builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.NAME)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.ADDRESS)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.RecState)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);
            builder.Property(e => e.FileNo)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);
        }
    }
   
}
