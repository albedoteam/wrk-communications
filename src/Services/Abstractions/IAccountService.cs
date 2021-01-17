using System.Threading.Tasks;

namespace Communications.Business.Services.Abstractions
{
    public interface IAccountService
    {
        Task<bool> IsAccountValid(string accountId);
    }
}