using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Campaign default setting for a list.</summary>
    public class ListCampaignDefaults:
        IModelObject
    {


        /// <summary>The default from name for campaigns sent to this list.</summary>
        [JsonProperty(PropertyName = "from_name")]
        public string FromName { get; set; }


        /// <summary>The default from email for campaigns sent to this list.</summary>
        [JsonProperty(PropertyName = "from_email")]
        public string FromEmail { get; set; }


        /// <summary>The default subject line for campaigns sent to this list.</summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }


        /// <summary>The default language for this lists’s forms.</summary>
        [JsonProperty(PropertyName = "language")]
        public CultureInfo Language { get; set; }
    }
}
