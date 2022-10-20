using Microsoft.AspNetCore.Mvc;
using ServiceBusPublisher.Model;
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
    
    [HttpPost("publish/user")]
    public async Task<IActionResult> PublishUser(UserModel userModel)
    {
        await _messagePublisher.Publish(userModel);
        return Ok();
    }
    
    [HttpPost("publish/order")]
    public async Task<IActionResult> PublishOrder(OrderModel orderModel)
    {
        await _messagePublisher.Publish(orderModel);
        return Ok();
    }

}