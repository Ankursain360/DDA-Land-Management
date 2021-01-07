using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class ResRateListTypeAConfiguration : IEntityTypeConfiguration<Resratelisttypea>
    {
        public void Configure(EntityTypeBuilder<Resratelisttypea> builder)
        {
            builder.ToTable("resratelisttypea", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ColonyId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.EncroachId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Rate).HasColumnType("decimal(20,2)");

            builder.Property(e => e.SubEncroachId).HasColumnType("int(11)");
        }
    }
}