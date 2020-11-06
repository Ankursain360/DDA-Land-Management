using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class PlanningPropertiesConfiguration : IEntityTypeConfiguration<PlanningProperties>
    {
        public void Configure(EntityTypeBuilder<PlanningProperties> entity)
        {
            entity.ToTable("planningproperties");

            entity.HasIndex(e => e.PlanningId)
                .HasName("PlanningId_idx");

            entity.HasIndex(e => e.PropertyRegistrationId)
                .HasName("PropertyRegistrationId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.PlanningId).HasColumnType("int(11)");

            entity.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            entity.Property(e => e.PropertyType).HasColumnType("tinyint(4)");

            entity.HasOne(d => d.Planning)
                .WithMany(p => p.PlanningProperties)
                .HasForeignKey(d => d.PlanningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlanningId");

            entity.HasOne(d => d.PropertyRegistration)
                .WithMany(p => p.PlanningProperties)
                .HasForeignKey(d => d.PropertyRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PropertyRegistrationIdForPlanning");
        }
    }
}
