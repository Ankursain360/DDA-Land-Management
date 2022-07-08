using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class New_Damage_ColonyConfiguration : IEntityTypeConfiguration<New_Damage_Colony>
    {
        public void Configure(EntityTypeBuilder<New_Damage_Colony> builder)
        {
            //builder.ToTable("newdamagecolony", "lms");

            builder.HasIndex(e => e.NewDamageVillageId)
                    .HasName("Fkdamageccolony_idx");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DamageColonycol)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.acquiredlandvillage)
                .WithMany(p => p.NewDamageColony)
                .HasForeignKey(d => d.NewDamageVillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fkdamageccolony");
        }
    }
}
