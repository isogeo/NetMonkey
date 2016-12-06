using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Entity that represents a member of a list.</summary>
    public class ListMember:
        IModelEntity
    {

        /// <summary>The MD5 hash of the lowercase version of the list member’s email address.</summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }

        /// <summary>Email address for a subscriber.</summary>
        [JsonProperty(PropertyName = "email_address")]
        public MailAddress EmailAddress { get; set; }

        /// <summary>An identifier for the address across all of MailChimp.</summary>
        [JsonProperty(PropertyName = "unique_email_id")]
        public string EmailId { get; set; }

        /// <summary>Subscriber’s current status.</summary>
        [JsonProperty(PropertyName = "status")]
        public ListMemberStatus? Status { get; set; }

        /// <summary>An individual merge var and value for a member.</summary>
        [JsonProperty(PropertyName = "merge_fields")]
        public Dictionary<string, string> MergeFields { get; set; }

        /// <summary>The key of this object’s properties is the ID of the interest in question.</summary>
        [JsonProperty(PropertyName = "interests")]
        public Dictionary<string, bool> Interests { get; set; }

        /// <summary>The default language for this lists’s forms.</summary>
        [JsonProperty(PropertyName = "language")]
        public CultureInfo Language { get; set; }

        /// <summary><see href="http://kb.mailchimp.com/lists/managing-subscribers/designate-and-send-to-vip-subscribers">VIP status</see> for the subscriber.</summary>
        [JsonProperty(PropertyName = "vip")]
        public bool? Vip { get; set; }

        /// <summary>The list id.</summary>
        [JsonProperty(PropertyName = "list_id")]
        public string ListId { get; internal set; }
    }
}
