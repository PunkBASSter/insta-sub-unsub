namespace InstaInfrastructureAbstractions
{
    public interface IPersistentCookieUtil
    {
        void SaveCookies(string username);
        bool LoadCookies(string username);
        void DeleteCookies(string username);
    }
}
