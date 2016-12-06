using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Results for a lists related query.</summary>
    public class ListResults:
        IResults
    {

        /// <summary>An array of objects, each representing a list.</summary>
        [JsonProperty(PropertyName = "lists")]
        public IList<List> Lists { get; internal protected set; }

        /// <summary>The total number of items matching the query regardless of pagination.</summary>
        [JsonProperty(PropertyName = "total_items")]
        public int TotalItems { get; protected internal set; }
    }
}
