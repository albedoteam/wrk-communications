using System;
using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.Enums;
using Communications.Requests;
using Communications.Responses;
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
                await context.RespondAsync<TemplateNotFound>(context.Message);
            }
            else
            {
                if (!Enum.TryParse<MessageType>(context.Message.MessageType, out var messageType))
                    throw new Exception($"MessageType {context.Message.MessageType} is invalid");

                if (!Enum.TryParse<ContentType>(context.Message.ContentType, out var contentType))
                    throw new Exception($"ContentType {context.Message.ContentType} is invalid");

                var contentParameters = _mapper.MapRequestToModel(context.Message.ContentParameters);

                var update = Builders<Template>.Update.Combine(
                    Builders<Template>.Update.Set(a => a.Name, context.Message.Name),
                    Builders<Template>.Update.Set(a => a.MessageType, messageType),
                    Builders<Template>.Update.Set(a => a.ContentType, contentType),
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