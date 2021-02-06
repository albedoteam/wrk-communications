using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using MassTransit;
using MongoDB.Driver;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class UpdateTemplateConsumer : IConsumer<UpdateTemplate>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public UpdateTemplateConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UpdateTemplate> context)
        {
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
                var contentParameters = _mapper.MapRequestToModel(context.Message.ContentParameters);

                var update = Builders<Template>.Update.Combine(
                    Builders<Template>.Update.Set(a => a.Name, context.Message.Name),
                    Builders<Template>.Update.Set(a => a.MessageType, context.Message.MessageType),
                    Builders<Template>.Update.Set(a => a.ContentType, context.Message.ContentType),
                    Builders<Template>.Update.Set(a => a.ContentPattern, context.Message.ContentPattern),
                    Builders<Template>.Update.Set(a => a.ContentParameters, contentParameters),
                    Builders<Template>.Update.Set(a => a.Enabled, context.Message.Enabled));

                await _repository.UpdateById(context.Message.Id, update);

                // get "updated" account
                template = await _repository.FindById(context.Message.Id);
                await context.RespondAsync(_mapper.MapModelToResponse(template));
            }
        }
    }
}