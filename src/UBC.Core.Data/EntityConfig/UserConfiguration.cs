using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UBC.Core.Domain.Entities;

namespace UBC.Core.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users", "dbo");

            builder
                .Property(p => p.Id)
                .HasColumnName("Id");

            builder
                .HasKey(p => p.Id)
                .HasAnnotation("SqlServer:Identity", "1, 1")
                .HasName("PK_Users");

            builder
                .Property(p => p.UserName)
                .HasColumnType("varchar(256)")
                .IsRequired()
                .HasColumnName("UserName");

            builder
                .Property(p => p.PasswordHash)
                .HasColumnType("varchar(Max)")
                .IsRequired()
                .HasColumnName("Password");

            builder
                .Ignore(s => s.Email);

            builder
                .Ignore(s => s.NormalizedEmail);

            builder
                .Ignore(s => s.NormalizedUserName);

            builder
                .Ignore(s => s.EmailConfirmed);

            builder
                .Ignore(s => s.SecurityStamp);

            builder
                .Ignore(s => s.ConcurrencyStamp);

            builder
                .Ignore(s => s.PhoneNumber);

            builder
                .Ignore(s => s.PhoneNumberConfirmed);

            builder
                .Ignore(s => s.TwoFactorEnabled);

            builder
                .Ignore(s => s.LockoutEnabled);

            builder
                .Ignore(s => s.AccessFailedCount);

            builder
                .Ignore(s => s.SecurityStamp);
        }
    }
}