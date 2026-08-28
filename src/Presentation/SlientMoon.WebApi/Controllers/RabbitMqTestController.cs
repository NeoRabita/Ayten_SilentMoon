using Microsoft.AspNetCore.Mvc;
using SlientMoon.Application.Interfaces.Messaging;
using System;
using System.Threading.Tasks;

namespace SlientMoon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqTestController : BaseController
{
    private readonly IMessagePublisher _messagePublisher;

    public RabbitMqTestController(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage()
    {
        var message = new
        {
            Message = "Hello from SilentMoon!",
            CreatedAt = DateTime.UtcNow
        };

        await _messagePublisher.PublishAsync(message);

        return Ok(new
        {
            Message = "Message sent to RabbitMQ successfully."
        });
    }
}