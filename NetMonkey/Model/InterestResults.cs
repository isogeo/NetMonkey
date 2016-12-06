using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Results for a list interest query.</summary>
    public class InterestResults:
        IResults
    {

        /// <summary>An array of this category’s interests.</summary>
        [JsonProperty(PropertyName = "interests")]
        public IList<Interest> Interests { get; internal protected set; }

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        public string ListId { get; internal set; }

        /// <summary>The id for the interest category.</summary>
        [JsonProperty(PropertyName = "category_id")]
        public string CategoryId { get; internal set; }

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        public int TotalItems { get; protected internal set; }
    }
}
