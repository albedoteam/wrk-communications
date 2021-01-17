using System.Collections.Generic;
using Communications.Business.Models;
using Communications.Responses;

namespace Communications.Business.Mappers.Abstractions
{
    public interface IMessageLogMapper
    {
        List<MessageLogResponse> MapModelToResponse(List<MessageLog> toList);
    }
}