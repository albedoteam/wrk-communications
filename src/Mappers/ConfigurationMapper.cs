using System.Collections.Generic;
using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Requests;
using Communications.Responses;

namespace Communications.Business.Mappers
{
    public class ConfigurationMapper: IConfigurationMapper
    {
        private readonly IMapper _mapper;

        public ConfigurationMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Configuration, CreateConfigurationRequest>().ReverseMap();
                cfg.CreateMap<ConfigurationContract, ConfigurationContractRequest>().ReverseMap();

                // model to response
                cfg.CreateMap<Configuration, ConfigurationResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));
                
                cfg.CreateMap<ConfigurationContract, ConfigurationContractResponse>().ReverseMap();
                
                // model to event
            });

            _mapper = config.CreateMapper();
        }

        public Configuration MapRequestToModel(CreateConfigurationRequest request)
        {
            return _mapper.Map<CreateConfigurationRequest, Configuration>(request);
        }

        public List<ConfigurationContract> MapRequestToModel(List<ConfigurationContractRequest> request)
        {
            return _mapper.Map<List<ConfigurationContractRequest>, List<ConfigurationContract>>(request);
        }

        public ConfigurationResponse MapModelToResponse(Configuration model)
        {
            return _mapper.Map<Configuration, ConfigurationResponse>(model);
        }

        public List<ConfigurationResponse> MapModelToResponse(List<Configuration> modelList)
        {
            return _mapper.Map<List<Configuration>, List<ConfigurationResponse>>(modelList);
        }
    }
}