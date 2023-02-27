namespace SeleniumUtils.Exceptions
{
    public class InstaAntiBotException : Exception
    {
        public InstaAntiBotException(string msg) : base(msg) { }

        public InstaAntiBotException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
