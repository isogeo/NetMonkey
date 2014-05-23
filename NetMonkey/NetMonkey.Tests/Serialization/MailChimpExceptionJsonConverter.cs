using System;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions;

namespace NetMonkey.Serialization.Tests
{

    public class MailChimpExceptionJsonConverterTests
    {

        [Theory]
        [InlineData("{\"status\":\"error\",\"code\":-99,\"name\":\"Unknown_Exception\",\"error\":\"An unknown error occurred processing your request. Please try again later.\"}", -99, MailChimpExceptionKind.Unknown, "An unknown error occurred processing your request. Please try again later.")]
        public void ReadJson_ShouldCreateCorrectMailChimpException(string input, int expectedCode, MailChimpExceptionKind expectedKind, string expectedMessage)
        {
            var e=JsonConvert.DeserializeObject(input, typeof(MailChimpException), new MailChimpExceptionJsonConverter());

            Assert.NotNull(e);
            Assert.IsType<MailChimpException>(e);

            var mcex=e as MailChimpException;
            Assert.Equal(expectedMessage, mcex.Message);
            Assert.Equal(expectedCode, mcex.Code);
            Assert.Equal(expectedKind, mcex.Kind);
        }
    }
}
