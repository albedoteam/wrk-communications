using System.Threading.Tasks;
using Communications.Business.Models.Enums;
using Communications.Business.Services.Abstractions;

namespace Communications.Business.Services
{
    public class CommunicationService : ICommunicationService
    {
        public async Task<SendResult> Send(Message message)
        {
            // todo implementar a integração de fato

            if (!message.Provider.Equals(Provider.Twillio))
                return new SendResult
                {
                    Success = false,
                    DetailMessage = "Invalid provider"
                };

            await Task.CompletedTask;
            return new SendResult
            {
                Success = false,
                DetailMessage = "Not implemented yet"
            };
        }
    }
}