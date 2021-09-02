using System;
using UrlShortener.Shared.Utils;
using Xunit;

namespace UrlShortener.Test.UnitTest
{
    public class UrlGenerator_Test
    {
        [Fact]
        public void Generate_Random_Url_Test()
        {
            string urlSixDigits = "";
            string urlFourDigits = "";

            urlSixDigits = UrlGenerator.GenerateUniqueValue(6);
            urlFourDigits = UrlGenerator.GenerateUniqueValue(4);

            Assert.Equal(6, urlSixDigits.Length);
            Assert.Equal(4, urlFourDigits.Length);
        }

        [Fact]
        public void Generate_Random_Url_Invalid_Digits_Test()
        {
            string invalidUrlWithElevenDigits = "";
            string invalidUrlWithThreeDigits = "";

            invalidUrlWithElevenDigits = UrlGenerator.GenerateUniqueValue(11);
            invalidUrlWithThreeDigits = UrlGenerator.GenerateUniqueValue(3);

            Assert.Equal("", invalidUrlWithElevenDigits);
            Assert.Equal("", invalidUrlWithThreeDigits);
        }

        [Fact]
        public void FormatShortUrl_Test()
        {
            string url = "";
            url = FormatUrl.FormatShortUrl("http", "www.payrocshortener.com", "YahSksz");
            Assert.Equal("http://www.payrocshortener.com/YahSksz", url);
        }

        [Theory]
        [InlineData("http://www.payrocshortener.com")]
        [InlineData("https://wwww.payrocshortener.com")]
        [InlineData("http://www.google.com")]
        [InlineData("https://www.google.com")]
        public void ValidUrl_Test(string url)
        {
            bool isValidUrl = false;
            isValidUrl = FormatUrl.ValidUri(url);
            Assert.True(isValidUrl);
        }

        [Theory]
        [InlineData("httpz://www.payrocshortener.com")]
        [InlineData("http://wwww.payrocshortener.com+")]
        [InlineData("https://www.payrocshortene;r.com")]
        public void InvalidUrl_Test(string url)
        {
            bool isValidUrl = false;
            isValidUrl = FormatUrl.ValidUri(url);
            Assert.False(isValidUrl);
        }

        [Fact]
        public void EmptyUrl_Test()
        {
            bool isValidUrl = false;
            isValidUrl = FormatUrl.ValidUri("");
            Assert.False(isValidUrl);
        }

    }
}
