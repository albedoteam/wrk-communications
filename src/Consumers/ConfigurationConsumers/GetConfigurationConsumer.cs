﻿using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class GetConfigurationConsumer : IConsumer<GetConfiguration>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public GetConfigurationConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetConfiguration> context)
        {
            var configuration = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (configuration is null)
                await context.RespondAsync<ConfigurationNotFound>(new { });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(configuration));
        }
    }
}