using OpenQA.Selenium;
using System.Text.Json.Serialization;

namespace InstaCommon.Contracts
{
    public class DeserializeableCookie : Cookie
    {
        [JsonConstructor]
        public DeserializeableCookie(string name, string value,
            string domain, string path, DateTime? expiry, bool secure, bool isHttpOnly, string sameSite) :
            base(name, value, domain, path, expiry, secure, isHttpOnly, sameSite)
        { }
    }

    public sealed class InstaCookies : List<DeserializeableCookie>
    {
        public InstaCookies() : base() { }

        public InstaCookies(int capacity) : base(capacity) { }

        [JsonConstructor]
        public InstaCookies(IEnumerable<DeserializeableCookie> collection) : base(collection) { }
    }
}
