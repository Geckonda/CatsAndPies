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
    public class WalletConfigurationEntity : IEntityTypeConfiguration<WalletEntity>
    {
        public void Configure(EntityTypeBuilder<WalletEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Balance)
                .HasColumnType("money");

            builder
                .HasOne(x => x.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<WalletEntity>(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
