using System.Collections.Generic;
using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Requests;
using Communications.Responses;

namespace Communications.Business.Mappers
{
    public class TemplateMapper: ITemplateMapper
    {
        private readonly IMapper _mapper;

        public TemplateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Template, CreateTemplateRequest>().ReverseMap();
                cfg.CreateMap<ContentParameter, ContentParameterRequest>().ReverseMap();

                // model to response
                cfg.CreateMap<Template, TemplateResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));
                
                cfg.CreateMap<ContentParameter, ContentParameterResponse>().ReverseMap();

                // model to event
            });

            _mapper = config.CreateMapper();
        }

        public Template MapRequestToModel(CreateTemplateRequest request)
        {
            return _mapper.Map<CreateTemplateRequest, Template>(request);
        }

        public TemplateResponse MapModelToResponse(Template model)
        {
            return _mapper.Map<Template, TemplateResponse>(model);
        }

        public List<TemplateResponse> MapModelToResponse(List<Template> modelList)
        {
            return _mapper.Map<List<Template>, List<TemplateResponse>>(modelList);
        }
    }
}