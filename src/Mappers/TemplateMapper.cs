using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
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

                // model to response
                cfg.CreateMap<Template, TemplateResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // model to event
            });

            _mapper = config.CreateMapper();
        }
    }
}