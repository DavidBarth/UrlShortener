using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class Url
    {

        [Key]
        public string ShortUrl { get; set; } = UrlShortener.Shared.Utils.UrlGenerator.GenerateUniqueValue(6);
        [Required]
        public Uri LongUrl { get; set; }
        [Required]
        public DateTime Expiration { get; set; }

    }
}
