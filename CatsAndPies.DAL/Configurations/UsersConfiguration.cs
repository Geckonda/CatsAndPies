using CatsAndPies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(x => x.Login)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(x => x.Password)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.HasIndex(x => x.Login)
                .IsUnique();

            builder
                .HasOne(x => x.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(x => x.RoleId);

        }
    }
}
