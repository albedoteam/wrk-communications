using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
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
            if (!context.Message.Id.IsValidObjectId())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.InvalidOperation,
                    ErrorMessage = "The configuration ID does not have a valid ObjectId format"
                });

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
                var contracts = _mapper.MapRequestToModel(context.Message.Contracts);

                var update = Builders<Configuration>.Update.Combine(
                    Builders<Configuration>.Update.Set(a => a.Name, context.Message.Name),
                    Builders<Configuration>.Update.Set(a => a.Provider, context.Message.Provider),
                    Builders<Configuration>.Update.Set(a => a.Contracts, contracts),
                    Builders<Configuration>.Update.Set(a => a.Enabled, context.Message.Enabled));

                await _repository.UpdateById(context.Message.Id, update);

                // get "updated" configuration
                configuration = await _repository.FindById(context.Message.Id);
                await context.RespondAsync(_mapper.MapModelToResponse(configuration));
            }
        }
    }
}