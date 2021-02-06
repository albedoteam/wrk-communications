using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class GetTemplateConsumer : IConsumer<GetTemplate>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public GetTemplateConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTemplate> context)
        {
            if (!context.Message.Id.IsValidObjectId())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.InvalidOperation,
                    ErrorMessage = "The template ID does not have a valid ObjectId format"
                });

            var template = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (template is null)
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Template not found"
                });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(template));
        }
    }
}