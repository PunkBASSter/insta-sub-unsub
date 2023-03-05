namespace InstaCommon.Exceptions
{
    public class UserPageUnavailable : Exception
    {
        public UserPageUnavailable(string msg) : base(msg) { }

        public UserPageUnavailable(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
