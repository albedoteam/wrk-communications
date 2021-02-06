﻿using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;

namespace Communications.Business.Mappers
{
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

                // model to event
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
    }
}