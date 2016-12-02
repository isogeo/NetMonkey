using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Entity that represents a <see href="http://kb.mailchimp.com/lists/growth/getting-started-with-lists">list</see>.</summary>
    public class List:
        IModelEntity
    {

        /// <summary>A string that uniquely identifies this list.</summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>The name of the list.</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary><see href="http://kb.mailchimp.com/lists/growth/about-the-required-email-footer-content">Contact information displayed in campaign footers</see> to comply with international spam laws.</summary>
        [JsonProperty(PropertyName = "contact")]
        public ListContact Contact { get; set; }

        /// <summary>The <see href="http://kb.mailchimp.com/accounts/compliance-tips/edit-the-permission-reminder">permission reminder</see>/ for the list.</summary>
        [JsonProperty(PropertyName = "permission_reminder")]
        public string PermissionReminder { get; set; }

        /// <summary><see href="http://kb.mailchimp.com/campaigns/design/set-up-email-subject-from-name-and-from-email-address-on-a-campaign">Default values for campaigns</see> created for this list.</summary>
        [JsonProperty(PropertyName = "campaign_defaults")]
        public ListCampaignDefaults CampaignDefaults { get; set; }

        /// <summary>Whether the list supports <see href="http://kb.mailchimp.com/lists/growth/how-to-change-list-name-and-defaults"> multiple formats for emails</see>.</summary>
        [JsonProperty(PropertyName = "email_type_option")]
        public string HasEmailTypeOption { get; set; }
    }
}
