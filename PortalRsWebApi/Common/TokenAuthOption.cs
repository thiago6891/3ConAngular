using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRSApi.Common
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "PortalRSApi";
        public static string Issuer { get; } = "MettaSistemas";

        //public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("UTION4UNIHJFNWIJFNI64N58HN3HB7IFNIHFIHIHE7B"));

        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

        public static DateTime Expiration => DateTime.Now.AddMonths(1);
        public static string TokenType { get; } = "Bearer";
    }
}