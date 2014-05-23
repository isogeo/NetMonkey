using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private MailChimpException()
        {
        }

        /// <summary>Creates a new instance of the <see cref="MailChimpException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="code">The MailChimp code related to the exception.</param>
        /// <param name="kind">The MailChimp error kind.</param>
        public MailChimpException(string message, int code, MailChimpExceptionKind kind):
            base(message ?? SR.UnknownMailChimpException)
        {
            _Code=code;
            _Kind=kind;
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

        private int _Code;
        private MailChimpExceptionKind _Kind;
    }
}
