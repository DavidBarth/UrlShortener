using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Utils
{
    public class FormatUrl
    {
        public static string FormatShortUrl(string scheme, string host, string shortUrl)
        {
            return $"{scheme}://{host}" + "/" + shortUrl;
        }

        public static bool ValidUri(string link)
        {
            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
