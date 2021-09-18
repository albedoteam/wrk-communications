namespace Communications.Business.Db.Abstractions
{
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Models;

    public interface IMessageLogRepository : IBaseRepositoryWithAccount<MessageLog>
    {
    }
}