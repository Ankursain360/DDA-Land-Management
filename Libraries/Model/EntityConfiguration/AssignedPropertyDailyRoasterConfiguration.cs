using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AssignedPropertyDailyRoasterConfiguration : IEntityTypeConfiguration<AssignedPropertyDailyRoaster>
    {
        public void Configure(EntityTypeBuilder<AssignedPropertyDailyRoaster> entity)
        {
            //entity.ToTable("assignedpropertydailyroaster");

            entity.HasIndex(e => e.DailyRoasterId)
                .HasName("DailyRoasterId_idx");

            entity.HasIndex(e => e.PropertyRegistrationId)
                .HasName("DailyRoasterPropertyRegistrationId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.DailyRoasterId).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");
        }
    }
}
