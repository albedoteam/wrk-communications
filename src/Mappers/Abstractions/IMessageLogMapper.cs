using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Events;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Models;
using Communications.Business.Services.Models;

namespace Communications.Business.Mappers.Abstractions
{
    public interface IMessageLogMapper
    {
        List<MessageLogResponse> MapModelToResponse(List<MessageLog> toList);
        MessageLog MapMessageToModel(Message message);
        MessageSent MapModelToSentEvent(MessageLog messageLog);
    }
}