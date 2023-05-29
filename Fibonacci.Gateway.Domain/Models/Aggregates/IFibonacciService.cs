using Gateway.Domain.Models.Aggregates;

namespace Fibonacci.Gateway.Domain.Models.Aggregates
{
    public interface IFibonacciService
    {
        Task CalculateNext(FibonacciNumber fibNumber);
    }
}
