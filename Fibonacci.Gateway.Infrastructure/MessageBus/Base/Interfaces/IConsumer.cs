using RabbitMQ.Client.Events;

namespace Fibonacci.Gateway.Infrastructure.MessageBus.Base.Interfaces
{
    public interface IRabbitMqConsumer
    {
        Task ReceivedMessageHandle(object sender, BasicDeliverEventArgs ea);
    }
}
