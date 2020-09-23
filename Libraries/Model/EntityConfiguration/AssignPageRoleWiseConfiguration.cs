using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AssignPageRoleWiseConfiguration:IEntityTypeConfiguration<AssignPageRoleWise>
    {
        public void Configure(EntityTypeBuilder<AssignPageRoleWise> builder)
        {

            builder.ToTable("assignpagerolewise");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModuleId)
                .HasColumnName("ModuleID")
                .HasColumnType("int(11)");

            builder.Property(e => e.PageId)
                .HasColumnName("PageID")
                .HasColumnType("int(11)");

            builder.Property(e => e.RAdd)
                .HasColumnName("R_Add")
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.RDelete)
                .HasColumnName("R_Delete")
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.RDisplay)
                .HasColumnName("R_Display")
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.REdit)
                .HasColumnName("R_Edit")
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.RView)
                .HasColumnName("R_View")
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");
        }
    }
}
