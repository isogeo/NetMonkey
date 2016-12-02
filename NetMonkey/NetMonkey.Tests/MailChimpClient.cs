using System.Threading.Tasks;
using Xunit;

namespace NetMonkey.Tests
{

    public class MailChimpClientTests
    {

        [Fact(Skip="Requires a valid MailChimp API key")]
        public async Task List_ShouldReturnData()
        {
            var client=new MailChimpClient("test-us4");

            var results = await client.GetLists(null);
            Assert.Equal(5, results.TotalItems);
        }
    }
}
