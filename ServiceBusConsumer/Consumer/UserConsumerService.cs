using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusConsumer.Models;

namespace ServiceBusConsumer.Consumer;

public class UserConsumerService : BackgroundService
{
    private readonly ISubscriptionClient _subscriptionClient;

    public UserConsumerService(ISubscriptionClient subscriptionClient)
    {
        _subscriptionClient = subscriptionClient;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // this will be called everytime you get message
        _subscriptionClient.RegisterMessageHandler((message, token) =>
        {
            var userModel = JsonConvert.DeserializeObject<UserModel>(Encoding.UTF8.GetString(message.Body));
            Console.WriteLine($"New user with name {userModel.FirstName}");
            return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        },new MessageHandlerOptions(args => Task.CompletedTask)
        {
            AutoComplete = false,
            MaxConcurrentCalls = 1
        });
        return Task.CompletedTask;
    }
}