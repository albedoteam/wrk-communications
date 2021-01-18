using System.Threading.Tasks;
using Accounts.Responses;

namespace Communications.Business.Services.Abstractions
{
    public interface IAccountService
    {
        Task<bool> IsAccountValid(string accountId);

        Task<AccountResponse> GetAccount(string accountId);
    }
}