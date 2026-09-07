using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.Aggregates
{
    public sealed class Order
    {
        public Guid Id { get; }
        public string Cliente { get; }
        public string Produto { get; }
        public int Quantidade { get; }
        public decimal ValorUnitario { get; }
        public decimal ValorTotal => Quantidade * ValorUnitario;
        public DateTimeOffset CriadoEm { get; }

        private Order (
            Guid id,
            string cliente,
            string produto,
            int quantidade,
            decimal valorUnitario,
            DateTimeOffset criadoEm)
        {
            Id = id;
            Cliente = cliente;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            CriadoEm = criadoEm;
        }

        public static Order Create(
            string cliente, 
            string produto,
            int quantidade,
              decimal valorUnitario)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cliente);
            ArgumentException.ThrowIfNullOrWhiteSpace(produto);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantidade);
            ArgumentOutOfRangeException.ThrowIfNegative(valorUnitario);

            return new Order(
                Guid.NewGuid(),
                cliente.Trim(),
                produto.Trim(),
                quantidade,
                valorUnitario,
                DateTimeOffset.UtcNow
            );
        }


    }
}
