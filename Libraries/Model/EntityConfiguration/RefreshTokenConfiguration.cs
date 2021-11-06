
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refreshtoken");
            builder.Property(e => e.Id).HasColumnType("int(11)");
           
            //builder.Property(e => e.IsUsed).HasColumnType("tinyint");
            //builder.Property(e => e.IsRevorked).HasColumnType("tinyint");
            builder.HasIndex(e => e.UserId)
                .HasName("fk_aspnet_User_idx");

            builder.Property(e => e.JwtId)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Token).HasColumnType("longtext");
            builder.Property(e => e.AddedDate).HasColumnType("date");
            builder.Property(e => e.ExpiryDate).HasColumnType("date");
            builder.HasOne(d => d.User)
                .WithMany(p => p.RefreshToken)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_aspnet_User");
        }
    }
}