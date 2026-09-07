using Orders.Application.Abstractions;
using Orders.Application.ValueObjects;
using Orders.Domain.Aggregates;
using Orders.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.Services
{
    public sealed class OrderApplicationService(
        IServiceBusPublisher publisher)
    {
        public async Task<OrderResponse> CreateAsync(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var order = Order.Create(
                request.Cliente,
                request.Produto,
                request.Quantidade,
                request.ValorUnitario);

            var @event = new CreatedOrderEvent(
                order.Id,
                order.Cliente,
                order.Produto,
                order.Quantidade,
                order.ValorUnitario,
                order.ValorTotal,
                order.CriadoEm);

            await publisher.PublishAsync(@event, cancellationToken);

            return new OrderResponse(
                order.Id,
                order.Cliente,
                order.Produto,
                order.Quantidade,
                order.ValorTotal,
                order.CriadoEm);
        }
    }
}
