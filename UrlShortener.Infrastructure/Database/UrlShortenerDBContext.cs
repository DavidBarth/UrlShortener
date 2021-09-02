using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Infrastructure.Database
{
    public class UrlShortenerDBContext : DbContext
    {
        public UrlShortenerDBContext(DbContextOptions<UrlShortenerDBContext> options) : base(options){
        }

        public DbSet<Url> Urls { get; set; }
    }
}
