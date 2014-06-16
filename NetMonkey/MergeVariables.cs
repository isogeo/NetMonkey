using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Optional merges for the email (FNAME, LNAME, <see href="http://kb.mailchimp.com/article/where-can-i-find-my-lists-merge-tags">etc</see>.).</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class MergeVariables
    {

        /// <summary>Set this to change the email address. This is only respected on calls using update_existing or when passed to listUpdateMember.</summary>
        [JsonProperty("new-email")]
        public string NewEmail { get; set; }

        /// <summary>Set the Opt-in IP field.</summary>
        [JsonProperty("optin_ip")]
        public string OptInIP { get; set; }

        /// <summary>Set the Opt-in Time field.</summary>
        [JsonProperty("optin_time")]
        public DateTime? OptInTime { get; set; }

        /// <summary>Set the Opt-in Time field.</summary>
        [JsonProperty("mc_language")]
        public CultureInfo Language { get; set; }

        /// <summary>Interest groupings.</summary>
        [JsonProperty("groupings")]
        public List<Grouping> Groupings { get; set; }
    }
}
