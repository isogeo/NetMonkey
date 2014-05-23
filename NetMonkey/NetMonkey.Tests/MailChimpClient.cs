using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetMonkey.Tests
{

    public class MailChimpClientTests
    {

        [Fact(Skip="Requires a valid MailChimp API key")]
        public async Task Ping_ShouldReturnMessage()
        {
            var client=new MailChimpClient("test-us4");

            var msg=await client.PingAsync(new CancellationToken());
            Assert.Equal("Everything's Chimpy!", msg);

        }

        [Fact]
        public async Task Ping_ShouldFailForABadApiKey()
        {
            var client=new MailChimpClient("test-us4");

            var mcex=await AssertEx.ThrowsAsync<MailChimpException>(async () => await client.PingAsync(new CancellationToken()));
            Assert.Equal(MailChimpExceptionKind.ApiInvalidKey, mcex.Kind);
        }
    }
}
