using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SN.Api.Models
{
    public class TokenConfiguration
    {
        public string SecretKey { get; set; }
        public string EncryptedKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int HoursToExpire { get; set; }
        public string UnauthorizedMessage { get; set; }

    }
}
