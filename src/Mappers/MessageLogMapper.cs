using System.Collections.Generic;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Events;
using AlbedoTeam.Communications.Contracts.Responses;
using AutoMapper;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using Communications.Business.Models.SubDocuments;
using Communications.Business.Services.Models;

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
                cfg.CreateMap<IDestinationAddress, DestinationAddress>().ReverseMap();

                cfg.CreateMap<MessageLog, MessageSent>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // message service
                cfg.CreateMap<Message, MessageLog>(MemberList.Destination)
                    .ForMember(m => m.Destinations, opt => opt.Ignore())
                    .AfterMap((src, dest) =>
                    {
                        dest.Destinations = new List<DestinationAddress>();
                        foreach (var (key, value) in src.Destinations)
                            dest.Destinations.Add(new DestinationAddress
                            {
                                DestinationType = src.MessageType == MessageType.Email
                                    ? DestinationType.Email
                                    : DestinationType.Phone,
                                Name = key,
                                Address = value
                            });
                    });
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