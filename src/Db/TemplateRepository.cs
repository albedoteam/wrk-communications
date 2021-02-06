using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using Communications.Business.Db.Abstractions;
using Communications.Business.Models;

namespace Communications.Business.Db
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(IDbContext<Template> context) : base(context)
        {
        }
    }
}