using Orders.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Abstractions
{
    public interface IServiceBusPublisher
    {
        Task PublishAsync(
            CreatedOrderEvent @event,
            CancellationToken cancellationToken);
    }
}
