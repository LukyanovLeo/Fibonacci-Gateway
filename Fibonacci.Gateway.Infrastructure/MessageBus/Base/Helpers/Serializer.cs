using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace Fibonacci.Gateway.Infrastructure.MessageBus.Base.Helpers
{
    public static class Serializer
    {
        public static T SerializeReceivedMessage<T>(BasicDeliverEventArgs ea)
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray()); 
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
