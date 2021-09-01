using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Infrastructure.Services.Url;

namespace UrlShortener.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UrlRedirectController : ControllerBase
    {
        private readonly IUrlService urlService;
        private readonly IMapper mapper;

        public UrlRedirectController(IUrlService urlService, IMapper mapper)
        {
            this.urlService = urlService;
            this.mapper = mapper;
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> Redirect(string shortUrl)
        {
            Models.Url url = await urlService.GetLongUrl(shortUrl);
            return this.RedirectPermanent(url.LongUrl.ToString());
        }
    }
}
