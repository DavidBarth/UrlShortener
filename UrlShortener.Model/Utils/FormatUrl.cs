using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Utils
{
    public class FormatUrl
    {
        public static string FormatShortUrl(string scheme, string host, string pathBase, string shortUrl)
        {
            return $"{scheme}://{host}{pathBase}" + "/" + shortUrl;
        }
    }
}
