using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetMonkey
{



    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Data related to a member.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization.OptIn)]
    public class MemberInfoData<TMergeVariables>
        where TMergeVariables:
            MergeVariables
    {

        /// <summary>The unique id (euid) for this email address on an account.</summary>
        [JsonProperty("id")]
        public string Identifier { get; set; }

        /// <summary>The email address associated with this record.</summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>The type of emails this customer asked to get.</summary>
        [JsonProperty("email_type")]
        public EmailType? EmailType { get; set; }

        /// <summary>Merges for the email (FNAME, LNAME, <see href="http://kb.mailchimp.com/article/where-can-i-find-my-lists-merge-tags">etc</see>.).</summary>
        [JsonProperty("merges")]
        public TMergeVariables MergeVariables { get; set; }

        /// <summary>The Member id used in our web app, allows you to create a link directly to it.</summary>
        [JsonProperty("web_id")]
        public int WebIdentifier { get; set; }
    }
}
