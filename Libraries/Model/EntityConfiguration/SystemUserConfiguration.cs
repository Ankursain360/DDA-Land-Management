using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
        }
    }
}