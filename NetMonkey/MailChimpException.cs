using System;
using System.Net;
using Newtonsoft.Json;

namespace NetMonkey
{

    /// <summary>An exception thrown by the MailChimp API.</summary>
    [JsonConverter(typeof(Model.Serialization.MailChimpExceptionJsonConverter))]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [Serializable]
    public class MailChimpException:
        Exception
    {

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        internal MailChimpException():
            base()
        { }

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message for the exception.</param>
        internal MailChimpException(string message):
            base(message)
        { }

        /// <summary>Gets the instance concerned by this exception.</summary>
        [JsonProperty(PropertyName = "instance")]
        public string Instance
        {
            get
            {
                if (!Data.Contains("Instance"))
                    return null;
                return Data["Instance"] as string;
            }
            internal protected set
            {
                Data["Instance"]=value;
            }
        }

        /// <summary>Gets the status associated with this exception.</summary>
        [JsonProperty(PropertyName = "status")]
        public HttpStatusCode StatusCode
        {
            get
            {
                if (!Data.Contains("Status"))
                    return 0;
                return (HttpStatusCode)Data["Status"];
            }
            internal protected set
            {
                Data["Status"]=value;
            }
        }

        /// <summary>Gets the type of this exception.</summary>
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                if (!Data.Contains("Type"))
                    return null;
                return Data["Type"] as string;
            }
            internal protected set
            {
                Data["Type"]=value;
            }
        }

        /// <summary>Gets the title of this exception.</summary>
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get
            {
                if (!Data.Contains("Title"))
                    return null;
                return Data["Title"] as string;
            }
            internal protected set
            {
                Data["Title"]=value;
            }
        }
    }
}
