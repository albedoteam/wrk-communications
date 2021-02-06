using Communications.Business.Mappers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Communications.Business.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationMapper, ConfigurationMapper>();
            services.AddTransient<ITemplateMapper, TemplateMapper>();
            services.AddTransient<IMessageLogMapper, MessageLogMapper>();

            return services;
        }
    }
}