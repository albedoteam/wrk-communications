namespace Communications.Business.Mappers
{
    using Abstractions;
    using Microsoft.Extensions.DependencyInjection;

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