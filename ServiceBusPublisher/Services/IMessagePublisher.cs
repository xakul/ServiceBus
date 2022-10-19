namespace ServiceBusPublisher.Services;

public interface IMessagePublisher
{
    Task Publish<T>(T obj);
    
    Task Publish(string raw);

}