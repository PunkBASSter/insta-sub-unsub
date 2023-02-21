using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace InstaCrawlerApp
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
