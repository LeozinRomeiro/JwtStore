using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired(true);

            builder.Property(t => t.Image)
                .HasColumnName("Image")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(true);

            builder.OwnsOne(x=>x.Email)
                .Property(t => t.Address)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(t => t.Verification)
                .Property(x => x.Code)
                .HasColumnName("EmailVerificationCode")
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(t => t.Verification)
                .Property(x => x.VerifiedAt)
                .HasColumnName("EmailVerificationVerifiedAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(t => t.Verification)
                .Property(x => x.ExpireAt)
                .HasColumnName("EmailVerificationExpireAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(t => t.Verification)
                .Ignore(x => x.IsActive);

            builder.OwnsOne(x => x.Password)
                .Property(t => t.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.OwnsOne(x => x.Password)
                .Property(t => t.ResetCode)
                .HasColumnName("PasswordResetCode")
                .IsRequired();
        }
    }
}
