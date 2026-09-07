using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.ValueObjects
{
    public sealed record CreateOrderRequest(
        string Cliente,
        string Produto,
        int Quantidade,
        decimal ValorUnitario
    );

}
