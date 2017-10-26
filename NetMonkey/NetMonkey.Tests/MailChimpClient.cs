using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetMonkey.Tests
{

    public class MailChimpClientTests
    {

        [Fact(Skip="Requires a valid MailChimp API key")]
        public async Task GetList_ShouldReturnData()
        {
            var client=new MailChimpClient("test");
            var query = new ListQuery();
            query.IncludeProperty(lr => lr.Lists[0].Id)
                .IncludeProperty(lr => lr.Lists[0].Name)
                .IncludeProperty(lr => lr.TotalItems);

            var results = await client.GetLists(query, CancellationToken.None);
            Assert.Equal(5, results.TotalItems);
        }

        [Fact(Skip = "Requires a valid MailChimp API key")]
        public async Task GetListMember_ShouldReturnData()
        {
            var client = new MailChimpClient("test");
            var query = new FieldsQuery<Model.ListMember>();
            query.IncludeProperty(lm => lm.Id)
                .IncludeProperty(lm => lm.Interests)
                .IncludeProperty(lm => lm.MergeFields);

            //var results = await client.GetListMember("eac8899d91", "140efef0331c72feaecfcd71c7903db9", query);
            var results = await client.GetListMember("eac8899d91", new MailAddress("valentin.blanlot@gmail.com"), query, CancellationToken.None);
        }

        [Fact(Skip = "Requires a valid MailChimp API key")]
        public async Task GetInterestCategory_ShouldReturnData()
        {
            var client = new MailChimpClient("test");
            var query = new ResultsQuery<Model.InterestCategoryResults>();
            query.IncludeProperty(icr => icr.Categories[0].Id)
                .IncludeProperty(icr => icr.Categories[0].Title);

            var results = await client.GetInterestCategories("eac8899d91", query, CancellationToken.None);
        }

        [Fact(Skip = "Requires a valid MailChimp API key")]
        public async Task GetInterest_ShouldReturnData()
        {
            var client = new MailChimpClient("test");
            var query = new ResultsQuery<Model.InterestResults>();
            query.IncludeProperty(ir => ir.Interests[0].Id)
                .IncludeProperty(ir => ir.Interests[0].Name);

            var results = await client.GetInterests("eac8899d91", "4a51da5e25", query, CancellationToken.None);
        }
    }
}
