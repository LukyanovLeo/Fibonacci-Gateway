using Gateway.Domain.Models.Base;

namespace Gateway.Domain.Models.Aggregates
{
    public class FibonacciNumber : Entity
    {
        public int NumberId { get; set; }

        public long PreviousValue { get; set; }

        public long CurrentValue { get; set; }


        public FibonacciNumber(int id)
        {
            NumberId = id;
            PreviousValue = 1;
            CurrentValue = 1;
        }
    }
}