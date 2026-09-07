using Azure.Messaging.ServiceBus;
using Orders.Application.Abstractions;
using Orders.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Infrastructure.Messaging
{
    public sealed class AzureServiceBusPublisher(
       ServiceBusClient client,
       ServiceBusSettings settings) : IServiceBusPublisher
    {
        public async Task PublishAsync(
            CreatedOrderEvent @event,
            CancellationToken cancellationToken)
        {
            await using var sender = client.CreateSender(settings.QueueName);

            var message = new ServiceBusMessage(
                BinaryData.FromObjectAsJson(@event))
            {
                MessageId = @event.OrderId.ToString(),
                Subject = nameof(CreatedOrderEvent),
                ContentType = "application/json",
            };

            await sender.SendMessageAsync(message, cancellationToken);
        }
    }
}
