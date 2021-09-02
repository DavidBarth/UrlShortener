using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Infrastructure.Services.Url;
using UrlShortener.Shared.DTO.API;
using UrlShortener.Shared.Utils;

namespace UrlShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService urlService;
        private readonly IMapper mapper;
        private readonly int maxLengthUrl;

        public UrlShortenerController(IUrlService urlService, IMapper mapper, IConfiguration configuration)
        {
            this.urlService = urlService;
            this.mapper = mapper;
            maxLengthUrl = Convert.ToInt16(configuration.GetSection("AppSettings:UrlSize").Value);
        }

        /// <summary>
        /// Creates a short URL
        /// </summary>
        /// <returns>UrlResponseDTO</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("PostUrl")]
        public async Task<ActionResult<UrlResponseDTO>> PostUrl(UrlRequestDTO urlRequest)
        {
            Models.Url url = mapper.Map<Models.Url>(urlRequest);
            //generate random ID
            url.ShortUrl = UrlGenerator.GenerateUniqueValue(maxLengthUrl);

            //save to DB
            url = await urlService.SaveUrl(url);
            
            //update model with scheme/host/url
            url.ShortUrl = FormatUrl.FormatShortUrl(this.Request.Scheme, this.Request.Host.Value, url.ShortUrl);
            
            //convert to a DTO and return
            UrlResponseDTO urlResponse = mapper.Map<UrlResponseDTO>(url);
            return urlResponse;
        }

        /// <summary>
        /// Clean expired URL's
        /// </summary>
        /// <returns>int</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("CleanExpiredUrls")]
        public async Task<ActionResult<int>> CleanExpiredUrls()
        {
            int totalUrlsDeleted = await urlService.DeleteExpiredUrls();
            return Ok("Total URL's deleted: " + totalUrlsDeleted);
        }
    }
}
