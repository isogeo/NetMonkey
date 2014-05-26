using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NetMonkey
{




    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>An exception thrown by the MailChimp API.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    [JsonConverter(typeof(Serialization.MailChimpExceptionJsonConverter))]
    public class MailChimpException:
        Exception
    {

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        protected MailChimpException()
        {
        }

        /// <summary>Creates an new instance of the <see cref="MailChimpException" /> class.</summary>
        protected MailChimpException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            info.AddValue(_CodeValueName, _Code);
            info.AddValue(_KindValueName, (int)_Kind);
        }

        /// <summary>Creates a new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="code">The MailChimp code related to the exception.</param>
        /// <param name="kind">The MailChimp error kind.</param>
        internal MailChimpException(string message, int code, MailChimpExceptionKind kind):
            base(message ?? SR.UnknownMailChimpException)
        {
            _Code=code;
            _Kind=kind;
        }

        /// <summary>Sets the <see cref="SerializationInfo" /> with information about the exception.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _Code=info.GetInt32(_CodeValueName);
            _Kind=(MailChimpExceptionKind)info.GetInt32(_KindValueName);
        }

        /// <summary>Gets the MailChimp code related to the exception.</summary>
        public int Code
        {
            get
            {
                return _Code;
            }
        }

        /// <summary>The MailChimp error kind.</summary>
        public MailChimpExceptionKind Kind
        {
            get
            {
                return _Kind;
            }
        }

        private const string _CodeValueName="Code";
        private const string _KindValueName="Kind";

        private int _Code;
        private MailChimpExceptionKind _Kind;
    }
}
