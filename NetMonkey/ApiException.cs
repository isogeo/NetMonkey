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
    /// <summary>MaiChimp API related exception.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public sealed class ApiException:
        MailChimpException
    {

        private ApiException()
        {
        }

        /// <summary>Creates a new instance of the <see cref="ApiException" /> class.</summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="code">The MailChimp code related to the exception.</param>
        /// <param name="kind">The MailChimp error kind.</param>
        internal ApiException(string message, int code, MailChimpExceptionKind kind) :
            base(message, code, kind)
        {
        }
    }
}
