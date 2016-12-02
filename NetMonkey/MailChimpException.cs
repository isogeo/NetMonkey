using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NetMonkey
{




    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>An exception thrown by the MailChimp API.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [Serializable]
    public class MailChimpException:
        Exception
    {

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        internal MailChimpException():
            base()
        { }

        /// <summary>Creates a new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        internal MailChimpException(string message) :
            base(message)
        { }

        /// <summary>Creates a new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="inner">The inner exception.</param>
        internal MailChimpException(string message, Exception inner) :
            base(message, inner)
        { }

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        protected MailChimpException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            if (!string.IsNullOrEmpty(Details))
                info.AddValue("Message", Details);

            info.AddValue("Instance", Instance);
            info.AddValue("Status", (int)StatusCode);
            info.AddValue("Type", Type);
            info.AddValue("Title", Title);
        }

        /// <summary>Sets the <see cref="SerializationInfo" /> with information about the exception.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            Details=info.GetString("Message");
            Instance=info.GetString("Instance");
            StatusCode=(HttpStatusCode)info.GetInt32("Status");
            Type=info.GetString("Type");
            Title=info.GetString("Title");
        }

        /// <summary>Gets the instance concerned by this exception.</summary>
        [JsonProperty(PropertyName = "instance")]
        public string Instance { get; internal protected set; }

        /// <summary>Gets the status associated with this exception.</summary>
        [JsonProperty(PropertyName = "status")]
        public HttpStatusCode StatusCode { get; internal protected set; }

        /// <summary>Gets the type of this exception.</summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; internal protected set; }

        /// <summary>Gets the title of this exception.</summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; internal protected set; }

        /// <summary>Gets the message for this exception.</summary>
        public override string Message
        {
            get
            {
                return Details ?? base.Message;
            }
        }

        [JsonProperty(PropertyName = "details")]
        internal string Details { get; set; }
    }
}
