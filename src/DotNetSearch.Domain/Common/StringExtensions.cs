using System;

namespace DotNetSearch.Domain.Common
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string value)
        {
            return string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1));
        }
    }
}
