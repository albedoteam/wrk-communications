using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Communications.Business.Db
{
    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}