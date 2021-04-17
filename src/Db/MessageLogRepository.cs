namespace Communications.Business.Db
{
    using Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Models;

    public class MessageLogRepository : BaseRepositoryWithAccount<MessageLog>, IMessageLogRepository
    {
        public MessageLogRepository(
            IBaseRepository<MessageLog> baseRepository,
            IHelpersWithAccount<MessageLog> helpers) : base(baseRepository, helpers)
        {
        }
    }
}