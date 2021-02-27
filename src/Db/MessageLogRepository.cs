using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Communications.Business.Models;

namespace Communications.Business.Db
{
    public class MessageLogRepository : BaseRepositoryWithAccount<MessageLog>, IMessageLogRepository
    {
        public MessageLogRepository(
            IBaseRepository<MessageLog> baseRepository,
            IHelpersWithAccount<MessageLog> helpers) : base(baseRepository, helpers)
        {
        }
    }
}