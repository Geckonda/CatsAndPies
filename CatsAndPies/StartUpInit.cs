using CatsAndPies.DAL.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Services.Implementations;

namespace CatsAndPies
{
    public static class StartUpInit
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
        }

        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
