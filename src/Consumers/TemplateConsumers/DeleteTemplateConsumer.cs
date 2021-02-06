using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class DeleteTemplateConsumer : IConsumer<DeleteTemplate>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public DeleteTemplateConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteTemplate> context)
        {
            if (!context.Message.Id.IsValidObjectId())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.InvalidOperation,
                    ErrorMessage = "The template ID does not have a valid ObjectId format"
                });

            var template = await _repository.FindById(context.Message.Id);
            if (template is null)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Template not found"
                });
            }
            else
            {
                await _repository.DeleteById(context.Message.Id);

                // get "soft-deleted"
                template = await _repository.FindById(context.Message.Id, true);

                await context.RespondAsync(_mapper.MapModelToResponse(template)); // respond async
            }
        }
    }
}