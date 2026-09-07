using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Infrastructure.Messaging
{
    public sealed class ServiceBusSettings
    {
        public const string SectionName = "ServiceBus";
        public string ConnectionString { get; set; } = string.Empty;
        public string QueueName { get; set; } = "orders";
    }
}
