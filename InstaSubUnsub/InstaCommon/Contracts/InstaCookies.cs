using OpenQA.Selenium;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace InstaCommon.Contracts
{
    public class DeserializeableCookie : Cookie
    {
        [JsonConstructor]
        public DeserializeableCookie(string name, string value,
            string domain, string path, DateTime? expiry, bool secure, bool isHttpOnly, string sameSite) :
            base(name, value, domain, path, expiry, secure, isHttpOnly, sameSite)
        { }
        public DeserializeableCookie(Cookie cookie) : 
            base(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, cookie.Expiry, cookie.Secure, cookie.IsHttpOnly, cookie.SameSite)
        { }
    }

    public sealed class InstaCookies : List<DeserializeableCookie>
    {
        public InstaCookies() : base() { }

        public InstaCookies(int capacity) : base(capacity) { }

        public InstaCookies(IEnumerable<DeserializeableCookie> collection) : base(collection) { }
    }
}
