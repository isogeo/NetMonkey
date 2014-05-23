using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Email related information.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class Email
    {

        /// <summary>An email address.</summary>
        [JsonProperty("email")]
        public string Address { get; set; }

        /// <summary>The unique id for an email address.</summary>
        [JsonProperty("euid")]
        public string Identifier { get; set; }

        /// <summary>The list email id.</summary>
        [JsonProperty("leid")]
        public string ListIdentifier { get; set; }
    }
}
