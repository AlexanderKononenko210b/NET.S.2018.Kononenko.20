using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Entity;
using XML.Interfaces;

namespace XML.Services
{
    public class UrlAddressParser : IParser<UrlAddress>
    {
        private const string SCHEME = "https://";

        /// <summary>   
        /// Method for check validate input string
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>true if string is valid, 
        /// array host,url,params,
        /// array params or null if they absent,
        /// message result verify</returns>
        public (UrlAddress , string) IsVerify(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return (null, $"{input} (null or whiteSpace, time: {DateTime.Now})");

            var indexScheme = input.IndexOf(SCHEME, StringComparison.CurrentCulture);

            if (indexScheme != 0)
                return (null, $"{input} (part scheme is not valid, time: {DateTime.Now})");

            if (input.Length <= SCHEME.Length)
                return (null, $"{input} (part <host>/<URL-path> is absent, time: {DateTime.Now})");

            var index = input.IndexOf('?');

            var address = new UrlAddress();

            if (index != -1)
            {
                var hostAndUrlPath = input.Substring(SCHEME.Length, index - SCHEME.Length)
                    .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (!hostAndUrlPath.Any())
                    return (null, $"{input} (part <host>/<URL-path> is absent, time: {DateTime.Now})");

                if (hostAndUrlPath[0].IndexOf('.') == -1)
                    return (null, $"{input} (part <host>is not valid, time: {DateTime.Now})");

                var parametrs = input.Substring(index + 1).Split(new[] { '=', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (!parametrs.Any())
                    return (null, $"{input} (part <parameters> exist but not valid, time: {DateTime.Now})");

                if (parametrs.Length % 2 != 0)
                    return (null, $"{input} (part <parameters> exist but not valid, time: {DateTime.Now})");

                address.HostName = hostAndUrlPath[0];

                address.Parametres = new List<UrlElement>();

                for(int i = 0; i < parametrs.Length - 1; i++)
                    address.Parametres.Add(new UrlElement(parametrs[i], parametrs[i + 1]));

                if (hostAndUrlPath.Length == 1)
                    return (address, $"Input string is valid, time: {DateTime.Now}");

                address.Uri = new List<UrlSegment>();

                for (int i = 1; i < hostAndUrlPath.Length; i++)
                    address.Uri.Add(new UrlSegment(hostAndUrlPath[i]));

                return (address, $"Input string is valid, time: {DateTime.Now}");
            }
            else
            {
                var hostAndUrlPath = input.Substring(SCHEME.Length)
                        .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (!hostAndUrlPath.Any())
                    return (null, $"{input} (part <host>/<URL-path> is absent, time: {DateTime.Now})");

                if (hostAndUrlPath[0].IndexOf('.') == -1)
                    return (null, $"{input} (part <host>/<URL-path> is absent, time: {DateTime.Now})");

                address.HostName = hostAndUrlPath[0];

                if (hostAndUrlPath.Length == 1)
                    return (address, $"Input string is valid, time: {DateTime.Now}");

                address.Uri = new List<UrlSegment>();

                for (int i = 1; i < hostAndUrlPath.Length; i++)
                    address.Uri.Add(new UrlSegment(hostAndUrlPath[i]));

                return (address, $"Input string is valid, time: {DateTime.Now}");
            }
        }
    }
}
