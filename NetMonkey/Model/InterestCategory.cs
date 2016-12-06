using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Entity that represents a category interest in a list.</summary>
    public class InterestCategory:
        IModelEntity
    {

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        public string ListId { get; internal set; }

        /// <summary>The id for the interest category.</summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }

        /// <summary>This field appears on signup forms and is often phrased as a question.</summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>The default language for this lists’s forms.</summary>
        [JsonProperty(PropertyName = "language")]
        public CultureInfo Language { get; set; }
    }
}
