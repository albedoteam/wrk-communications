using System;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.Enums;
using MassTransit;
using MongoDB.Driver;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class UpdateConfigurationsConsumer : IConsumer<UpdateConfiguration>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public UpdateConfigurationsConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UpdateConfiguration> context)
        {
            var configuration = await _repository.FindById(context.Message.Id);
            if (configuration is null)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = $"Configuration not found for id {context.Message.Id}"
                });
            }
            else
            {
                if (!Enum.TryParse<Provider>(context.Message.Provider, out var provider))
                    throw new Exception($"Provider {context.Message.Provider} is invalid");

                var contracts = _mapper.MapRequestToModel(context.Message.Contracts);

                var update = Builders<Configuration>.Update.Combine(
                    Builders<Configuration>.Update.Set(a => a.Name, context.Message.Name),
                    Builders<Configuration>.Update.Set(a => a.Provider, provider),
                    Builders<Configuration>.Update.Set(a => a.Contracts, contracts),
                    Builders<Configuration>.Update.Set(a => a.Enabled, context.Message.Enabled));

                await _repository.UpdateById(context.Message.Id, update);

                // get "updated" account
                configuration = await _repository.FindById(context.Message.Id);
                await context.RespondAsync(_mapper.MapModelToResponse(configuration));
            }
        }
    }
}