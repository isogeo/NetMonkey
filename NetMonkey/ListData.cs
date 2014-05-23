using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Data related to a list.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class ListData
    {

        /// <summary>The list id for this list. This will be used for all other list management functions.</summary>
        [JsonProperty("id")]
        public string Identifier { get; set; }

        /// <summary>The list id used in our web app, allows you to create a link directly to it.</summary>
        [JsonProperty("web_id")]
        public int WebIdentifier { get; set; }

        /// <summary>The name of the list.</summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>The date that this list was created.</summary>
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
    }
}
