using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Results for a list interest categories query.</summary>
    public class InterestCategoryResults:
        IResults
    {

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        public string ListId { get; internal set; }

        /// <summary>This array contains individual interest categories.</summary>
        [JsonProperty(PropertyName = "categories")]
        public IList<InterestCategory> Categories { get; internal protected set; }

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        public int TotalItems { get; protected internal set; }
    }
}
