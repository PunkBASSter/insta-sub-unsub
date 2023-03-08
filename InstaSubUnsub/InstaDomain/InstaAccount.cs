namespace InstaDomain
{
    /// <summary>
    /// It's more like an infrastructure-related model, but not actually a domain one
    /// </summary>
    public class InstaAccount
    {
        private readonly string _username;
        private readonly string _password;

        public InstaAccount(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Missing username or password for Account object creation.");

            _username = username;
            _password = password;
        }

        public string Username => _username;
        public string Password => _password;
    }
}
