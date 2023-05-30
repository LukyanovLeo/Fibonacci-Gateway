using Fibonacci.Gateway.Domain.Models.Aggregates;
using Gateway.Domain.Models.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/fibonacci")]
    public class FibonacciController : Controller
    {
        private readonly IFibonacciService _fibonacciService;

        public FibonacciController(IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
        }


        [HttpPost]
        [Route("start")]
        public async Task<IActionResult> StartAsync(int tasksNumber)
        {
            var requests = new Task[tasksNumber];
            for (int i = 0; i < tasksNumber; i++)
            {
                var index = i;
                requests[index] = Task.Factory.StartNew(() =>
                    _fibonacciService.CalculateNext(new FibonacciNumber(index))
                );
            }
            Task.WaitAll(requests);

            return Ok();
        }
    }
}
