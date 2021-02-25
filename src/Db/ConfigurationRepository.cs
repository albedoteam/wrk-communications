using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Communications.Business.Models;

namespace Communications.Business.Db
{
    public class ConfigurationRepository : BaseRepositoryWithAccount<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IBaseRepository<Configuration> baseRepository) : base(baseRepository)
        {
        }
    }
}