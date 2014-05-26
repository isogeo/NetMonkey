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

        [Fact(Skip="Requires a valid MailChimp API key")]
        public async Task List_ShouldReturnData()
        {
            var client=new MailChimpClient("test-us4");
            var filter=new ListFilter() {
                Name="Users"
            };

            var lists=await client.ListAsync(new CancellationToken(), filter);
            Assert.Equal(1, lists.Total);
            Assert.Equal(1, lists.Data.Count);
        }

        [Fact(Skip="Requires a valid MailChimp API key")]
        public async Task MemberInfo_ShouldReturnData()
        {
            var client=new MailChimpClient("test-us4");
            var filter=new ListFilter() {
                Name="Users"
            };
            var lists=await client.ListAsync(new CancellationToken(), filter);
            var lid=lists.Data[0].Identifier;
            var email=new Email() {
                Address="admin-app@isogeo.com"
            };

            var info=await client.MemberInfoAsync<MergeVariables>(lid, new [] { email }, new CancellationToken());

            Assert.Equal(1, info.SuccessCount);
        }
    }
}
