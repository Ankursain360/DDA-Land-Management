using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class DocumentcategoryConfiguration : IEntityTypeConfiguration<Documentcategory>
    {
        public void Configure(EntityTypeBuilder<Documentcategory> builder)
        {
           // builder.ToTable("documentcategory", "lms");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            builder.Property(e => e.CategoryName)
                .HasColumnName("categoryName")
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
        }
    }
}
