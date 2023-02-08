namespace InstaDomain
{
    public class User
    {
        public long Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string? Name { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Rank { get; set; }
        public bool IsFollower { get; set; }
        public UserStatus Status { get; set; }
    }
}