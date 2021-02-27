using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Communications.Business.Models;

namespace Communications.Business.Db
{
    public class TemplateRepository : BaseRepositoryWithAccount<Template>, ITemplateRepository
    {
        public TemplateRepository(
            IBaseRepository<Template> baseRepository,
            IHelpersWithAccount<Template> helpers) : base(baseRepository, helpers)
        {
        }
    }
}