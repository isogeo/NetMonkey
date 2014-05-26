using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Grouping related information.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class Grouping
    {

        /// <summary>The grouping id.</summary>
        [JsonProperty("id")]
        public int? Identifier { get; set; }

        /// <summary>The interest group name.</summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>The list email id.</summary>
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
    }
}
