using System.Text.RegularExpressions;

namespace InstaCommon.Extensions
{
    public static partial class StringExtensions
    {
        public static bool HasRussianText(this string str)
        {
            return HasRussianTextRegex().IsMatch(str);
        }

        [GeneratedRegex("[А-Яа-я]")]
        private static partial Regex HasRussianTextRegex();
    }
}
