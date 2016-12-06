using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Entity that represents an interest in a list.</summary>
    public class Interest:
        IModelEntity
    {

        /// <summary>The id for the interest category.</summary>
        [JsonProperty(PropertyName = "category_id")]
        public string CategoryId { get; internal set; }

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        public string ListId { get; internal set; }

        /// <summary>The id for the interest category.</summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }

        /// <summary>This field appears on signup forms and is often phrased as a question.</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>The number of subscribers associated with this interest.</summary>
        [JsonProperty(PropertyName = "subscriber_count")]
        public string SubscriberCount { get; set; }
    }
}
