using Microsoft.AspNetCore.Mvc;
using ServiceBusPublisher.Services;

namespace ServiceBusPublisher.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessagePublisher _messagePublisher;

    public MessageController(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    [HttpPost("publish/text")]
    public async Task<IActionResult> PublishText()
    {
        using var reader = new StreamReader(Request.Body);
        var bodyAsString = await reader.ReadToEndAsync();
        await _messagePublisher.Publish(bodyAsString);
        return Ok();
    }

}