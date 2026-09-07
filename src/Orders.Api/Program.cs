using Orders.Infrastructure.Messaging;
using Orders.Application.Abstractions;
using Orders.Application.Services;
using Azure.Messaging.ServiceBus;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var settings = builder.Configuration.
    GetSection(ServiceBusSettings.SectionName)
    .Get<ServiceBusSettings>()
    ?? throw new InvalidOperationException(
        "A seção ServiceBus não foi configurada.");

if (string.IsNullOrWhiteSpace(settings.ConnectionString))
    throw new InvalidOperationException("Configure ServiceBus: ConnectionString.");

builder.Services.AddSingleton(settings);
builder.Services.AddSingleton(
    new ServiceBusClient(
        settings.ConnectionString,
        new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        }));

builder.Services.AddSingleton<IServiceBusPublisher, AzureServiceBusPublisher>();
builder.Services.AddSingleton<IServiceBusConsumer, AzureServiceBusConsumer>();
builder.Services.AddScoped<OrderApplicationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();

