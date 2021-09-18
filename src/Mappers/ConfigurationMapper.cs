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

    public class ConfigurationMapper : IConfigurationMapper
    {
        private readonly IMapper _mapper;

        public ConfigurationMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Configuration, CreateConfiguration>().ReverseMap();
                cfg.CreateMap<ConfigurationContract, IConfigurationContract>().ReverseMap();
                cfg.CreateMap<FromAddress, IFromAddress>().ReverseMap();

                // model to response
                cfg.CreateMap<Configuration, ConfigurationResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // request -> query
                cfg.CreateMap<ListConfigurations, QueryParams>(MemberList.Destination)
                    .ForMember(l => l.Sorting, opt => opt.MapFrom(o => o.Sorting.ToString()));
            });

            _mapper = config.CreateMapper();
        }

        public Configuration MapRequestToModel(CreateConfiguration request)
        {
            return _mapper.Map<CreateConfiguration, Configuration>(request);
        }

        public List<ConfigurationContract> MapRequestToModel(List<IConfigurationContract> request)
        {
            return _mapper.Map<List<IConfigurationContract>, List<ConfigurationContract>>(request);
        }

        public ConfigurationResponse MapModelToResponse(Configuration model)
        {
            return _mapper.Map<Configuration, ConfigurationResponse>(model);
        }

        public List<ConfigurationResponse> MapModelToResponse(List<Configuration> modelList)
        {
            return _mapper.Map<List<Configuration>, List<ConfigurationResponse>>(modelList);
        }

        public QueryParams RequestToQuery(ListConfigurations request)
        {
            return _mapper.Map<ListConfigurations, QueryParams>(request);
        }
    }
}