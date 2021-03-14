using Latchet.Infrastructures.Events.Outbox;
using Latchet.Utilities.Configurations;
using Latchet.Utilities.Services.MessageBus;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Latchet.InfrastructuresEvents.PullingPublisher
{
    public class PoolingPublisherHostedService : IHostedService
    {
        private readonly LatchetConfigurations _configuration;
        private readonly IOutBoxEventItemRepository _outBoxEventItemRepository;
        private readonly IMessageBus _messageBus;
        private Timer _timer;
        public PoolingPublisherHostedService(LatchetConfigurations configuration, IOutBoxEventItemRepository outBoxEventItemRepository, IMessageBus messageBus)
        {
            _configuration = configuration;
            _outBoxEventItemRepository = outBoxEventItemRepository;
            _messageBus = messageBus;

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendOutBoxItems, null, TimeSpan.Zero, TimeSpan.FromSeconds(_configuration.PullingPublisher.SendOutBoxInterval));
            return Task.CompletedTask;
        }

        private void SendOutBoxItems(object state)
        {
            _timer.Change(Timeout.Infinite, 0);
            var outboxItems = _outBoxEventItemRepository.GetOutBoxEventItemsForPublishe(_configuration.PullingPublisher.SendOutBoxCount);

            foreach (var item in outboxItems)
            {
                _messageBus.Send(new Parcel
                {
                    CorrelationId = item.AggregateId,
                    MessageBody = item.EventPayload,
                    MessageId = item.EventId.ToString(),
                    MessageName = item.EventName,
                    Route = $"{_configuration.ServiceId}.{item.EventName}",
                    Headers = new Dictionary<string, object>
                    {
                        ["AccuredByUserId"] = item.AccuredByUserId,
                        ["AccuredOn"] = item.AccuredOn.ToString(),
                        ["AggregateName"] = item.AggregateName,
                        ["AggregateTypeName"] = item.AggregateTypeName,
                        ["EventTypeName"] = item.EventTypeName,
                    }
                });
                item.IsProcessed = true;
            }
            _outBoxEventItemRepository.MarkAsRead(outboxItems);
            _timer.Change(0, _configuration.PullingPublisher.SendOutBoxInterval);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
