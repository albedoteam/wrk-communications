using Accounts.Requests;
using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer;
using Communications.Business.Consumers.ConfigurationConsumers;
using Communications.Business.Consumers.MessageLogConsumers;
using Communications.Business.Consumers.TemplateConsumers;
using Communications.Business.Db;
using Communications.Business.Mappers;
using Communications.Business.Services;
using Communications.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Communications.Business
{
    public class Startup : IWorkerConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayerAccess(db =>
            {
                db.ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                db.DatabaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            });

            services.AddMappers();
            services.AddRepositories();
            services.AddServices();
            services.AddTransient<IJobRunner, JobConsumer>();

            services.AddBroker(
                configure => configure
                    .SetBrokerOptions(broker => broker.Host = configuration.GetValue<string>("BrokerOptions:Host")),
                consumers => consumers
                    .Add<ListConfigurationsRequestConsumer>()
                    .Add<GetConfigurationRequestConsumer>()
                    .Add<CreateConfigurationRequestConsumer>()
                    .Add<UpdateConfigurationsRequestConsumer>()
                    .Add<DeleteConfigurationRequestConsumer>()
                    .Add<ListMessageLogsRequestConsumer>()
                    .Add<ListTemplatesRequestConsumer>()
                    .Add<GetTemplateRequestConsumer>()
                    .Add<CreateTemplateRequestConsumer>()
                    .Add<UpdateTemplateRequestConsumer>()
                    .Add<DeleteTemplateRequestConsumer>(),
                queues => queues
                    .Map<MessageSent>(),
                clients => clients
                    .Add<GetAccountRequest>());
        }
    }
}