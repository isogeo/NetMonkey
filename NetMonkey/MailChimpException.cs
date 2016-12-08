using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
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
        [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Nobody should be able to create this ecxeption anyway")]
        internal MailChimpException():
            base()
        { }

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message for the exception.</param>
        [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Nobody should be able to create this ecxeption anyway")]
        internal MailChimpException(string message):
            base(message)
        { }

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected MailChimpException(SerializationInfo info, StreamingContext context):
            base(info, context)
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
        public string Kind
        {
            get
            {
                if (!Data.Contains("Kind"))
                    return null;
                return Data["Kind"] as string;
            }
            internal protected set
            {
                Data["Kind"]=value;
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

        /// <summary>Gets the errors for this exception.</summary>
        [JsonProperty(PropertyName = "errors")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter is needed for JSON deserialization")]
        public IList<Model.MailChimpError> Errors
        {
            get
            {
                if (!Data.Contains("Errors"))
                    return null;
                return Data["Errors"] as IList<Model.MailChimpError>;
            }
            internal protected set
            {
                Data["Errors"]=value;
            }
        }
    }
}
