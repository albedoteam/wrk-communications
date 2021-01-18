using System.Collections.Generic;
using AutoMapper;
using Communications.Absctractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Requests;
using Communications.Responses;

namespace Communications.Business.Mappers
{
    public class TemplateMapper : ITemplateMapper
    {
        private readonly IMapper _mapper;

        public TemplateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Template, CreateTemplate>().ReverseMap();
                cfg.CreateMap<ContentParameter, IContentParameter>().ReverseMap();

                // model to response
                cfg.CreateMap<Template, TemplateResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // model to event
            });

            _mapper = config.CreateMapper();
        }

        public Template MapRequestToModel(CreateTemplate request)
        {
            return _mapper.Map<CreateTemplate, Template>(request);
        }

        public TemplateResponse MapModelToResponse(Template model)
        {
            return _mapper.Map<Template, TemplateResponse>(model);
        }

        public List<TemplateResponse> MapModelToResponse(List<Template> modelList)
        {
            return _mapper.Map<List<Template>, List<TemplateResponse>>(modelList);
        }

        public List<ContentParameter> MapRequestToModel(List<IContentParameter> request)
        {
            return _mapper.Map<List<IContentParameter>, List<ContentParameter>>(request);
        }
    }
}