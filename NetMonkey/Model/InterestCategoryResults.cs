using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Results for a list interest categories query.</summary>
    public class InterestCategoryResults:
        IResults
    {

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Setter is needed for JSON deserialization")]
        public string ListId { get; internal set; }

        /// <summary>This array contains individual interest categories.</summary>
        [JsonProperty(PropertyName = "categories")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is needed for JSON deserialization")]
        public IList<InterestCategory> Categories { get; internal protected set; }

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        public int TotalItems { get; protected internal set; }
    }
}
