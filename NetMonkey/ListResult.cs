using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Result of the operation including valid data and any errors.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class ListResult
    {

        /// <summary>The total number of lists which matched the provided filters.</summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>The lists which matched the provided filters.</summary>
        [JsonProperty("data")]
        public List<ListData> Data { get; set; }
    }
}
