using System.Collections.Generic;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Requests;
using Communications.Responses;

namespace Communications.Business.Mappers.Abstractions
{
    public interface IConfigurationMapper
    {
        Configuration MapRequestToModel(CreateConfigurationRequest request);
        List<ConfigurationContract> MapRequestToModel(List<ConfigurationContractRequest> request);
        ConfigurationResponse MapModelToResponse(Configuration model);
        List<ConfigurationResponse> MapModelToResponse(List<Configuration> modelList);
    }
}