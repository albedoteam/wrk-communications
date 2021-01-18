using System.Collections.Generic;
using Communications.Business.Models;
using Communications.Business.Services.Abstractions;
using Communications.Events;
using Communications.Responses;

namespace Communications.Business.Mappers.Abstractions
{
    public interface IMessageLogMapper
    {
        List<MessageLogResponse> MapModelToResponse(List<MessageLog> toList);
        MessageLog MapMessageToModel(Message message);
        MessageSent MapModelToSentEvent(MessageLog messageLog);
    }
}