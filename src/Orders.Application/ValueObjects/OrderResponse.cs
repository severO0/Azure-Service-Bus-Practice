using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Application.ValueObjects
{
    public sealed record OrderResponse(
        Guid Id,
        string Cliente,
        string Produto,
        int Quantidade,
        decimal ValorTotal,
        DateTimeOffset CriadoEm
    );

}
