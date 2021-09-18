namespace Communications.Business
{
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Events;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
    using AlbedoTeam.Sdk.MessageConsumer;
    using AlbedoTeam.Sdk.MessageConsumer.Configuration;
    using Consumers;
    using Consumers.ConfigurationConsumers;
    using Consumers.MessageLogConsumers;
    using Consumers.TemplateConsumers;
    using Db;
    using Mappers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Startup : IWorkerConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayerAccess(db =>
            {
                db.ConnectionString = configuration.GetValue<string>("DatabaseSettings_ConnectionString");
                db.DatabaseName = configuration.GetValue<string>("DatabaseSettings_DatabaseName");
            });

            services.AddMappers();
            services.AddRepositories();
            services.AddServices();
            services.AddTransient<IJobRunner, JobConsumer>();

            services.AddBroker(
                configure =>
                {
                    configure.SetBrokerOptions(broker =>
                    {
                        broker.HostOptions = new HostOptions
                        {
                            Host = configuration.GetValue<string>("Broker_Host"),
                            HeartbeatInterval = 10,
                            RequestedChannelMax = 40,
                            RequestedConnectionTimeout = 60000
                        };

                        broker.KillSwitchOptions = new KillSwitchOptions
                        {
                            ActivationThreshold = 10,
                            TripThreshold = 0.15,
                            RestartTimeout = 60
                        };

                        broker.PrefetchCount = 1;
                    });
                },
                consumers =>
                {
                    // configurations
                    consumers
                        .Add<ListConfigurationsConsumer>()
                        .Add<GetConfigurationConsumer>()
                        .Add<CreateConfigurationConsumer>()
                        .Add<UpdateConfigurationsConsumer>()
                        .Add<DeleteConfigurationConsumer>();

                    // message logs
                    consumers
                        .Add<ListMessageLogsConsumer>();

                    // templates
                    consumers
                        .Add<ListTemplatesConsumer>()
                        .Add<GetTemplateConsumer>()
                        .Add<CreateTemplateConsumer>()
                        .Add<UpdateTemplateConsumer>()
                        .Add<DeleteTemplateConsumer>();

                    // message sender
                    consumers
                        .Add<SendMessageConsumer>();
                },
                queues => queues
                    .Map<MessageSent>(),
                clients => clients
                    .Add<GetAccount>());
        }
    }
}