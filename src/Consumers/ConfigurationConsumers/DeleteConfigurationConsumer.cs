﻿using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using MassTransit;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class DeleteConfigurationConsumer : IConsumer<DeleteConfiguration>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public DeleteConfigurationConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteConfiguration> context)
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
                await _repository.DeleteById(context.Message.Id);

                // get "soft-deleted"
                configuration = await _repository.FindById(context.Message.Id, true);

                await context.RespondAsync(_mapper.MapModelToResponse(configuration)); // respond async
            }
        }
    }
}