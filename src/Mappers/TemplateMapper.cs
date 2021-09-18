namespace Communications.Business.Mappers
{
    using System.Collections.Generic;
    using Abstractions;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Communications.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using AutoMapper;
    using Models;
    using Models.SubDocuments;

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

                // request -> query
                cfg.CreateMap<ListTemplates, QueryParams>(MemberList.Destination)
                    .ForMember(l => l.Sorting, opt => opt.MapFrom(o => o.Sorting.ToString()));
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

        public QueryParams RequestToQuery(ListTemplates request)
        {
            return _mapper.Map<ListTemplates, QueryParams>(request);
        }
    }
}