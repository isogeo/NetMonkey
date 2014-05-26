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
    /// <summary>MailChimp list related exception.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public sealed class ListException:
        MailChimpException
    {

        private ListException()
        {
        }

        /// <summary>Creates a new instance of the <see cref="ListException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="code">The MailChimp code related to the exception.</param>
        /// <param name="kind">The MailChimp error kind.</param>
        internal ListException(string message, int code, MailChimpExceptionKind kind) :
            base(message, code, kind)
        {
        }
    }
}
