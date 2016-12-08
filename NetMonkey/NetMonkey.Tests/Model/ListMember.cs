using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace NetMonkey.Model.Tests
{
    public class ListMemberTests
    {

        [Theory]
        [InlineData("TEST_NAME_UPPERCASE", "TEST_VALUE_UPPERCASE")]
        [InlineData("test_name_lowercase", "test_value_lowercase")]
        public void ListMember_MergeFields_AreSerializedWithTheProperCase(string name, string value)
        {
            var client = new MailChimpClient("test-us1");
            var member = new ListMember() {
                MergeFields=new Dictionary<string, string>() {
                    { name, value }
                }
            };

            var serialized = JsonConvert.SerializeObject(member, client.SerializerSettings);
            var obj=(JObject)JToken.Parse(serialized);
            var field = (JProperty)obj["merge_fields"].First;

            Assert.Equal(name, field.Name);
            Assert.Equal(value, field.Value);
        }
    }
}
