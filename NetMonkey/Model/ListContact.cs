using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Entity that represents a list contact./</summary>
    public class ListContact:
        IModelObject
    {

        /// <summary>The company name for the list.</summary>
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        /// <summary>The street address for the list contact.</summary>
        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; }

        /// <summary>The street address for the list contact.</summary>
        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; }

        /// <summary>The city for the list contact.</summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>The state for the list contact.</summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>	The postal or zip code for the list contact.</summary>
        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        /// <summary></summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>The phone number for the list contact.</summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
    }
}
