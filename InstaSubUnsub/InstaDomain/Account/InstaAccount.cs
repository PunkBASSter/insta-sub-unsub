namespace InstaDomain.Account
{
    public class InstaAccount : BaseEntity
    {
        public InstaAccount() : this("TestUser", "TestPassword") { }
        public InstaAccount(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Missing username or password for Account object creation.");

            Username = username;
            Password = password;
        }

        public string Username { get; init; }
        public string Password { get; init; }
    }
}
