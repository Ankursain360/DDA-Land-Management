using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class DamagePayeeRegistrationConfiguration : IEntityTypeConfiguration<DamagePayeeRegistration>
    {
        public void Configure(EntityTypeBuilder<DamagePayeeRegistration> entity)
        {
            entity.ToTable("payeeregistration", "lms");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.EmailId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.IsVerified)
                .IsRequired()
                .HasColumnType("char(1)");

            entity.Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
        
        }
    }
}