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
    public class CatsConfiguration : IEntityTypeConfiguration<CatEntity>
    {
        public void Configure(EntityTypeBuilder<CatEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.AdoptedTime)
                .HasColumnType("date")
                .IsRequired();

            builder
                .HasOne(c => c.Color)
                .WithMany(c => c.Cats)
                .HasForeignKey(c => c.ColorId);


            builder
                .HasOne(c => c.Personality)
                .WithMany(p => p.Cats)
                .HasForeignKey(c => c.PersonalityId);


            builder
                .HasOne(x => x.Owner)
                .WithOne(ow => ow.Cat)
                .HasForeignKey<CatEntity>(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
