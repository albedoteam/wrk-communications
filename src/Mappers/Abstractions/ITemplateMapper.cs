﻿using Communications.Business.Models;
using Communications.Requests;
using Communications.Responses;

namespace Communications.Business.Mappers.Abstractions
{
    public interface ITemplateMapper
    {
        Template MapRequestToModel(CreateTemplateRequest request);
        TemplateResponse MapModelToResponse(Template model);
    }
}