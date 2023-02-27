namespace InstaDomain.Enums
{
    public enum UserStatus
    {
        New, //newly discovered
        Followed, //followed by us
        Unfollowed, //unfollowed by us
        Protected, //protected from unfollowing
        Visited, //has been visited by the crawler
        Error, //has been visited and something went wrong during the visit
        Unavailable
    }
}
