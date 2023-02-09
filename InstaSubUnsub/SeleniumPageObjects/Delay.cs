using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumUtils
{
    public class Delay
    {
        private readonly int _minDelay;
        private readonly int _maxDelay;
        private readonly Random _random = new Random(DateTime.UtcNow.Microsecond);

        public Delay(int min=200, int max = 900) 
        {
            _minDelay = min;
            _maxDelay = max;
        }

        public void Random(Action a)
        {
            Thread.Sleep(_random.Next(_minDelay, _maxDelay));
            a();
        }
    }
}
