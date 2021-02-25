using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Commands;
using AlbedoTeam.Communications.Contracts.Common;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Services.Abstractions;
using Communications.Business.Services.Models;
using Markdig;
using MassTransit;

namespace Communications.Business.Consumers
{
    public class SendMessageConsumer : IConsumer<SendMessage>
    {
        private readonly IAccountService _accountService;
        private readonly ICommunicationService _communicationService;

        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMessageLogMapper _messageLogMapper;
        private readonly IMessageLogRepository _messageLogRepository;
        private readonly ITemplateRepository _templateRepository;

        public SendMessageConsumer(
            ICommunicationService communicationService,
            IAccountService accountService,
            ITemplateRepository templateRepository,
            IMessageLogRepository messageLogRepository,
            IMessageLogMapper messageLogMapper,
            IConfigurationRepository configurationRepository)
        {
            _communicationService = communicationService;
            _accountService = accountService;
            _templateRepository = templateRepository;
            _messageLogRepository = messageLogRepository;
            _messageLogMapper = messageLogMapper;
            _configurationRepository = configurationRepository;
        }

        public async Task Consume(ConsumeContext<SendMessage> context)
        {
            var message = await CreateMessage(context.Message);
            var sendResult = await _communicationService.Send(message);
            var messageLog = await PersistMessageLog(message, sendResult);

            if (sendResult.Success)
                await context.Publish(_messageLogMapper.MapModelToSentEvent(messageLog));
        }

        private async Task<Message> CreateMessage(SendMessage command)
        {
            if (!command.AccountId.IsValidObjectId())
                throw new InvalidCastException("The account ID does not have a valid ObjectId format");

            if (!command.TemplateId.IsValidObjectId())
                throw new InvalidCastException("The template ID does not have a valid ObjectId format");

            var account = await _accountService.GetAccount(command.AccountId);
            if (account is null || !account.Enabled)
                throw new InvalidOperationException($"Account invalid for id {command.AccountId}");

            var configurations = (await _configurationRepository.FilterBy(command.AccountId, c => c.Enabled)).ToList();

            if (configurations.SingleOrDefault() is null)
                throw new InvalidOperationException($"Configuration invalid for AccountId {command.AccountId}");

            var configuration = configurations.Single();

            var template = await _templateRepository.FindById(command.AccountId, command.TemplateId);
            if (template is null || !template.Enabled)
                throw new InvalidOperationException($"Template invalid for id {command.TemplateId}");

            var contract = configuration.Contracts.FirstOrDefault(c => c.MessageType == template.MessageType);
            if (contract is null)
                throw new InvalidOperationException($"Contract for message type {template.MessageType} not found");

            var message = new Message
            {
                Provider = configuration.Provider,
                Subject = command.Subject,
                From = contract.From,
                MessageType = template.MessageType,
                Content = await ParseTemplate(template, command.Parameters),
                Destinations = command.Destinations.ToDictionary(
                    destination => destination.Name,
                    destination => destination.Address)
            };

            return message;
        }

        private async Task<MessageLog> PersistMessageLog(Message message, SendResult sendResult)
        {
            var messageLog = _messageLogMapper.MapMessageToModel(message);

            if (sendResult.Success)
            {
                messageLog.Status = "Sent";
                messageLog.SentAt = DateTime.Now;
            }
            else
            {
                messageLog.Status = "Fail";
                messageLog.DetailMessage = sendResult.DetailMessage;
            }

            await _messageLogRepository.InsertOne(messageLog);
            return messageLog;
        }

        private static async Task<string> ParseTemplate(
            Template template,
            IEnumerable<IMessageParameter> messageParameters)
        {
            var content = messageParameters.Aggregate(
                template.ContentPattern,
                (current, parameter) => current.Replace($"${{{parameter.Key}}}", parameter.Value));

            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            var result = Markdown.ToHtml(content, pipeline);
            return await Task.FromResult(result);
        }
    }
}