using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Infrastructure.Services.Url
{
    public interface IUrlService
    {
        Task<Models.Url> SaveUrl(Models.Url url);
        Task<Models.Url> GetLongUrl(string shortUrl);
        Task<Models.Url> GetExistingLongUrl(Models.Url url);
        Task<int> DeleteExpiredUrls();
        Task<List<Models.Url>> GetExistingStoredUrls();

    }
}
