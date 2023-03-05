namespace InstaCommon.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string msg) : base(msg) { }

        public LoginFailedException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
