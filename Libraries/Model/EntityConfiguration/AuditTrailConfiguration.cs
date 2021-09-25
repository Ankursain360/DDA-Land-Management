using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Model.EntityConfiguration
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditModel>
    {
        public void Configure(EntityTypeBuilder<AuditModel> builder)
        {

            builder.ToTable("auditmodel", "lms");

            builder.Property(e => e.Id)
                 .HasColumnName("id")
                 .HasColumnType("int(11)");

            builder.Property(e => e.ActionName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Area)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ControllerName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IpAddress)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.AuthorizationToken)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.Userbrowser)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.LoggedInAt)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LoggedOutAt)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LoginStatus)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PageAccessed)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RoleId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SessionId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.UserId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.UrlReferrer)
                .HasMaxLength(2000)
                .IsUnicode(false);



        }
    }
}
