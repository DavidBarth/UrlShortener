using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class Url
    {

        [Key]
        public string ShortUrl { get; set; }
        [Required]
        public Uri LongUrl { get; set; }
        [Required]
        public DateTime Expiration { get; set; }

    }
}
