using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Communications.Business.Models;

namespace Communications.Business.Db
{
    public class MessageLogRepository : BaseRepository<MessageLog>, IMessageLogRepository
    {
        public MessageLogRepository(IDbContext<MessageLog> context) : base(context)
        {
        }
    }
}