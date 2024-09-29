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
    internal class RolesConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasMany(x => x.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId);

            builder.HasData(
                new RoleEntity
                {
                    Id = 1,
                    Name = "Admin",
                },
                new RoleEntity
                {
                    Id = 2,
                    Name = "User"
                });
        }
    }
}
