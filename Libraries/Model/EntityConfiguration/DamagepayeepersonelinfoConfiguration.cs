﻿using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DamagepayeepersonelinfoConfiguration : IEntityTypeConfiguration<Damagepayeepersonelinfo>
    {
        public void Configure(EntityTypeBuilder<Damagepayeepersonelinfo> builder)
        {
            //builder.ToTable("damagepayeepersonelinfo", "lms");

            builder.HasIndex(e => e.DamagePayeeRegisterTempId)
                .HasName("FkDamagePayeeIdtemp_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AadharNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AadharNoFilePath).HasColumnType("longtext");

            builder.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamagePayeeRegisterTempId).HasColumnType("int(11)");

            builder.Property(e => e.EmailId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PanNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PanNoFilePath).HasColumnType("longtext");

            builder.Property(e => e.PhotographPath).HasColumnType("longtext");

            builder.Property(e => e.SignaturePath).HasColumnType("longtext");

            builder.Property(e => e.OtherDocPath).HasColumnType("longtext");
            

            builder.HasOne(d => d.DamagePayeeRegister)
                .WithMany(p => p.Damagepayeepersonelinfo)
                .HasForeignKey(d => d.DamagePayeeRegisterTempId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkDamagePayeeIdtemp");


        }
    }
}
