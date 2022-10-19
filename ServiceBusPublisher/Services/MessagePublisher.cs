using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace ServiceBusPublisher.Services;

public class MessagePublisher : IMessagePublisher
{
    private readonly IQueueClient _queueClient;

    public MessagePublisher(IQueueClient queueClient)
    {
        _queueClient = queueClient;
    }
    
    public Task Publish<T>(T obj)
    {
        var objectAsText = JsonConvert.SerializeObject(obj);
        var message = new Message(Encoding.UTF8.GetBytes(objectAsText));
        return _queueClient.SendAsync(message);
        
    }

    public Task Publish(string raw)
    {
        var message = new Message(Encoding.UTF8.GetBytes(raw));
        return _queueClient.SendAsync(message);
    }
}