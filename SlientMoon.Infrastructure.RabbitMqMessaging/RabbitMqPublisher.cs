using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SlientMoon.Application.Interfaces.Messaging;
using SlientMoon.Infrastructure.RabbitMqMessaging.Settings;
using System.Text;
using System.Text.Json;

namespace SlientMoon.Infrastructure.RabbitMqMessaging;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqSettings _settings;

    public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }

    public async Task PublishAsync<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: _settings.QueueName,
            body: body);
    }
}