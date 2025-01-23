using CatsAndPies.Domain.Entities.PiesTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    public class PiesConfiguration : IEntityTypeConfiguration<PieEntity>
    {
        public void Configure(EntityTypeBuilder<PieEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
               .HasColumnType("varchar(100)")
               .IsRequired();
            builder.Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(x => x.ImgLink)
                .HasColumnType("varchar(255)");

        }
    }
}
