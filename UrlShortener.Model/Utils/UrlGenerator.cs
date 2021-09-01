using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Utils
{
    public class UrlGenerator
    {
        public static string GenerateUniqueValue(int size)
        {
            //values can be set in the app.config
            if (size < 11 && size > 3)
            {
                Guid guid = Guid.NewGuid();
                string uniqueString = Convert.ToBase64String(guid.ToByteArray()).Replace("+", "").Replace("/", "").Replace("=", "");
                return uniqueString.Substring(0, size);
            }
            return "";
        }
    }
}
