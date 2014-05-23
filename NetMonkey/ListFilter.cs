using System;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Filters to apply to a list query.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ListFilter
    {

        /// <summary>Return a single list using a known list_id. Accepts multiples separated by commas when not using exact matching.</summary>
        [JsonProperty("list_id")]
        public string Identifier { get; set; }

        /// <summary>Only lists that match this name.</summary>
        [JsonProperty("list_name")]
        public string Name { get; set; }

        /// <summary>Only lists that have a default from name matching this.</summary>
        [JsonProperty("from_name")]
        public string FromName { get; set; }

        /// <summary>Only lists that have a default from email matching this.</summary>
        [JsonProperty("from_email")]
        public string FromEmail { get; set; }

        /// <summary>Only lists that have a default from subject matching this.</summary>
        [JsonProperty("from_subject")]
        public string FromSubject { get; set; }

        /// <summary>only show lists that were created before this.</summary>
        [JsonProperty("created_before")]
        public DateTime? CreatedBefore { get; set; }

        /// <summary>only show lists that were created since this.</summary>
        [JsonProperty("created_after")]
        public DateTime? CreatedAfter { get; set; }

        /// <summary>Flag for whether to filter on exact values when filtering, or search within content for filter values.</summary>
        [JsonProperty("exact")]
        public bool? ExactMatch { get; set; }
    }
}
