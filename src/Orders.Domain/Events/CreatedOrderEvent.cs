using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.Events
{
    public sealed record CreatedOrderEvent(
        Guid OrderId,
        string Cliente,
        string Produto,
        int Quantidade,
        decimal ValorUnitario,
        decimal ValorTotal,
        DateTimeOffset CriadoEm
    );
}
