using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auth0.helloserverless.com.Util
{
    public static class HeaderHelper
    {
        static readonly Encoding httpHeadersEncoding = Encoding.GetEncoding("iso-8859-1");

        public static string AsSingleLine(this IHeaderDictionary headers)
        {
            if (headers == null)
                return null;

            List<string> items = new List<string>();

            foreach(var kvp in headers)
            {
                string val = kvp.Value;

                var item = $"{kvp.Key}={val}";

                items.Add(item);
            }

            var result = string.Join('|', items);

            return result;
        }

        public static (string username, string password) ParseBasicAuthInfo(this IHeaderDictionary headers)
        {
            if (headers == null)
                return (username: null, password: null);

            string authHdrValue = headers["Authorization"];

            var prefix = "Basic ";

            if(string.IsNullOrEmpty(authHdrValue) || !authHdrValue.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return (username: null, password: null);

            var authValueBase64 = authHdrValue.Substring(prefix.Length);

            var base64Bytes = Convert.FromBase64String(authValueBase64);

            var kvpVal = httpHeadersEncoding.GetString(base64Bytes);

            var kvpComponents = kvpVal.Split(':', StringSplitOptions.RemoveEmptyEntries);

            string uname = null, pwd = null;

            if (kvpComponents.Length > 0)
                uname = kvpComponents[0].Trim();

            if (kvpComponents.Length > 1)
                pwd = kvpComponents[1].Trim();

            return (uname, pwd);
        }
    }
}
