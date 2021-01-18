using System.Threading.Tasks;
using Accounts.Requests;
using Accounts.Responses;
using Communications.Business.Services.Abstractions;
using MassTransit;

namespace Communications.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRequestClient<GetAccount> _client;

        public AccountService(IRequestClient<GetAccount> client)
        {
            _client = client;
        }

        public async Task<bool> IsAccountValid(string accountId)
        {
            var (accountResponse, notFoundResponse) = await _client.GetResponse<AccountResponse, AccountNotFound>(new
            {
                Id = accountId,
                ShowDeleted = false
            });

            if (accountResponse.IsCompletedSuccessfully)
            {
                var account = await accountResponse;
                return account.Message.Enabled;
            }

            await notFoundResponse;
            return false;
        }

        public async Task<AccountResponse> GetAccount(string accountId)
        {
            var (accountResponse, notFoundResponse) = await _client.GetResponse<AccountResponse, AccountNotFound>(new
            {
                Id = accountId,
                ShowDeleted = false
            });

            if (accountResponse.IsCompletedSuccessfully)
            {
                var account = await accountResponse;
                return account.Message;
            }

            await notFoundResponse;
            return null;
        }
    }
}