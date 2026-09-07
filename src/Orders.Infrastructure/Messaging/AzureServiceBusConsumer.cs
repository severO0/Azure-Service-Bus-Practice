using Azure.Messaging.ServiceBus;
using Orders.Application.Abstractions;
using Orders.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Infrastructure.Messaging
{
    public sealed class AzureServiceBusConsumer(
        ServiceBusClient client,
        ServiceBusSettings settings) : IServiceBusConsumer
    {
        public async Task<CreatedOrderEvent?> ConsumeAsync(CancellationToken cancellationToken)
        {
            await using var receiver = client.CreateReceiver(
                settings.QueueName, 
                new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            });

            var message = await receiver.ReceiveMessageAsync(
                TimeSpan.FromSeconds(5),
                cancellationToken);

            if (message is null)
                return null;

            try
            {
                var @event = message.Body.ToObjectFromJson<CreatedOrderEvent>();

                if (@event is null)
                    throw new InvalidOperationException(
                        "A mensagem contém um pedido inválido.");
                await receiver.CompleteMessageAsync(
                    message,
                    cancellationToken);

                return @event;
            }
            catch (Exception)
            {
                await receiver.AbandonMessageAsync(
                    message,
                    cancellationToken : cancellationToken);
                throw;
            }
        }
    }
}
