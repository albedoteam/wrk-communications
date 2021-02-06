using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Services.Abstractions;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class CreateTemplateConsumer : IConsumer<CreateTemplate>
    {
        private readonly IAccountService _accountService;
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public CreateTemplateConsumer(
            ITemplateMapper mapper,
            ITemplateRepository repository,
            IAccountService accountService)
        {
            _mapper = mapper;
            _repository = repository;
            _accountService = accountService;
        }

        public async Task Consume(ConsumeContext<CreateTemplate> context)
        {
            var isAccountValid = await _accountService.IsAccountValid(context.Message.AccountId);
            if (!isAccountValid)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.InvalidOperation,
                    ErrorMessage = $"Account invalid for id ${context.Message.AccountId}"
                });

                return;
            }

            var exists = (await _repository.FilterBy(t => t.Name.Equals(context.Message.Name))).Any();
            if (exists)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.AlreadyExists,
                    ErrorMessage = "Template already exists"
                });
                return;
            }

            var template = await _repository.InsertOne(_mapper.MapRequestToModel(context.Message));
            await context.RespondAsync(_mapper.MapModelToResponse(template));
        }
    }
}