namespace InstaCommon
{
    public class Delay
    {
        private readonly int _minDelay;
        private readonly int _maxDelay;
        private readonly Random _random = new Random(DateTime.UtcNow.Microsecond);

        public Delay(int min = 200, int max = 900)
        {
            _minDelay = min;
            _maxDelay = max;
        }

        public void Random(Action a)
        {
            Random();
            a();
        }

        public void Random()
        {
            Thread.Sleep(_random.Next(_minDelay, _maxDelay));
        }
    }
}
