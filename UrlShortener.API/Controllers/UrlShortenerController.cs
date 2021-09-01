using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UrlShortenerController(IUrlService urlService, IMapper mapper)
        {
            this.urlService = urlService;
            this.mapper = mapper;
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
            Models.Url url = new Models.Url();
            url = await urlService.SaveUrl(mapper.Map<Models.Url>(urlRequest));
            url.ShortUrl = FormatUrl.FormatShortUrl(this.Request.Scheme, this.Request.Host.Value, this.Request.PathBase, url.ShortUrl);
            return mapper.Map<UrlResponseDTO>(url);
        }
    }
}
