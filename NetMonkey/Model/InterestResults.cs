using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Results for a list interest query.</summary>
    public class InterestResults:
        IResults
    {

        /// <summary>An array of this category’s interests.</summary>
        [JsonProperty(PropertyName = "interests")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is needed for JSON deserialization")]
        public IList<Interest> Interests { get; internal protected set; }

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Setter is needed for JSON deserialization")]
        public string ListId { get; internal set; }

        /// <summary>The id for the interest category.</summary>
        [JsonProperty(PropertyName = "category_id")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Setter is needed for JSON deserialization")]
        public string CategoryId { get; internal set; }

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        public int TotalItems { get; protected internal set; }
    }
}
