namespace Communications.Business.Mappers.Abstractions
{
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Communications.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using Models;
    using Models.SubDocuments;

    public interface IConfigurationMapper
    {
        Configuration MapRequestToModel(CreateConfiguration request);
        List<ConfigurationContract> MapRequestToModel(List<IConfigurationContract> request);
        ConfigurationResponse MapModelToResponse(Configuration model);
        List<ConfigurationResponse> MapModelToResponse(List<Configuration> modelList);
        QueryParams RequestToQuery(ListConfigurations request);
    }
}