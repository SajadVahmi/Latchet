using Latchet.Application.Commands;
using Latchet.Application.Events;
using Latchet.Utilities.Configurations;
using Latchet.Utilities.Services.JsonSerializers;
using Latchet.Utilities.Services.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Latchet.Messaging.IdempotentConsumers
{
    public class IdempotentMessageConsumer : IMessageConsumer
    {
        private readonly LatchetConfigurations latchetConfiguration;
        private readonly IEventDispatcher eventDispatcher;
        private readonly IJsonSerializer jsonSerializer;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IMessageInboxItemRepository _messageInboxItemRepository;
        private readonly Dictionary<string, string> _messageTypeMap = new Dictionary<string, string>();
        public IdempotentMessageConsumer(LatchetConfigurations hamoonConfigurations, IEventDispatcher eventDispatcher, IJsonSerializer jsonSerializer, ICommandDispatcher commandDispatcher, IMessageInboxItemRepository messageInboxItemRepository)
        {
            latchetConfiguration = hamoonConfigurations;
            this.eventDispatcher = eventDispatcher;
            this.jsonSerializer = jsonSerializer;
            this.commandDispatcher = commandDispatcher;
            _messageInboxItemRepository = messageInboxItemRepository;
            LoadMessageMap();
        }

        private void LoadMessageMap()
        {
            if (latchetConfiguration?.Messageconsumer?.Commands?.Any() == true)
            {
                foreach (var item in latchetConfiguration?.Messageconsumer?.Commands)
                {
                    _messageTypeMap.Add($"{latchetConfiguration.ServiceId}.{item.CommandName}", item.MapToClass);
                }
            }
            if (latchetConfiguration?.Messageconsumer?.Events?.Any() == true)
            {
                foreach (var eventPublisher in latchetConfiguration?.Messageconsumer?.Events)
                {
                    foreach (var @event in eventPublisher?.EventData)
                    {
                        _messageTypeMap.Add($"{eventPublisher.FromServiceId}.{@event.EventName}", @event.MapToClass);

                    }
                }
            }
        }

        public void ConsumeCommand(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var eventType = Type.GetType(mapToClass);
                dynamic command = jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                commandDispatcher.Send(command);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }

        public void ConsumeEvent(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var eventType = Type.GetType(mapToClass);
                dynamic @event = jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                eventDispatcher.PublishDomainEventAsync(@event);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }
    }
}
