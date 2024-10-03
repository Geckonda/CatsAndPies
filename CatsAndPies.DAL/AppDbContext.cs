using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatsAndPies.Domain.Entities;
using CatsAndPies.DAL.Configurations;

namespace CatsAndPies.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<QuestionnaireEntity> Questionnairies => Set<QuestionnaireEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionnairiesConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
