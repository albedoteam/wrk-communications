using System;
using System.Linq;
using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Services.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class CreateTemplateRequestConsumer: IConsumer<CreateTemplateRequest>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;
        private readonly IAccountService _accountService;

        public CreateTemplateRequestConsumer(
            ITemplateMapper mapper,
            ITemplateRepository repository,
            IAccountService accountService)
        {
            _mapper = mapper;
            _repository = repository;
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<CreateTemplateRequest> context)
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

            var template = await _repository.InsertOne(_mapper.MapRequestToModel(context.Message));
            await context.RespondAsync(_mapper.MapModelToResponse(template));
        }
    }
}