using System;
using System.Globalization;
using Newtonsoft.Json;

namespace NetMonkey.Model
{

    /// <summary>Error that may be returned as part of the API.</summary>
    [Serializable]
    public class MailChimpError:
        IModelObject
    {

        /// <summary>Converts the value of this instance to a string representation.</summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}: {1}",
                Field,
                Message
            );
        }

        /// <summary>The field that is responsible for the error.</summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>The error message.</summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
