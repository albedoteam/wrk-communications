namespace Communications.Business.Services.Abstractions
{
    using System.Threading.Tasks;
    using Models;

    public interface ICommunicationService
    {
        Task<SendResult> Send(Message message);
    }
}