namespace InstaDomain
{
    public class InstaAccount
    {
        private readonly string _username;
        private readonly string _password;

        public InstaAccount(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Missing username or password for Account object creation.");

            _username = username;
            _password = password;
        }

        public string Username => _username;
        public string Password => _password;
    }
}
