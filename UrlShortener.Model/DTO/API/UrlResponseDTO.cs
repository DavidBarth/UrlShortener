using System;

namespace UrlShortener.Shared.DTO.API
{
    public class UrlResponseDTO
    {
        public Uri ShortUrl { get; set; }
        public Uri LongUrl { get; set; }

    }
}
