using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using Communications.Business.Services.Abstractions;
using Communications.Business.Services.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Communications.Business.Services
{
    public class CommunicationService : ICommunicationService
    {
        public async Task<SendResult> Send(Message message)
        {
            if (message.Provider.Equals(Provider.Twillio) && message.MessageType == MessageType.Email)
                return await SendUsingTwillio(message);

            return await DontSend(message);
        }

        private static async Task<SendResult> SendUsingTwillio(Message message)
        {
            // todo externalize on some vault/secret
            const string apiKey = "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";

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(message.From.Address, message.From.Name);
            var subject = message.Subject;

            var tos = message.Destinations.Select(destination =>
                new EmailAddress(destination.Value, destination.Key)).ToList();

            var plainTextContent = message.Content;
            var htmlContent = message.Content;

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent,
                htmlContent);

            var response = await client.SendEmailAsync(msg);

            return new SendResult
            {
                Success = response.IsSuccessStatusCode,
                DetailMessage = response.StatusCode.ToString()
            };
        }

        private static async Task<SendResult> DontSend(Message message)
        {
            await Task.CompletedTask;
            return new SendResult
            {
                Success = false,
                DetailMessage =
                    $"Not implemented yet for provider {message.Provider} and message type {message.MessageType}"
            };
        }
    }
}