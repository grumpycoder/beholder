using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace splc.beholder.web.Utility
{
    public static class StringHelper
    {

        //public static string ToValidFileName(this string s, char replaceChar = ' ', char[] includeChars = null)
        //{
        //    var invalid = Path.GetInvalidFileNameChars();
        //    if (includeChars != null)
        //    {
        //        invalid = invalid.Union(includeChars).ToArray();
        //        return string.Join(string.Empty, s.ToCharArray().Select(o => o.Equals(invalid) ? replaceChar : o));
        //    }
        //}

        public static string ToValidFileName(this string s, char replaceChar = '_', char[] includeChars = null)
        {
            var invalid = Path.GetInvalidFileNameChars();
            if (includeChars != null) invalid = invalid.Union(includeChars).ToArray();
            return string.Join(string.Empty, s.ToCharArray().Select(o => o.Equals(invalid) ? replaceChar : o));
        }

    }
}