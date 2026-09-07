using Microsoft.AspNetCore.Mvc;
using Orders.Application.Abstractions;
using Orders.Application.Services;
using Orders.Application.ValueObjects;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController(
        OrderApplicationService service,
        IServiceBusConsumer consumer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var order = await service.CreateAsync(
                request,
                cancellationToken);

            return Accepted(order);
        }

        [HttpPost("consume-next")]
        public async Task<ActionResult<OrderResponse>> ConsumeNext(
            CancellationToken cancellationToken)
        {
            var @event = await consumer.ConsumeAsync(cancellationToken);

            if (@event is null)
                return NoContent();

            return Ok(new
            {
                message = "Pedido recebido e concluído.",
                @event
            });
        }
    }
}
