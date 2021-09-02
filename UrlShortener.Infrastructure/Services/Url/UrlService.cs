using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly int maxAttempts;


        public UrlService(UrlShortenerDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            maxAttempts = Convert.ToInt16(configuration.GetSection("AppSettings:UrlSize").Value);
        }

        public async Task<Models.Url> SaveUrl(Models.Url url)
        {
            //Apply if the application should return an existing long URL (need to check business logic regarding expiration)
            //Models.Url existingUrl = await GetExistingLongUrl(url);
            //if (existingUrl is not null)
            //    return existingUrl;

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

        public async Task<Models.Url> GetExistingLongUrl(Models.Url url)
        {
            return await context.Urls.FirstOrDefaultAsync(u => u.LongUrl == url.LongUrl);
        }

        public async Task<int> DeleteExpiredUrls()
        {
            List<Models.Url> listUrls = new List<Models.Url>();
            try
            {
                int totalDeleted = 0;
                listUrls = await context.Urls.Where(u => u.Expiration < DateTime.Now).ToListAsync();
                if (listUrls.Count > 0)
                {
                    context.Urls.RemoveRange(listUrls);
                    totalDeleted = await context.SaveChangesAsync();
                }

                return totalDeleted;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
