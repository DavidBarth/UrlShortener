using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Infrastructure.Database;

namespace UrlShortener.Infrastructure.Services.Url
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDBContext context;
        const int maxAttempts = 5;

        public UrlService(UrlShortenerDBContext context)
        {
            this.context = context;
        }

        public async Task<Models.Url> SaveUrl(Models.Url url)
        {
            bool concurrencyError;
            int totalAttempts = 0;
            do
            {
                concurrencyError = false;
                try
                {
                    context.Urls.Add(url);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    SqlException innerException = ((Microsoft.Data.SqlClient.SqlException)ex.InnerException);
                    if (innerException != null && innerException.Number == 2627)
                    {
                        concurrencyError = true;
                        context.Entry(url).Reload();
                        url = new Models.Url() { Expiration = url.Expiration, LongUrl = url.LongUrl };
                    }
                    else
                    {
                        throw ex;
                    }
                    totalAttempts++;
                }

            } while (concurrencyError && totalAttempts < maxAttempts);

            return url;
        }

        public async Task<Models.Url> GetLongUrl(string shortUrl)
        {
            Models.Url url = await context.Urls.FindAsync(shortUrl);
            return url;
        }
    }
}
