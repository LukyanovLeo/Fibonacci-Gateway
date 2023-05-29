using Fibonacci.Gateway.Domain.Models.Aggregates;
using Gateway.Domain.Models.Aggregates;
using System.Net.Http.Json;

namespace Fibonacci.Gateway.Infrastructure.Services
{
    public class FibonacciService : IFibonacciService
    {
        public async Task CalculateNext(FibonacciNumber fibNumber)
        {
            var httpClient = new HttpClient();
            var content = JsonContent.Create(fibNumber);

            await httpClient.PostAsync("http://localhost:7777/api/fibonacci/calculate", content);
        }
    }
}
