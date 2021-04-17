namespace Communications.Business.Db
{
    using Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Models;

    public class ConfigurationRepository : BaseRepositoryWithAccount<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(
            IBaseRepository<Configuration> baseRepository,
            IHelpersWithAccount<Configuration> helpers) : base(baseRepository, helpers)
        {
        }
    }
}