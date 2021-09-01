using System;
using UrlShortener.Shared.Utils;
using Xunit;

namespace UrlShortener.Test.UnitTest
{
    public class UrlGenerator_Test
    {
        [Fact]
        public void Generate_Random_Url()
        {
            string urlSixDigits = "";
            string urlFourDigits = "";

            urlSixDigits = UrlGenerator.GenerateUniqueValue(6);
            urlFourDigits = UrlGenerator.GenerateUniqueValue(4);

            Assert.Equal(6, urlSixDigits.Length);
            Assert.Equal(4, urlFourDigits.Length);
        }

        [Fact]
        public void Generate_Random_Url_Invalid_Digits()
        {
            string invalidUrlWithElevenDigits = "";
            string invalidUrlWithThreeDigits = "";

            invalidUrlWithElevenDigits = UrlGenerator.GenerateUniqueValue(11);
            invalidUrlWithThreeDigits = UrlGenerator.GenerateUniqueValue(3);

            Assert.Equal("", invalidUrlWithElevenDigits);
            Assert.Equal("", invalidUrlWithThreeDigits);
        }
    }
}
