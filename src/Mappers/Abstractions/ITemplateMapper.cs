using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;

namespace Communications.Business.Mappers.Abstractions
{
    public interface ITemplateMapper
    {
        Template MapRequestToModel(CreateTemplate request);
        TemplateResponse MapModelToResponse(Template model);
        List<TemplateResponse> MapModelToResponse(List<Template> modelList);
        List<ContentParameter> MapRequestToModel(List<IContentParameter> request);
    }
}