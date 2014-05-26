using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Group related information.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class Group
    {

        /// <summary>The group name.</summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>Whether the member has this group selected.</summary>
        [JsonProperty("interested")]
        public bool? IsInterested { get; set; }
    }
}
