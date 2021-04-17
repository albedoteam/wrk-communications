namespace Communications.Business.Mappers.Abstractions
{
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Communications.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using Models;
    using Models.SubDocuments;

    public interface ITemplateMapper
    {
        Template MapRequestToModel(CreateTemplate request);
        TemplateResponse MapModelToResponse(Template model);
        List<TemplateResponse> MapModelToResponse(List<Template> modelList);
        List<ContentParameter> MapRequestToModel(List<IContentParameter> request);
        QueryParams RequestToQuery(ListTemplates request);
    }
}