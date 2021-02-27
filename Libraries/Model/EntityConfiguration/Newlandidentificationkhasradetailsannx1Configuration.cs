//using Libraries.Model.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Libraries.Model.Entity
//{
//    public class Newlandidentificationkhasradetailsannx1Configuration : IEntityTypeConfiguration<Newlandidentificationkhasradetailsannx1>
    
//    {
//        public void Configure(EntityTypeBuilder<Newlandidentificationkhasradetailsannx1> builder)
//        {
//            builder.ToTable("newlandidentificationkhasradetailsannx1", "lms");

//            builder.HasIndex(e => e.Identificationid)
//                .HasName("fk_Indentification_idx");

//            builder.Property(e => e.Id)
//                        .HasColumnName("id")
//                        .HasColumnType("int(11)");

//            builder.Property(e => e.AreaBigha).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaBiswa).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.AreaBiswani).HasColumnType("decimal(18,3)");

//            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

//            builder.Property(e => e.Identificationid).HasColumnType("int(11)");

//            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

//            builder.Property(e => e.KhasaNo)
//                        .IsRequired()
//                        .HasMaxLength(45)
//                        .IsUnicode(false);

//            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

//            builder.Property(e => e.OwnerName)
//                        .HasMaxLength(100)
//                        .IsUnicode(false);

//            builder.Property(e => e.OwnershipStatus)
//                        .IsRequired()
//                        .HasMaxLength(30)
//                        .IsUnicode(false);

//            builder.HasOne(d => d.Identification)
//                        .WithMany(p => p.Newlandidentificationkhasradetailsannx1)
//                        .HasForeignKey(d => d.Identificationid)
//                        .OnDelete(DeleteBehavior.ClientSetNull)
//                        .HasConstraintName("fk_Indentification");
//        }
//    }
//}
