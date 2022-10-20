using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace ServiceBusPublisher.Services;

public class MessagePublisher : IMessagePublisher
{
    private readonly ITopicClient _topicClient;

    public MessagePublisher(ITopicClient topicClient)
    {
        _topicClient = topicClient;
    }
    
    public Task Publish<T>(T obj)
    {
        var objectAsText = JsonConvert.SerializeObject(obj);
        var message = new Message(Encoding.UTF8.GetBytes(objectAsText));
        message.UserProperties["messageType"] = typeof(T).Name;
        return _topicClient.SendAsync(message);
        
    }

    public Task Publish(string raw)
    {
        var message = new Message(Encoding.UTF8.GetBytes(raw));
        message.UserProperties["messageType"] = "Raw";
        return _topicClient.SendAsync(message);
    }
}