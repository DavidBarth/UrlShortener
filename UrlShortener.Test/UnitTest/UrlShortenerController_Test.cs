using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.API.Controllers;
using UrlShortener.Infrastructure.Services.Url;
using UrlShortener.Models;
using UrlShortener.Shared.DTO.API;
using Xunit;

namespace UrlShortener.Test.UnitTest
{
    public class UrlShortenerController_Test
    {
        [Fact]
        public async Task TestControllerReturs_One_ShortenedUrl()
        {
            // Arrange
            var mockUrlService = new Mock<IUrlService>();
            var mockMapper = new Mock<IMapper>();
            var mockConfiguration = new Mock<IConfiguration>();
            var testUrl = mockUrlService.Setup(service => service.GetExistingStoredUrls())
                .ReturnsAsync(GetTestUrls());
            mockMapper.Setup(mapper => mapper.Map<UrlResponseDTO>(testUrl));
            mockConfiguration.Setup(configuration => configuration.GetSection("AppSettings:UrlSize").Value);
            var controller = new UrlShortenerController(mockUrlService.Object, mockMapper.Object, mockConfiguration.Object);
            controller.ControllerContext = GetControllerContext(); ;

            // Act
            var result = await controller.GetShortenedUrls();

            // Assert
            Assert.IsType<ActionResult<List<UrlResponseDTO>>>(result);
            Assert.True(result.Value.Count == 1);
        }

        private static ControllerContext GetControllerContext()
        {
            var request = new Mock<HttpRequest>();
            request.SetupGet(r => r.Host).Returns(new HostString("localhost", 44377));
            var httpContext = Mock.Of<HttpContext>(_ => _.Request == request.Object);
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            return controllerContext;
        }

        private List<Url> GetTestUrls()
        {
            var testUrls = new List<Url>()
            {
                new Url()
                {
                    ShortUrl = string.Empty,
                    LongUrl = new Uri("https://jamesnewkirk.typepad.com/posts/2007/09/why-you-should-.html")
                }
            };
            return testUrls;
        }
    }
}
