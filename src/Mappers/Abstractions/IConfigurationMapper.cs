using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;

namespace Communications.Business.Mappers.Abstractions
{
    public interface IConfigurationMapper
    {
        Configuration MapRequestToModel(CreateConfiguration request);
        List<ConfigurationContract> MapRequestToModel(List<IConfigurationContract> request);
        ConfigurationResponse MapModelToResponse(Configuration model);
        List<ConfigurationResponse> MapModelToResponse(List<Configuration> modelList);
    }
}