using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Gateway.Domain.Models.Aggregates;
using Fibonacci.Gateway.Domain.Models.Aggregates;
using Microsoft.Extensions.Hosting;
using Fibonacci.Gateway.Infrastructure.MessageBus.Base.Interfaces;
using Fibonacci.Gateway.Infrastructure.MessageBus.Base.Helpers;
using Newtonsoft.Json.Linq;

namespace Gateway.MessageBus.Infrastructure
{
    public class FibonacciConsumer : BackgroundService, IRabbitMqConsumer
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IFibonacciService _fibonacciService;
        private readonly string FibonacciQueueName = "fibonacci_queue";

        public FibonacciConsumer(IFibonacciService fibonacciService)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _fibonacciService = fibonacciService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) => ReceivedMessageHandle(ch, ea);            
            _channel.BasicConsume(FibonacciQueueName, false, consumer);
        }

        public async Task ReceivedMessageHandle(object sender, BasicDeliverEventArgs ea)
        {
            var message = Serializer.SerializeReceivedMessage<FibonacciNumber>(ea);

            Console.WriteLine($"Stream: {message.NumberId}. Fibonacci value: {message.CurrentValue}");

            await _fibonacciService.CalculateNext(message);
            _channel.BasicAck(ea.DeliveryTag, false);
        }
    }
}
