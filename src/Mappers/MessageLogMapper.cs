using System.Collections.Generic;
using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Services.Abstractions;
using Communications.Events;
using Communications.Responses;

namespace Communications.Business.Mappers
{
    public class MessageLogMapper : IMessageLogMapper
    {
        private readonly IMapper _mapper;

        public MessageLogMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model

                // model to response
                cfg.CreateMap<MessageLog, MessageLogResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // model to event
                cfg.CreateMap<MessageLog, MessageSent>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // message service
                cfg.CreateMap<Message, MessageLog>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public List<MessageLogResponse> MapModelToResponse(List<MessageLog> toList)
        {
            return _mapper.Map<List<MessageLog>, List<MessageLogResponse>>(toList);
        }

        public MessageLog MapMessageToModel(Message message)
        {
            return _mapper.Map<Message, MessageLog>(message);
        }

        public MessageSent MapModelToSentEvent(MessageLog messageLog)
        {
            return _mapper.Map<MessageLog, MessageSent>(messageLog);
        }
    }
}