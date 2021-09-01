using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.DTO.API
{
    public class UrlRequestDTO
    {
        [Required]
        public Uri LongUrl { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
    }
}
