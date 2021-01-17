using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Communications.Business.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            // services.AddTransient<IAccountMapper, AccountMapper>();

            return services;
        }
    }
}