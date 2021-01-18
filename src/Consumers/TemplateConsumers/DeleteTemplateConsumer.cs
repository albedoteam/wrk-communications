using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
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
            var template = await _repository.FindById(context.Message.Id);
            if (template is null)
            {
                await context.RespondAsync<TemplateNotFound>(new { });
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