using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Communications.Business.Db
{
    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IMessageLogRepository, MessageLogRepository>();

            return services;
        }
    }
}