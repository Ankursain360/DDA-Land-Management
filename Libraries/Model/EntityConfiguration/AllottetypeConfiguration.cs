using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class AllottetypeConfiguration : IEntityTypeConfiguration<Allottetype>
    {
        public void Configure(EntityTypeBuilder<Allottetype> builder)
        {
            //builder.ToTable("allottetype", "lms");

            builder.HasIndex(e => e.DamagePayeeRegisterTempId)
                .HasName("FKdamagePayeetempId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AtsgpadocumentPath)
                .HasColumnName("ATSGPADocumentPath")
                .HasColumnType("longtext");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePayeeRegisterTempId).HasColumnType("int(11)");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.DamagePayeeRegister)
                .WithMany(p => p.Allottetype)
                .HasForeignKey(d => d.DamagePayeeRegisterTempId)
                .HasConstraintName("FKdamagePayeetempId");



        }
    }
}
