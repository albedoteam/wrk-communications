namespace Communications.Business.Db
{
    using Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Models;

    public class TemplateRepository : BaseRepositoryWithAccount<Template>, ITemplateRepository
    {
        public TemplateRepository(
            IBaseRepository<Template> baseRepository,
            IHelpersWithAccount<Template> helpers) : base(baseRepository, helpers)
        {
        }
    }
}