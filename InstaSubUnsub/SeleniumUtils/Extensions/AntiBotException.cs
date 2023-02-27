namespace SeleniumUtils.Extensions
{
    public class InstaAntiBotException : Exception
    {
        public InstaAntiBotException(string msg) : base(msg) { }

        public InstaAntiBotException(string? message, InstaAntiBotException? innerException) : base(message, innerException) { }
    }
}
