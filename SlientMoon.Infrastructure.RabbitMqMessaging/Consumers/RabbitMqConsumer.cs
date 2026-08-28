using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SlientMoon.Application.Messages;
using SlientMoon.Infrastructure.RabbitMqMessaging.Settings;
using System.Text;
using System.Text.Json;

namespace SlientMoon.Infrastructure.RabbitMqMessaging.Consumers;

public class RabbitMqConsumer : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqConsumer(
        IOptions<RabbitMqSettings> options,
        IServiceScopeFactory scopeFactory)
    {
        _settings = options.Value;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        try
        {
            await using var connection =
                await factory.CreateConnectionAsync(stoppingToken);

            await using var channel =
                await connection.CreateChannelAsync(
                    cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: _settings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(
                        ea.Body.ToArray());

                    var message =
                        JsonSerializer.Deserialize<UserRegisteredMessage>(
                            json);

                    if (message is not null)
                    {
                        Console.WriteLine(
                            $"User registered: {message.UserId}, {message.Email}");
                    }

                    await channel.BasicAckAsync(
                        deliveryTag: ea.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"RabbitMQ Consumer Error: {ex.Message}");

                    await channel.BasicNackAsync(
                        deliveryTag: ea.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);
                }
            };

            await channel.BasicConsumeAsync(
                queue: _settings.QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            Console.WriteLine(
                $"RabbitMQ Consumer started. Queue: {_settings.QueueName}");

            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // Application shutting down
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"RabbitMQ Consumer failed: {ex}");
        }
    }
}