namespace Communications.Business.Mappers.Abstractions
{
    using System.Collections.Generic;
    using AlbedoTeam.Communications.Contracts.Events;
    using AlbedoTeam.Communications.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using Models;
    using Services.Models;

    public interface IMessageLogMapper
    {
        List<MessageLogResponse> MapModelToResponse(List<MessageLog> toList);
        MessageLog MapMessageToModel(Message message);
        MessageSent MapModelToSentEvent(MessageLog messageLog);
        QueryParams RequestToQuery(ListMessageLogs request);
    }
}