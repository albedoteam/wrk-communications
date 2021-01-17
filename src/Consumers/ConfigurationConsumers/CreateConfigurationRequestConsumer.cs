using System;
using System.Linq;
using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Services.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class CreateConfigurationRequestConsumer : IConsumer<CreateConfigurationRequest>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;
        private readonly IAccountService _accountService;

        public CreateConfigurationRequestConsumer(
            IConfigurationMapper mapper,
            IConfigurationRepository repository,
            IAccountService accountService)
        {
            _mapper = mapper;
            _repository = repository;
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<CreateConfigurationRequest> context)
        {
            var isAccountValid = await _accountService.IsAccountValid(context.Message.AccountId);
            if (!isAccountValid)
                throw new Exception($"Account invalid for id ${context.Message.AccountId}");

            var exists = (await _repository.FilterBy(t => t.Name.Equals(context.Message.Name))).Any();
            if (exists)
            {
                await context.RespondAsync<ConfigurationExists>(new { });
                return;
            }

            var configuration = await _repository.InsertOne(_mapper.MapRequestToModel(context.Message));
            await context.RespondAsync(_mapper.MapModelToResponse(configuration));
        }
    }
}