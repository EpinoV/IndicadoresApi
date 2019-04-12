using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN.Api
{
    public static class Utilities
    {
        public static string Base64Decode(string value)
        {
            var utf8String = System.Convert.FromBase64String(value);
            var decodeString = Encoding.UTF8.GetString(utf8String);

            return decodeString;
        }

        public static string Base64Encode(string value)
        {
            var utf8String = Encoding.UTF8.GetBytes(value);
            var base64String = System.Convert.ToBase64String(utf8String);

            return base64String;
        }
    }
}
