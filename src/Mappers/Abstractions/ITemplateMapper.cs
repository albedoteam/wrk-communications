using System.Collections.Generic;
using Communications.Absctractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Requests;
using Communications.Responses;

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