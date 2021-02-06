using System.Threading.Tasks;
using Communications.Business.Services.Models;

namespace Communications.Business.Services.Abstractions
{
    public interface ICommunicationService
    {
        Task<SendResult> Send(Message message);
    }
}